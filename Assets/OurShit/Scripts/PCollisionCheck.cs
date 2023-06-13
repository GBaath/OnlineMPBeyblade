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
            pmove.Collide(collision.transform.position, 1);
            StartCoroutine(GameManager.Instance.CamShake(0.2f, 0.1f));

        }
        else if (collision.CompareTag("Terrain"))
        {
            pmove.Collide(collision.transform.position, 3);
            StartCoroutine(GameManager.Instance.CamShake(0.2f, 0.2f ));
        }
    }
}
