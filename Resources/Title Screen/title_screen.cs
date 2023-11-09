using Godot;
using System;

public partial class title_screen : Node2D
{
	private AnimatedSprite2D _background;
	private Sprite2D male_player_sprite;
	private AudioStreamPlayer music;
	// Called when the node enters the scene tree for the first time.
	
	public void _on_background_music_finished()
	{
		music.Play(0);
	}
	public override void _Ready()
	{
		_background = GetNode<AnimatedSprite2D>("Background");
		_background.Frame = 0;
		_background.Play("bg_anim");

		male_player_sprite = GetNode<Sprite2D>("Male_player");
		music = GetNode<AudioStreamPlayer>("background_music");
		music.Play(0);
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}
}
