using Godot;
using System;

// Attaches to the quit_b button Node
public partial class quit : Button
{
	AudioStreamPlayer audio;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		audio = GetNode<AudioStreamPlayer>("click");
	}
	public void _whenpressed()
	{
		audio.Play(0);
		GetTree().Root.PropagateNotification((int)NotificationWMCloseRequest);
	}

	public override void _Notification(int what)
	{
		if (what == NotificationWMCloseRequest) { GetTree().Quit(); }
	}
	
	
	
	
	
}
