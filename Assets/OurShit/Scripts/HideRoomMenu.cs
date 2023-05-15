using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HideRoomMenu : MonoBehaviour
{
    public GameObject roomPanel;


    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.P))
        {
            ToggleRoomPanel();
        }
    }

    public void ToggleRoomPanel()
    {
        if (roomPanel.activeSelf)
            roomPanel.SetActive(false);
        else
            roomPanel.SetActive(true);
    }

}
