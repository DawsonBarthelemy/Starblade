using Godot;
using System;

public partial class options_resume : Button
{
	void _on_pressed()
	{
		QueueFree();
	}
}
