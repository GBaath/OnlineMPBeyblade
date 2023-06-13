using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Alteruna;
using UnityEditor;

public class Spinning : AttributesSync
{

    public Alteruna.Avatar avatar;


    [SynchronizableField]
    public float moveSpinnModifier;
    [SynchronizableField]
    public float spinnSpeed = 10;
    [SynchronizableField]
    private float velocityX;
    [SynchronizableField]
    private float velocityY;

    private Vector3 spinDir = new(0, 0, -1);

    public float tiltAmmount = 30;
    public float tiltOffset = 0.1f;

    private Rigidbody2D rBody;



    private void Start()
    {
        avatar = GetComponentInParent<Alteruna.Avatar>();
        rBody = GetComponentInParent<Rigidbody2D>();
    }

    private void Update()
    {
        transform.Rotate(spinDir * 40 * spinnSpeed * moveSpinnModifier * Time.deltaTime);

        if (avatar.IsMe)
        {
            velocityX = rBody.velocity.x;
            velocityY = rBody.velocity.y;
        }

        Vector2 moveDir;

        if (new Vector2(velocityX, velocityY).sqrMagnitude > 1)
        {
            moveDir = new Vector2(velocityX, velocityY).normalized;
        }
        else
            moveDir = new Vector2(velocityX, velocityY);

        VisualTilting(moveDir.x, moveDir.y);

    }

    public void VisualTilting(float DirX, float DirY)
    {
        transform.localPosition = new Vector3(tiltOffset * DirX, tiltOffset * DirY, 0);
        transform.localRotation = Quaternion.Euler(tiltAmmount * DirY, -tiltAmmount * DirX, transform.localRotation.eulerAngles.z);
    }



}
