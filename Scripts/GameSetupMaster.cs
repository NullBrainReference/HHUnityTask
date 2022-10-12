using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSetupMaster : MonoBehaviour
{
    [SerializeField] private int rowsCount;
    [SerializeField] private int columnsCount;
    [SerializeField] private List<int> filledCelles;
    [SerializeField] private int winCell;

    public int WinCellindex { get { return winCell; } }

    void Start()
    {
        
    }

    public bool IsCellFilled(int index)
    {
        bool isFilled = false;

        foreach(int id in filledCelles)
        {
            if (id == index)
            {
                isFilled = true;
                return isFilled;
            }
        }

        return isFilled;
    }
    public bool IsWinCell(int index)
    {
        bool isWinCell = false;
        if (winCell == index) isWinCell = true;
        return isWinCell;
    }
    public int GetCellsCount()
    {
        return columnsCount * rowsCount;
    }
    public int GetFilledCellsCount()
    {
        return filledCelles.Count;
    }
}
