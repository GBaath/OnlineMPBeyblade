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


    public Alteruna.Avatar avatar;

    GameManager gm;

    public float speed = 1;
    private float moveX, moveY;

    public float pushMultiplier =1;
    
    private Vector2 move;
    private Vector2 automove;
    private Vector2 spawnpoint;

    private Rigidbody2D rb;
    private SpriteRenderer sr;

    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        gm = GameManager.Instance;
        rb = GetComponent<Rigidbody2D>();
        avatar = GetComponent<Alteruna.Avatar>();
        spawnpoint = transform.position;

        SetColor();

        gm.players[avatar.Possessor.Index] = this;
       
    }

    void Update()
    {
        if (!avatar.IsMe)
            return;


        moveY = Input.GetAxis(VERTICAL);
        moveX = Input.GetAxis(HORIZONTAL);

        move = moveX * Vector3.right + moveY * Vector3.up;



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
        switch (avatar.Possessor.Index)
        {
            case 0:
                sr.color = Color.blue;
                break;

            case 1:
                sr.color = Color.red;
                break;

            case 2:
                sr.color = Color.yellow;
                break;

            case 3:
                sr.color = Color.green;
                break;

            default:
                break;
        }
    }
    public void Collide(int otherindex)
    {
        rb.AddForce((gm.players[otherindex].transform.position - transform.position).normalized*10*pushMultiplier*-1 , ForceMode2D.Impulse);
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
