using Godot;
using System;

public partial class WesternPlayer : CharacterBody2D
{
	[Export]
	private float speed = 100f;
	private Vector2 currentVelocity;
	AnimatedSprite2D charAnim;
	
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

	}
	
	private void mozgasInput() {

		currentVelocity = Input.GetVector("ui_left" , "ui_right" , "ui_up", "ui_down" );
		if(currentVelocity.X > 0){
			charAnim.Play("run_right");
		}
		else if(currentVelocity.X < 0){
			charAnim.Play("run_left");
		}
		else if(currentVelocity.Y > 0){
			charAnim.Play("run_down");
		}
		else if(currentVelocity.Y < 0){
			charAnim.Play("run_up");
		}else
			charAnim.Play("idle");
		currentVelocity *= speed;



	}
}
