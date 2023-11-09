using Godot;
using System;

// Attaches to the play_b button Node
public partial class play : Button
{
	AudioStreamPlayer audio;
	AnimationPlayer begin_animation;
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		audio = GetNode<AudioStreamPlayer>("click");
		begin_animation = GetNode<AnimationPlayer>("AnimationPlayer");
		begin_animation.Play("Idle");
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}


	public void pressed()
	{
		audio.Play(0);
		begin_animation.Play("Begin");
	}
}
