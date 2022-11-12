using Godot;
using System;

public class Cells : TileMap
{
    private Godot.Collections.Array usedCells;

    private Godot.Collections.Array GetEmptyNeighbourCells(Vector2 cell)
    {
        var emptyCells = new Godot.Collections.Array();

        for (int x = (int)cell.x - 1; x <= cell.x + 1; x++)
        {
            for (int y = (int)cell.y - 1; y <= cell.y + 1; y++)
            {
                var coords = new Vector2(x, y);

                if (!usedCells.Contains(coords))
                {
                    emptyCells.Add(coords);
                }
            }
        }

        return emptyCells;
    }

    public void NextTurn()
    {
        usedCells = this.GetUsedCells();

        foreach (Vector2 cell in usedCells)
        {
            var emptyCells = GetEmptyNeighbourCells(cell);
            int numOfAliveCells = 8 - emptyCells.Count;

            if (numOfAliveCells < 2 || numOfAliveCells > 3)
            {
                this.SetCell((int)cell.x, (int)cell.y, -1);
            }

            foreach (Vector2 emptyCell in emptyCells)
            {
                numOfAliveCells = 9 - GetEmptyNeighbourCells(emptyCell).Count;

                if (numOfAliveCells == 3)
                {
                    this.SetCell((int)emptyCell.x, (int)emptyCell.y, 0);
                }
            }
        }
    }

    public void ClearCells()
    {
        usedCells = this.GetUsedCells();

        foreach (Vector2 cell in usedCells)
        {
            this.SetCell((int)cell.x, (int)cell.y, -1);
        }
    }
}
