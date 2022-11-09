using Godot;
using System;

public class Main : Node2D
{
    Cells cells;
    Sprite hintSprite;
    Camera2D camera;
    Timer tickTimer;
    Button startButton;

    public override void _Ready()
    {
        cells = GetNode<Cells>("Cells");
        hintSprite = GetNode<Sprite>("HintSprite");
        camera = GetNode<Camera2D>("Camera");
        tickTimer = GetNode<Timer>("TickTimer");
        startButton = GetNode<Button>("UI/StartButton");
    }

    private void _on_TickTimer_timeout()
    {
        cells.NextTurn();
    }

    public override void _UnhandledInput(InputEvent ievent)
    {
        base._UnhandledInput(ievent);

        if (ievent is InputEventMouseMotion mmevent)
        {
            var cellPosition = new Vector2();
            cellPosition.x = (int)(mmevent.Position.x / (64 / camera.Zoom.x) + camera.Position.x / 64);
            cellPosition.y = (int)(mmevent.Position.y / (64 / camera.Zoom.y) + camera.Position.y / 64);

            var mouseGlobalPosition = mmevent.Position + camera.Position;

            if (mouseGlobalPosition.x < 0)
            {
                cellPosition.x -= 1;
            }
            if (mouseGlobalPosition.y < 0)
            {
                cellPosition.y -= 1;
            }

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

    private void _on_StartButton_pressed()
    {
        if (tickTimer.IsStopped())
        {
            tickTimer.Start();
            startButton.Text = "STOP";
        }
        else
        {
            tickTimer.Stop();
            startButton.Text = "START";
        }
    }
}




