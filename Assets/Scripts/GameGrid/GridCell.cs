using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using Sirenix.OdinInspector;
using UnityEngine.EventSystems;
public class GridCell : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public Position pos;
    public GameGrid grid;

    [ReadOnly]
    public bool PointerOn;

    public void OnPointerEnter(PointerEventData eventData)
    {
        PointerOn = true;
        //grid?.LightCell(transform.position);
    }

    public void OnPointerExit(PointerEventData eventData)
    {

        PointerOn = false;
    }
}
