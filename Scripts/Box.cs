using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Box : MonoBehaviour, IDragHandler, IEndDragHandler, IBeginDragHandler
{
    [SerializeField] private Transform draggingParent;
    [SerializeField] private Transform originalParent;
    [SerializeField] private Cell currentCell;

    public void Init(Transform draggingParent, Cell currentCell)
    {
        this.draggingParent = draggingParent;
        this.originalParent = currentCell.transform.parent;
        this.currentCell = currentCell;
    }
    public void OnBeginDrag(PointerEventData eventData)
    {
        if (!currentCell.IsPocket) return;
        transform.parent = draggingParent;
        currentCell.BoxLeft();
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (!currentCell.IsPocket) return;
        transform.position = Input.mousePosition;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if (!currentCell.IsPocket) return;
        Cell pasteCell = GameController.Instace.GetClosestCell(this.transform, currentCell);
        pasteCell.PlaceBox(this);
        currentCell = pasteCell;

        if (!pasteCell.IsPocket)
        {
            GameController.Instace.CheckWinCondition(pasteCell);
        }

        GameController.Instace.SaveGame();
    }
}
