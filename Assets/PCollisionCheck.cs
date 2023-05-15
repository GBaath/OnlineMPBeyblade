using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Alteruna;

public class PCollisionCheck : AttributesSync

{
    GameManager gm;
    private pmove pmove;
    private void Start()
    {
       gm = GameManager.Instance;
        pmove = GetComponentInParent<pmove>();
    }
    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            InvokeRemoteMethod(nameof(PlayerCollision), pmove.avatar.Possessor.Index, collision.transform.parent.GetComponent<pmove>().avatar.Possessor.Index);

            InvokeRemoteMethod(nameof(PlayerCollision), collision.transform.root.GetComponent<pmove>().avatar.Possessor.Index, pmove.avatar.Possessor.Index);

        }
    }


    [SynchronizableMethod]
    public void PlayerCollision(int indexOther)
    {
        pmove.Collide(indexOther);
    }
}
