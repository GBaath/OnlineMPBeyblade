using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestoryTimer : MonoBehaviour
{

    public float timeToDoom = 1;
    
    private void Start()
    {
        Invoke(nameof(Doomsday), timeToDoom);
    }

    void Doomsday()
    {

        Destroy(gameObject);

    }


}
