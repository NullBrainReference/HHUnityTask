using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cell : MonoBehaviour
{
    [SerializeField] private bool isOcupied;
    [SerializeField] private bool isPocket;
    [SerializeField] private int hierarhyPos;

    public int HierarhyPos { get { return this.hierarhyPos; } }
    public bool IsOcupied { get {return this.isOcupied; } }
    public bool IsPocket { get { return this.isPocket; } }

    public Cell()
    {
        isOcupied = false;
        isPocket = false;
    }
    public void SetHierarhyPos(int index)
    {
        hierarhyPos = index;
    }
    public Cell(int hierarhyPos)
    {
        this.hierarhyPos = hierarhyPos;
    }
    public void PlaceBox(Box box)
    {
        box.transform.parent = this.transform;
        isOcupied = true;
    }
    public void SetOcupied()
    {
        isOcupied = true;
    }
    public void BoxLeft()
    {
        isOcupied = false;
    }
    //public void SetBasedOnCell(Cell cell)
    //{
    //    this.isOcupied = cell.isOcupied;
    //    this.isPocket = cell.isPocket;
    //    this.SetHierarhyPos(cell.hierarhyPos);
    //}
}
