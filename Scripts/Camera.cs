using Godot;
using System;

public class Camera : Camera2D
{
    [Export]
    float moveSpeed = 5f;

    public override void _Process(float delta)
    {
        base._Process(delta);

        float x = Input.GetActionStrength("right") - Input.GetActionStrength("left");
        float y = Input.GetActionStrength("down") - Input.GetActionStrength("up");

        this.Position += new Vector2(x, y) * moveSpeed;
    }
}
