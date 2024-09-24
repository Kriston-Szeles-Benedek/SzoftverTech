using Godot;
using System;

public partial class Bullet : Area2D
{
	private Vector2 direction;
	[Export]
	private float speed = 300f; // Golyó sebessége

	public void SetDirection(Vector2 newDirection)
	{
		direction = newDirection;
	}

	public override void _Process(double delta)
	{
		// A golyó mozgatása
		Position += direction * speed * (float)delta;

		// Kilépés a scene-ből, ha túl messze megy
		if (Position.Length() > 2000)
		{
			QueueFree();
		}
	}
}
