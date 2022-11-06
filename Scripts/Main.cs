using Godot;
using System;

public class Main : Node2D
{
    public override void _Ready()
    {
    }

    private void _on_TickTimer_timeout()
    {
        GetNode<Cells>("Cells").NextTurn();
    }
}


