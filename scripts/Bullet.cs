using Godot;
using System;

public partial class Bullet : Area2D
{
	private Vector2 direction;
	[Export] private float speed = 300f; // Golyó sebessége
	[Export] private int damage = 10;

	public void SetDirection(Vector2 newDirection)
	{
		direction = newDirection;
	}

	public override void _Ready()
	{
		BodyEntered += OnBodyEntered;
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
	
	public void OnBodyEntered(Node2D body)
	{
		if (body is IDamageable damageable)
		{
			damageable.TakeDamage(damage);
		}
		QueueFree();
	}
}
