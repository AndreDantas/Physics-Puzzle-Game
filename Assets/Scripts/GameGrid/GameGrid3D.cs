using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using Sirenix.OdinInspector;
public class GameGrid3D : GameGrid
{

    public Material cellMaterial;

    protected override GridCell PrepareCell(int i, int j)
    {
        var obj = GameObject.CreatePrimitive(PrimitiveType.Cube);
        obj.name = "3D Cell (" + i + "," + j + ")";
        obj.transform.localScale = new Vector3(cellSize, cellSize, cellSize);
        obj.transform.parent = cellsParent.transform;

        var col = obj.GetOrAddComponent<BoxCollider>();
        col.isTrigger = true;
        obj.transform.position = new Vector3(gridValues.min.x + i * cellSize + cellSize / 2f, gridValues.min.y + j * cellSize + cellSize / 2f) + (Vector3)gridCenter;

        var mesh = obj.GetOrAddComponent<MeshRenderer>();
        mesh.material = cellMaterial;

        return obj.GetOrAddComponent<GridCell>();

    }
}
