using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spinning : MonoBehaviour
{

    private float spinnSpeed = 10;
    private Vector3 spinDir = new(0, 0, -1);


    private void Update()
    {
        transform.Rotate(spinDir * 40 * spinnSpeed * Time.deltaTime);
    }



}
