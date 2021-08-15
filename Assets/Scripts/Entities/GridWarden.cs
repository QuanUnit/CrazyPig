using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridWarden
{
    public Cell OccupiedCell => _occupiedCell;
    private Cell _occupiedCell;

    public void ChangeCell(Cell cell)
    {
        _occupiedCell = cell;
    }
}
