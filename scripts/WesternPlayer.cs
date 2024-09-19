using Godot;
using System;

public partial class WesternPlayer : CharacterBody2D
{
	[Export]
	private float speed = 100f;
	private Vector2 currentVelocity;

	
	
	public override void _PhysicsProcess(double delta)
	{
		base._PhysicsProcess(delta);

		mozgasInput();

		Velocity = currentVelocity;

		MoveAndSlide();

	}
	
	private void mozgasInput() {

		currentVelocity = Input.GetVector("ui_left" , "ui_right" , "ui_up", "ui_down" );
		currentVelocity *= speed;



	}
}
