using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelGrid : Singleton<LevelGrid>
{
    public Dictionary<Vector2Int, Cell> PassablesCells
    {
        get
        {
            Dictionary<Vector2Int, Cell> output = new Dictionary<Vector2Int, Cell>();

            foreach (var pair in _grid)
                if (pair.Value.CellType == Cell.Type.passable) output.Add(pair.Key, pair.Value);

            return output;
        }
    }
    public Dictionary<Vector2Int, Cell> Grid => _grid;

    [SerializeField] private int _rows;
    [SerializeField] private int _columns;

    [SerializeField] private Vector2 _offset;
    [SerializeField] private Vector2 _cellSize;


    private Vector2 _vectorUp = Tool.WorldUpVector;
    private Vector2 _vectorRight = Vector2.right;

    private Dictionary<Vector2Int, Cell> _grid = new Dictionary<Vector2Int, Cell>();

    public void Initalize(LevelTemplate level)
    {
        Cell.Type[,] template = level.Template;
        Vector2 bottomLeftVertex = _offset;

        for(int i = 0; i < template.GetLength(0); i++)
        {
            for(int j = 0; j < template.GetLength(1); j++)
            {
                Vector2 topRightVertex = _vectorUp * _cellSize.y + _vectorRight * _cellSize.x + bottomLeftVertex;
                Vector2 cellPosition = (topRightVertex + bottomLeftVertex) / 2;

                Cell cell = new Cell(cellPosition, _vectorRight, _vectorUp, _cellSize, template[i, j]);
                _grid.Add(new Vector2Int(i, j), cell);

                bottomLeftVertex += _vectorRight * _cellSize.x;

            }
            bottomLeftVertex = _offset + _vectorUp * (i + 1) * _cellSize.y;
        }
    }

    public List<Cell> GetPassableNeiboured(Cell target)
    {
        List<Cell> result = new List<Cell>();

        Vector2Int targetCellPos = _grid.FindValue(target);

        Vector2Int[] dirs = { new Vector2Int(-1, 0), new Vector2Int(1, 0), new Vector2Int(0, 1), new Vector2Int(0, -1) };

        foreach (var dir in dirs)
        {
            if(_grid.TryGetValue(dir + targetCellPos, out Cell cell) == true && cell.CellType == Cell.Type.passable)
            {
                result.Add(cell);
            }
        }
        return result;
    }

    public Cell[] RandomPath(int lenght, Cell startCell)
    {
        List<Cell> result = new List<Cell>();

        Cell prevCell = null;
        Cell currentCell = startCell;

        while (result.Count < lenght)
        {
            List<Cell> passableCells = GetPassableNeiboured(currentCell);

            if (passableCells.Count == 0)
                throw new System.Exception("impossible path");

            Cell randomCell = passableCells[Random.Range(0, passableCells.Count)];

            if(randomCell != prevCell)
            {
                prevCell = currentCell;
                currentCell = randomCell;
                result.Add(currentCell);
            }
        }

        return result.ToArray();
    }
}

public class Cell
{
    public enum Type
    {
        passable = 0,
        impassable = 1,
    }

    public Vector2 Position => _position;
    public Type CellType => _type;

    private Vector2 _position;
    private Vector2 _size;
    private Vector2 _vectorUp;
    private Vector2 _vectorRight;

    private Type _type;

    public Cell(Vector2 position, Vector2 vectorRight, Vector2 vectorUp, Vector2 size, Type type)
    {
        _type = type;
        _size = size;
        _position = position;
        _vectorUp = vectorUp;
        _vectorRight = vectorRight;
    }
}
