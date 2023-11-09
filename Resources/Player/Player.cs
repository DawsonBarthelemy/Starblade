using Godot;
using System;



public partial class Player : CharacterBody2D
{
	// Variables Used
	AnimationTree playeranimation;
	Sprite2D playersprite;
	Vector2 velocity = Vector2.Zero;
	AnimationNodeStateMachinePlayback animationState;
	const double original_speed = 10;
	Camera2D camera;
	Vector2 zoom;
	Vector2 minimumzoom = new Vector2(0.5f, 0.5f);
	Vector2 maximumzoom = new Vector2(1.2f, 1.2f);
	Vector2 zoomfactor = new Vector2(0.02f, 0.02f);
	PackedScene scene = GD.Load<PackedScene>("res://Resources/UI/options_ui.tscn");
	//PackedScene scene = GD.Load<PackedScene>("res://options_menu");  // godot loads the Resource when it reads the line.
	bool options_open;
	TabContainer tabContainerNode;
	Vector2 cameraPosition;
	Vector2 screenSize;
	Control options;
	Callable closeoptions;



	// Setting Camera Zoom
	void SetZoom(Vector2 value)
	{
		if (value.X > maximumzoom.X)
		{
			value.X = maximumzoom.X;
		}
		if (value.Y > maximumzoom.Y)
		{
			value.Y = maximumzoom.Y;
		}

		if (value.X < minimumzoom.X)
		{
			value.X = minimumzoom.X;
		}
		if (value.Y < minimumzoom.Y)
		{
			value.Y = minimumzoom.Y;
		}

		camera.Zoom = value;
	}

	// Called when the node enters the scene tree for the first time.
	//---------------------------------------------------------------
	public override void _Ready()
	{
		playeranimation = GetNode<AnimationTree>("PlayerAnims");
		playersprite = (Sprite2D)GetNode("player_sprite_male");
		animationState = (AnimationNodeStateMachinePlayback)playeranimation.Get("parameters/playback");

		camera = GetNode<Camera2D>("view");
		Vector2 zoom = new Vector2(1, 1);
        

    }

    //---------------------------------------------------------------

    // Called every frame. 'delta' is the elapsed time since the previous frame.
    public override void _Process(double delta)
	{
		float scaling = .6f;
		if (Input.IsActionJustPressed("open_menu") && options_open)
		{
				options_open = false;
				options.QueueFree();
				camera.Zoom = new Vector2(1f, 1f);
		}
		// Menu Script
		else if (Input.IsActionJustPressed("open_menu") && !options_open)
		{
			options_open = true;

			options = (options_ui)scene.Instantiate();



			options.Size = new Vector2(400, 300);
			options.Scale *= scaling;

			options.Position = new Vector2((camera.Position.X-(options.Size.X)*scaling/2), (camera.Position.Y - (options.Size.Y)*scaling/2));
			//options.Position = camera.GlobalPosition;

			camera.AddChild(options);

			tabContainerNode = options.GetNode<TabContainer>("options_tabs");
			tabContainerNode.CurrentTab = 2;
		}



		// Check if options are not open
		if (!options_open)
		{
			if (Input.IsActionPressed("zoom_in"))
			{
				zoom += zoomfactor;
				SetZoom(zoom);
			}
			if (Input.IsActionPressed("zoom_out"))
			{
				zoom -= zoomfactor;
				SetZoom(zoom);
			}
			if (Input.IsActionJustPressed("zoom_reset"))
			{
				zoom = new Vector2(1,1);
				SetZoom(zoom);
			}
			double speed = original_speed;

			velocity.X = Input.GetActionStrength("move_right") - Input.GetActionStrength("move_left");
			velocity.Y = Input.GetActionStrength("move_down") - Input.GetActionStrength("move_up");

			velocity = velocity.Normalized() * (float)speed;

			if (velocity == Vector2.Zero)
			{
				velocity = Vector2.Zero;

				animationState.Travel("Idle");
				
			}
			else
			{
				animationState.Travel("Walk");
				playeranimation.Set("parameters/Idle/blend_position", velocity);
				playeranimation.Set("parameters/Walk/blend_position", velocity);
			}
			this.Velocity = velocity * new Vector2((float)speed, (float)speed);
			MoveAndSlide();



		}
	}
}
