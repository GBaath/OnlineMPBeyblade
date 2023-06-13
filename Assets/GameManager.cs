using Alteruna;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : AttributesSync
{
    public static GameManager Instance { get; private set; }

    public List<pmove> players = new List<pmove>();
    public GameObject StartPanel;

    public GameObject CameraRef;


    private void Start()
    {
        Instance = this;
    }

    public void EnableStartPanel()
    {       
        StartPanel.SetActive(true);
        Time.timeScale = 0;
    }

    public void StartGame()
    {
        InvokeRemoteMethod(nameof(SetTimescale), UserId.AllInclusive, 1);
        StartPanel.SetActive(false);
    }

    public void RestartGame()
    {
        InvokeRemoteMethod(nameof(PlayRestartOnAll), UserId.AllInclusive);
    }

    [SynchronizableMethod]
    public void PlayRestartOnAll()
    {
        for (int i = 0; i < players.Count; i++)
        {
            if (players[i] != null)
            {
                players[i].Restart();
            }
        }
    }

    void MakeList()
    {
        FindObjectOfType<pmove>();

    }

    [SynchronizableMethod]
    public void SetTimescale(float timeScaleAmmount)
    {
        Time.timeScale = timeScaleAmmount;
        StartPanel.SetActive(false);
    }

    public IEnumerator CamShake(float duration, float magnitude)
    {
        Vector3 originalPos = new Vector3(0,0,-10);
        float elapsedTime = 0f;

        while (elapsedTime < duration)
        {
            float xOffset = Random.Range(-0.5f, 0.5f) * magnitude;
            float yOffset = Random.Range(-0.5f, 0.5f) * magnitude;

            CameraRef.transform.localPosition = new Vector3(xOffset, yOffset, originalPos.z);
            elapsedTime += Time.deltaTime;

            yield return null;
        }
        CameraRef.transform.localPosition = originalPos;
    }


}
