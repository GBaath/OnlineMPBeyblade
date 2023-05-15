using Alteruna;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : AttributesSync
{
    public static GameManager Instance { get; private set; }

    public List<pmove> players = new List<pmove>();

    private void Start()
    {
        Instance = this;
    }
}
