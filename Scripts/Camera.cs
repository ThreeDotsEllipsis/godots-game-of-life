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

            else if (mbevent.ButtonIndex == (int)ButtonList.WheelDown)
            {
                this.Zoom += new Vector2(0.1f, 0.1f);
            }
            else if (mbevent.ButtonIndex == (int)ButtonList.WheelUp)
            {
                this.Zoom -= new Vector2(0.1f, 0.1f);
            }
        }

        else if (ievent is InputEventMouseMotion mmevent)
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
