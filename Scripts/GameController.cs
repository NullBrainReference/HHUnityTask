using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameController : MonoBehaviour
{
    [SerializeField] private GameObject cellsParent;
    [SerializeField] private GameObject cellPrefab;
    [SerializeField] private GameObject boxPrefab;
    [SerializeField] private GameObject canvas;

    [SerializeField] public AudioVolumeController audioVolumeController;
    [SerializeField] private GameSetupMaster setupMaster;
    [SerializeField] private List<Cell> otherCellConteiners;
    private List<Cell> cells;

    [SerializeField] private GameObject startButton;
    [SerializeField] private GameObject continueButton;
    [SerializeField] private GameObject gameGroup;
    [SerializeField] private GameObject retryScreen;
    [SerializeField] private TextMeshProUGUI resultText;

    public static GameController Instace { get; private set; }

    private void Awake()
    {
        Instace = this;
    }

    private void Start()
    {
        cells = new List<Cell> { };
        SetCellMatrix(setupMaster.GetCellsCount());
        SetOtherConteiners();
    }
    public void StartGame()
    {
        PutBoxesByMasterValues();
        SaveGame();
    }
    public void SetCellMatrix(int count)
    {
        for (int i = 0; i < count; i++) 
        {
            GameObject newCell = Instantiate(cellPrefab, cellsParent.transform);
            Cell cell = newCell.GetComponent<Cell>();
            cell.SetHierarhyPos(i);
            cells.Add(cell);
        }
    }
    
    public void SetOtherConteiners()
    {
        for (int i = 0; i < otherCellConteiners.Count; i++)
        {
            otherCellConteiners[i].SetHierarhyPos(cells.Count + i);
        }
    }
    public Cell GetClosestCell(Transform boxTransform, Cell defaultCell)
    {
        Cell closestCell = defaultCell;
        foreach(Cell cell in cells)
        {
            if(Vector2.Distance(boxTransform.position ,cell.transform.position) <
                Vector2.Distance(boxTransform.position, closestCell.transform.position))
            {
                closestCell = cell;
            }
        }
        foreach (Cell cell in otherCellConteiners)
        {
            if (Vector2.Distance(boxTransform.position, cell.transform.position) <
                Vector2.Distance(boxTransform.position, closestCell.transform.position))
            {
                closestCell = cell;
            }
        }

        if (closestCell.IsOcupied) 
            return defaultCell;

        return closestCell;
    }
    public void PutBoxesByMasterValues()
    {
        foreach(Cell cell in cells)
        {
            if(setupMaster.IsCellFilled(cell.HierarhyPos))
            {
                GameObject newBox = Instantiate(boxPrefab, cell.transform);
                Box box = newBox.GetComponent<Box>();
                box.Init(canvas.transform, cell);
                cell.PlaceBox(box);
            }
        }
        foreach (Cell cell in otherCellConteiners)
        {
            if (setupMaster.IsCellFilled(cell.HierarhyPos))
            {
                GameObject newBox = Instantiate(boxPrefab, cell.transform);
                Box box = newBox.GetComponent<Box>();
                box.Init(canvas.transform, cell);
                cell.PlaceBox(box);
            }
        }
    }
    public void PutBoxesByOcupation()
    {
        foreach (Cell cell in cells)
        {
            if (cell.IsOcupied)
            {
                GameObject newBox = Instantiate(boxPrefab, cell.transform);
                Box box = newBox.GetComponent<Box>();
                box.Init(canvas.transform, cell);
                cell.PlaceBox(box);
            }
        }
        foreach (Cell cell in otherCellConteiners)
        {
            if (cell.IsOcupied)
            {
                GameObject newBox = Instantiate(boxPrefab, cell.transform);
                Box box = newBox.GetComponent<Box>();
                box.Init(canvas.transform, cell);
                cell.PlaceBox(box);
            }
        }
    }
    public void CheckWinCondition(Cell cell)
    {
        if (setupMaster.IsWinCell(cell.HierarhyPos))
        {
            ShowWinScreen();
        }
        else
        {
            ShowRetryScreen();
        }
    }
    private void ShowWinScreen()
    {
        retryScreen.SetActive(true);
        resultText.text = "онаедю";
    }
    private void ShowRetryScreen()
    {
        retryScreen.SetActive(true);
        resultText.text = "ньхайю";
    }
    public void ResetGame()
    {
        foreach(Cell cell in cells)
        {
            if (cell.IsOcupied)
            {
                Destroy(cell.transform.GetComponentInChildren<Box>().transform.gameObject);
                cell.BoxLeft();
            }
        }
        foreach (Cell cell in otherCellConteiners)
        {
            if (cell.IsOcupied)
            {
                Destroy(cell.transform.GetComponentInChildren<Box>().transform.gameObject);
                cell.BoxLeft();
            }
        }
        PutBoxesByMasterValues();
    }
    public List<Cell> GetCells()
    {
        return cells;
    }
    public List<Cell> GetPockets()
    {
        return otherCellConteiners;
    }
    public void ContinueGame()
    {
        UserData userData = UserData.GetData();

        continueButton.SetActive(false);
        startButton.SetActive(false);
        gameGroup.SetActive(true);

        if (userData == null) return;

        for (int i = 0; i < cells.Count; i++)
        {
            if (userData.cells[i])
            {
                cells[i].SetOcupied();
            }
        }
        for (int i = 0; i < otherCellConteiners.Count; i++)
        {
            if(userData.pockets[i])
                otherCellConteiners[i].SetOcupied();
        }

        PutBoxesByOcupation();

        int placedCount = 0;
        foreach(Cell cell in cells)
        {
            if (cell.IsOcupied) placedCount++;
        }
        if(placedCount>2)
        {
            CheckWinCondition(cells[setupMaster.WinCellindex]);
        }
    }
    public void SaveGame()
    {
        UserData userData = new UserData();
        userData.SaveData(cells, otherCellConteiners);
    }
}
