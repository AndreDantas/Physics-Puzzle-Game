using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using Sirenix.OdinInspector;
public abstract class GameGrid : MonoBehaviour
{
    public Vector2 gridCenter;
    public float cellSize = 1f;
    [ShowInInspector]
    public Bounds gridValues { get { return new Bounds(gridCenter, new Vector3(columns * cellSize, rows * cellSize)); } }
    public ProceduralMeshRenderer cellHover;
    protected GameObject gridPlane;
    protected GameObject cellsParent;

    [SerializeField, HideInInspector]
    protected int columns = 4;
    [ShowInInspector]
    public int Columns { get { return columns; } set { columns = UtilityFunctions.ClampMin(value, 1); } }
    [SerializeField, HideInInspector]
    protected int rows = 4;
    [ShowInInspector]
    public int Rows { get { return rows; } set { rows = UtilityFunctions.ClampMin(value, 1); } }

    protected GridCell[,] gridCells;

    private void Start()
    {
        BuildGrid();
    }

    private void Update()
    {
        var cell = GetCell(Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 0)));
        if (cell)
            LightCell(cell.transform.position);
    }

    [Button(ButtonSizes.Large)]
    public virtual void BuildGrid()
    {
        if (columns <= 0 || rows <= 0)
            return;

        transform.DestroyChildren(true);

        UtilityFunctions.SafeDestroy(gridPlane);
        gridPlane = new GameObject("Grid");
        gridPlane.transform.position = gridCenter;
        gridPlane.transform.SetParent(transform);
        gridPlane.transform.localScale = new Vector3(columns * cellSize, rows * cellSize);

        UtilityFunctions.SafeDestroy(cellsParent);
        cellsParent = new GameObject("Cells");
        cellsParent.transform.position = gridCenter;
        cellsParent.transform.SetParent(transform);

        gridCells = new GridCell[columns, rows];

        for (int i = 0; i < columns; i++)
        {
            for (int j = 0; j < rows; j++)
            {
                var cell = PrepareCell(i, j);
                cell.pos = new Position(i, j);
                cell.grid = this;
                gridCells[i, j] = cell;

            }
        }

    }

    protected virtual GridCell PrepareCell(int i, int j)
    {

        var obj = new GameObject();
        obj.name = "Cell (" + i + "," + j + ")";
        obj.transform.localScale = new Vector3(cellSize, cellSize);
        obj.transform.parent = cellsParent.transform;

        var col = obj.GetOrAddComponent<BoxCollider2D>();
        col.isTrigger = true;
        obj.transform.position = new Vector3(gridValues.min.x + i * cellSize + cellSize / 2f, gridValues.min.y + j * cellSize + cellSize / 2f) + (Vector3)gridCenter;

        return obj.GetOrAddComponent<GridCell>();
    }

    private void OnDrawGizmosSelected()
    {
        UtilityFunctions.GizmosDrawGrid(gridValues.min, columns, rows, cellSize);
    }

    public void LightCell(Vector3 pos)
    {
        cellHover?.RenderSquaresArea(new List<Vector3> { pos + new Vector3(0, 0, -1f) }, cellSize, cellSize);
    }

    /// <summary>
    /// Checks if the coordinate is valid in this grid.
    /// </summary>
    public bool ValidCoordinate(int x, int y)
    {
        if (gridCells == null)
            return false;
        if (x < 0 || x >= gridCells.GetLength(0))
            return false;
        if (y < 0 || y >= gridCells.GetLength(1))
            return false;

        return true;
    }

    public GridCell GetCell(int x, int y)
    {
        if (gridCells?.ValidCoordinates(x, y) ?? false)
        {
            return gridCells[x, y];
        }
        return null;
    }

    public GridCell GetCell(Vector2 position)
    {
        Vector2 deltaPos = position - gridCenter + new Vector2((Columns / 2) * cellSize, (Rows / 2) * cellSize) + new Vector2(cellSize / 2f, cellSize / 2f);
        deltaPos = deltaPos / cellSize;
        var pos = new Position(Mathf.FloorToInt(deltaPos.x), Mathf.FloorToInt(deltaPos.y));

        if (ValidCoordinate(pos.x, pos.y))
        {
            return gridCells[pos.x, pos.y];
        }
        return null;
    }

    public bool ValidPosition(Vector2 pos)
    {
        return gridValues.Contains(pos);

    }

}
