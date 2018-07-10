using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using Sirenix.OdinInspector;
public class GridPlacement : MonoBehaviour
{

    public GameGrid grid;
    public List<DragAndDrop> objectsToPlace = new List<DragAndDrop>();


    private void Start()
    {
        grid.BuildGrid();
        foreach (var item in objectsToPlace)
        {

            item.OnDrop += OnDrop;
        }
    }

    public void OnDrop(Vector3 v, GameObject sender)
    {
        if (!grid)
            return;
        if (grid.ValidPosition(v))
        {
            var cell = grid.GetCell(v);
            if (cell != null)
            {

                sender.transform.position = cell.transform.position;
            }
        }
    }
}
