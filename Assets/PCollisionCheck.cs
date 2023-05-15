using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Alteruna;

public class PCollisionCheck : AttributesSync

{
    GameManager gm = GameManager.Instance;
    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            InvokeRemoteMethod(nameof(PlayerCollision),UserId.AllInclusive,);
        }
    }


    [SynchronizableMethod]
    public void PlayerCollision(Vector2 otherPos, Vector2 selfPos, Vector2 otherForce, Vector2 selfForce, Vector2 angleBetween)
    {

    }
}
