using Godot;
using System;

public class Main : Node2D
{
    Cells cells;
    Sprite hintSprite;
    Camera2D camera;
    Timer tickTimer;
    Button switchButton;
    Button stepButton;
    Slider speedSlider;

    public override void _Ready()
    {
        cells = GetNode<Cells>("Cells");
        hintSprite = GetNode<Sprite>("HintSprite");
        camera = GetNode<Camera2D>("Camera");
        tickTimer = GetNode<Timer>("TickTimer");
        switchButton = GetNode<Button>("UI/SwitchButton");
        stepButton = GetNode<Button>("UI/StepButton");
        speedSlider = GetNode<Slider>("UI/SpeedSlider");
    }

    private void _on_TickTimer_timeout()
    {
        cells.NextTurn();
    }

    public override void _UnhandledInput(InputEvent ievent)
    {
        base._UnhandledInput(ievent);

        var mousePosition = GetGlobalMousePosition();

        if (ievent is InputEventMouseMotion mmevent)
        {
            var cellPosition = (mousePosition / 64);
            cellPosition = new Vector2((int)cellPosition.x, (int)cellPosition.y);

            if (mousePosition.x < 0)
            {
                cellPosition.x -= 1;
            }
            if (mousePosition.y < 0)
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

    private void _on_SwitchButton_pressed()
    {
        if (tickTimer.IsStopped())
        {
            tickTimer.Start();
            switchButton.Text = "AUTOMATIC";
            stepButton.Hide();
            speedSlider.Show();
        }
        else
        {
            tickTimer.Stop();
            switchButton.Text = "MANUAL";
            stepButton.Show();
            speedSlider.Hide();
        }
    }

    private void _on_StepButton_pressed()
    {
        cells.NextTurn();
    }

    private void _on_ClearButton_pressed()
    {
        cells.ClearCells();
    }

    private void _on_SpeedSlider_value_changed(float value)
    {
        tickTimer.WaitTime = 1 / value;
    }
}



