using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetButton : MonoBehaviour
{
    [SerializeField]private GameObject retryScreen;

    public void ResetGame()
    {
        retryScreen.SetActive(false);
        GameController.Instace.ResetGame();
    }
}
