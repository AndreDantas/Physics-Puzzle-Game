using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using Sirenix.OdinInspector;
public class GameGrid : MonoBehaviour
{

    public Vector2 gridCenter;
    public float cellSize = 1f;
    [ShowInInspector]
    public Bounds gridValues { get => new Bounds(gridCenter, new Vector3(columns * cellSize, rows * cellSize)); }
    protected GameObject gridPlane;
    protected GameObject cellsParent;

    [SerializeField, HideInInspector]
    private int columns;
    [ShowInInspector]
    public int Columns { get => columns; set => columns = UtilityFunctions.ClampMin(value, 1); }
    [SerializeField, HideInInspector]
    private int rows;
    [ShowInInspector]
    public int Rows { get => rows; set => rows = UtilityFunctions.ClampMin(value, 1); }

    protected GridCell[,] gridCells;

    [Button(ButtonSizes.Large)]
    public void BuildGrid()
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

    GridCell PrepareCell(int i, int j)
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
}
