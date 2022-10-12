using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartButton : MonoBehaviour
{
    public GameObject startButton;
    public GameObject continueButton;
    public GameObject gameGroup;
    public void StartGame()
    {
        ShowUI(true);
        startButton.SetActive(false);
        continueButton.SetActive(false);
        GameController.Instace.StartGame();
    }
    public void ShowUI(bool on)
    {
        gameGroup.SetActive(on);
    }

    
}
