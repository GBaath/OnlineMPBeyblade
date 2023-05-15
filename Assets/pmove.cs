using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Alteruna;
using Unity.VisualScripting;

public class pmove : MonoBehaviour
{
    const string horizontal = "Horizontal";
    const string vertical = "Vertical";


    private Alteruna.Avatar avatar;


    private void Start()
    {
        avatar = GetComponent<Alteruna.Avatar>();
    }

    void Update()
    {
        if (!avatar.IsMe)
            return;


        if (Input.GetButtonDown(horizontal))
        {
            Vector2 move = Input.GetAxisRaw(horizontal) * Vector2.right;
            transform.position += (Vector3)move;
        }
        if (Input.GetButtonDown(vertical))
        {
            Vector2 move = Input.GetAxisRaw(vertical) * Vector2.up;
            transform.position += (Vector3)move;
        }
    }
}
