using Godot;
using System;

public partial class WesternPlayer : CharacterBody2D
{
	[Export]
	private float speed = 100f;
	private Vector2 currentVelocity;
	AnimatedSprite2D charAnim;

	[Export]
	private PackedScene bulletScene; // Bullet scene referenciája

	public override void _Ready()
	{
		charAnim = GetNode<AnimatedSprite2D>("AnimatedSprite2D");
	}
	
	public override void _PhysicsProcess(double delta)
	{
		base._PhysicsProcess(delta);

		mozgasInput();

		Velocity = currentVelocity;

		MoveAndSlide();

		// Lövés gomb kezelése (pl. bal egérgomb)
		if (Input.IsActionJustPressed("Shoot"))
		{
			Shoot();
		}
	}
	
	private void mozgasInput() 
	{
		currentVelocity = Input.GetVector("ui_left", "ui_right", "ui_up", "ui_down");
		if(currentVelocity.X > 0)
		{
			charAnim.Play("run_right");
		}
		else if(currentVelocity.X < 0)
		{
			charAnim.Play("run_left");
		}
		else if(currentVelocity.Y > 0)
		{
			charAnim.Play("run_down");
		}
		else if(currentVelocity.Y < 0)
		{
			charAnim.Play("run_up");
		}
		else
		{
			charAnim.Play("idle");
		}

		currentVelocity *= speed;
	}

	private void Shoot()
	{
		// Bullet instance létrehozása
		Bullet bulletInstance = (Bullet)bulletScene.Instantiate();

		// A játékos pozíciójába helyezzük a bulletet
		bulletInstance.Position = GlobalPosition;

		// Az egér pozíciójának lekérése
		Vector2 mousePosition = GetGlobalMousePosition();

		// Az irány kiszámítása a bullet számára
		Vector2 direction = (mousePosition - GlobalPosition).Normalized();
		
		// Átadjuk az irányt a bulletnek
		bulletInstance.SetDirection(direction);

		// Hozzáadjuk a scene-hez a bulletet
		GetParent().AddChild(bulletInstance);
	}
}
