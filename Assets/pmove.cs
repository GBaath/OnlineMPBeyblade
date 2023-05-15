using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Alteruna;
using Unity.VisualScripting;
using System;

public class pmove : AttributesSync
{
    const string HORIZONTAL = "Horizontal";
    const string VERTICAL = "Vertical";


    private Alteruna.Avatar avatar;

    GameManager gm = GameManager.Instance;

    public float speed = 1;
    private float moveX, moveY;
    
    private Vector2 move;
    private Vector2 automove;
    private Vector2 spawnpoint;

    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        avatar = GetComponent<Alteruna.Avatar>();
        spawnpoint = transform.position;

        SetColor();
       
    }

    void Update()
    {
        if (!avatar.IsMe)
            return;


        moveY = Input.GetAxis(VERTICAL);
        moveX = Input.GetAxis(HORIZONTAL);

        move = moveX * Vector3.right + moveY * Vector3.up;

        SetManagerRefs(avatar.Possessor.Index);


    }
    private void FixedUpdate()
    {
        rb.AddForce(move * speed + automove);
    }

    public void Respawn()
    {
        transform.position = spawnpoint;
        rb.velocity = Vector3.zero;
    }
    private void SetColor()
    {
        if (avatar.Possessor.Index == 0)
            avatar.GetComponentInChildren<SpriteRenderer>().color = Color.blue;
        else
            avatar.GetComponentInChildren<SpriteRenderer>().color = Color.red;
    }
    private void SetManagerRefs(int index)
    {
       

        switch (index)
        {
            case 0:
                gm.player1Pos = transform.position;
                gm.player1Force = rb.velocity;
                break;

            case 1:
                gm.player2Pos = transform.position;
                gm.player2Force = rb.velocity;
                break;

            default:
                break;
        }
    }
    public void Collide()
    {
        switch (avatar.Possessor.Index)
        {
            case 0:

                break;

            case 1:
                
                break;

            default:
                break;
        }
    }




    private IEnumerator LerpAutomove(Vector2 startValue, Vector2 endVal, float duration)
    {
        float time = 0;
        while (time < duration)
        {
            automove = Vector2.Lerp(startValue, endVal, time / duration);
            time += Time.deltaTime;
            yield return null;
        }
        automove = endVal;
    }
    IEnumerator VarChange(System.Action<bool> boolVar, float cooldown, bool endValue)
    {
        yield return new WaitForSeconds(cooldown);
        boolVar(endValue);
    }
}
