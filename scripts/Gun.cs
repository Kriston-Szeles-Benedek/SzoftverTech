using Godot;
using System;

public partial class Gun : Node2D
{
	[Export] PackedScene bullet;
	[Export] float bullet_speed = 700f;
	[Export] float bulletPerSec = 3f; // 3 lövés másodpercenként
	[Export] float bulletDamage = 30f;
	
	float fireRate;
	float timeUntilFire = 0f;
	
	private WesternPlayer player;

	public override void _Ready()
	{
		fireRate = 1f / bulletPerSec;  // fireRate = 1 / 3f = 0.33 másodpercenként
		player = GetParent<WesternPlayer>();  // Feltételezzük, hogy a játékos a szülője
	}

	public override void _Process(double delta)
	{
		// Folyamatosan csökkentjük az időt a következő lövésig
		timeUntilFire -= (float)delta;

		// Lövés bemenet és időzítés ellenőrzése
		if (Input.IsActionJustPressed("shoot") && timeUntilFire <= 0f)
		{
			// Lövedék létrehozása
			RigidBody2D bulletInstance = bullet.Instantiate<RigidBody2D>();
			
			// Lövedék pozíciójának beállítása
			bulletInstance.GlobalPosition = GlobalPosition;

			// Lövés iránya: a játékos mozgási iránya (Velocity) normálizálva
			Vector2 shootDirection = player.Velocity.Normalized();
			bulletInstance.LinearVelocity = shootDirection * bullet_speed;

			// Lövedék hozzáadása a játékhierarchiához
			GetTree().Root.AddChild(bulletInstance);

			// Időzítés újraindítása
			timeUntilFire = fireRate;
		}
	}
}
