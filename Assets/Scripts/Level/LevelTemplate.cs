using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelTemplate
{
    public static LevelTemplate DefaultTemplate
    {
        get
        {
            int rows = 9;
            int columns = 17;

            Cell.Type[,] output = new Cell.Type[rows, columns];

            for(int i = 0; i < rows; i++)
            {
                for(int j = 0; j < columns; j++)
                {
                    if (i % 2 == 1 && j % 2 == 1)
                        output[i, j] = Cell.Type.impassable;
                    else
                        output[i, j] = Cell.Type.passable;
                }
            }

            return new LevelTemplate(output);
        }
    }

    public Cell.Type[,] Template => _template;

    private Cell.Type[,] _template;

    public LevelTemplate(Cell.Type[,] template)
    {
        _template = template;
    }
}
