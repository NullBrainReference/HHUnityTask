using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundButton : MonoBehaviour
{
    [SerializeField] private GameObject volumePanel;
    public void SwitchOnClick()
    {
        volumePanel.SetActive(!volumePanel.activeInHierarchy);
    }
}
