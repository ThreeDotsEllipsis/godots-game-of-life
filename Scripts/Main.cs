using Godot;
using System;

public class Main : Node2D
{
    Cells cells;

    public override void _Ready()
    {
        cells = GetNode<Cells>("Cells");
    }

    private void _on_TickTimer_timeout()
    {
        cells.NextTurn();
    }
}


