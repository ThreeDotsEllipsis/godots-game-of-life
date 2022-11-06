using Godot;
using System;

public class Cells : TileMap
{
    private Godot.Collections.Array usedCells;

    public override void _Ready()
    {
        base._Ready();
        GD.Print(this.GetUsedCells());
    }

    public void NextTurn()
    {
        usedCells = this.GetUsedCells();
        this.CreateCells();
        this.DestroyCells();
    }

    private void CreateCells()
    {
        foreach (Vector2 cell in usedCells)
        {
            for (int x = (int)cell.x - 1; x <= cell.x + 1; x++)
            {
                for (int y = (int)cell.y - 1; y <= cell.y + 1; y++)
                {


                    int numOfAliveCells = 0;

                    for (int x1 = x - 1; x1 <= x + 1; x1++)
                    {
                        for (int y1 = y - 1; y1 <= y + 1; y1++)
                        {
                            if (usedCells.Contains(new Vector2(x1, y1)))
                            {
                                numOfAliveCells++;
                            }
                        }
                    }

                    if (numOfAliveCells == 3)
                    {
                        this.SetCell(x, y, 0);
                    }


                }
            }
        }
    }

    private void DestroyCells()
    {
        foreach (Vector2 cell in usedCells)
        {
            int numOfAliveCells = -1;

            for (int x = (int)cell.x - 1; x <= cell.x + 1; x++)
            {
                for (int y = (int)cell.y - 1; y <= cell.y + 1; y++)
                {
                    if (usedCells.Contains(new Vector2(x, y)))
                    {
                        numOfAliveCells++;
                    }
                }
            }

            if (numOfAliveCells < 2)
            {
                this.SetCell((int)cell.x, (int)cell.y, -1);
            }

            if (numOfAliveCells > 3)
            {
                this.SetCell((int)cell.x, (int)cell.y, -1);
            }
        }
    }
}
