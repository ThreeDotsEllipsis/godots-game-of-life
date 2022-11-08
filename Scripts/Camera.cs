using Godot;
using System;

public class Camera : Camera2D
{
    [Export]
    float moveSpeed = 5f;

    private Vector2 startMouseDrag = Vector2.Zero;

    public override void _Input(InputEvent ievent)
    {
        base._Input(ievent);

        if (ievent is InputEventMouseButton mbevent)
        {
            if (mbevent.ButtonIndex == (int)ButtonList.Middle)
            {
                startMouseDrag = mbevent.Position;
            }
        }

        if (ievent is InputEventMouseMotion mmevent)
        {
            if (Input.IsMouseButtonPressed((int)ButtonList.Middle))
            {
                var movement = startMouseDrag - mmevent.Position;
                this.Position += movement * moveSpeed;
                startMouseDrag = mmevent.Position;
            }
        }
    }
}
