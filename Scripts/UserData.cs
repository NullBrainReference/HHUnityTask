using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class UserData
{
    public float volume;
    public List<bool> cells;
    public List<bool> pockets;

    public void SaveData(List<Cell> cells, List<Cell> pockets)
    {
        this.cells = new List<bool> { };
        this.pockets = new List<bool> { };

        foreach(Cell cell in cells)
        {
            this.cells.Add(cell.IsOcupied);
        }
        foreach (Cell cell in pockets)
        {
            this.pockets.Add(cell.IsOcupied);
        }

        this.volume = GameController.Instace.audioVolumeController.GetVolumeValue();
        string userString = JsonUtility.ToJson(this);

        PlayerPrefs.SetString("UserData", userString);
    }
    public static UserData GetData()
    {
        string userString = PlayerPrefs.GetString("UserData");
        UserData userData = JsonUtility.FromJson<UserData>(userString);
        return userData;
    }
}
