using Godot;
using System;

// Attaches to the options_b button Node
public partial class options : Button
{
	AudioStreamPlayer audio;
	
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}
	public void pressed()
	{
		audio = GetNode<AudioStreamPlayer>("click");
		audio.Play(0);
	}
}
