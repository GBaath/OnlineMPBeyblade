using Alteruna;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : AttributesSync
{
    public static GameManager Instance { get; private set; }

    [SynchronizableField]
    public Vector2 player1Pos;
    [SynchronizableField]
    public Vector2 player2Pos;
    [SynchronizableField]
    public Vector2 player1Force;
    [SynchronizableField]
    public Vector2 player2Force;

    [SynchronizableField]
    Vector2 pushForce;


}
