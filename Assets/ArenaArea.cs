using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArenaArea : MonoBehaviour
{
    private pmove playerRef; 
   

    private void OnTriggerExit2D(Collider2D collision)
    {
        collision.transform.root.TryGetComponent<pmove>(out playerRef);

        if (!playerRef)
            return;

        playerRef.Respawn();
    }
}
