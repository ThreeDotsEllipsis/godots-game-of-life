using Godot;
using System;

public class Main : Node2D
{
    Cells cells;
    Sprite hintSprite;
    Camera2D camera;


    public override void _Ready()
    {
        cells = GetNode<Cells>("Cells");
        hintSprite = GetNode<Sprite>("HintSprite");
        camera = GetNode<Camera2D>("Camera");
    }

    private void _on_TickTimer_timeout()
    {
        cells.NextTurn();
    }

    public override void _Input(InputEvent ievent)
    {
        base._Input(ievent);

        if (ievent is InputEventMouseMotion mmevent)
        {
            var cellPosition = new Vector2();
            cellPosition.x = (int)(mmevent.Position.x / (64 / camera.Zoom.x) + camera.Position.x / 64);
            cellPosition.y = (int)(mmevent.Position.y / (64 / camera.Zoom.y) + camera.Position.y / 64);

            hintSprite.Position = cellPosition * 64;

            if (Input.IsMouseButtonPressed((int)ButtonList.Left))
            {
                cells.SetCell((int)cellPosition.x, (int)cellPosition.y, 0);
            }
            else if (Input.IsMouseButtonPressed((int)ButtonList.Right))
            {
                cells.SetCell((int)cellPosition.x, (int)cellPosition.y, -1);
            }
        }

        else if (ievent is InputEventMouseButton mbevent)
        {
            if (mbevent.ButtonIndex == (int)ButtonList.Left)
            {
                cells.SetCell((int)hintSprite.Position.x / 64, (int)hintSprite.Position.y / 64, 0);
            }
            else if (mbevent.ButtonIndex == (int)ButtonList.Right)
            {
                cells.SetCell((int)hintSprite.Position.x / 64, (int)hintSprite.Position.y / 64, -1);
            }
        }

    }
}


