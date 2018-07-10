using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using Sirenix.OdinInspector;
public class GameGrid2D : GameGrid
{


    public Sprite cellTexture;

    protected override GridCell PrepareCell(int i, int j)
    {
        var obj = new GameObject();
        obj.name = "2D Cell (" + i + "," + j + ")";
        obj.transform.localScale = new Vector3(cellSize, cellSize);
        obj.transform.parent = cellsParent.transform;

        var col = obj.GetOrAddComponent<BoxCollider2D>();
        col.isTrigger = true;
        obj.transform.position = new Vector3(gridValues.min.x + i * cellSize + cellSize / 2f, gridValues.min.y + j * cellSize + cellSize / 2f) + (Vector3)gridCenter;

        var spr = obj.GetOrAddComponent<SpriteRenderer>();
        spr.sprite = cellTexture;

        return obj.GetOrAddComponent<GridCell>();

    }
}
