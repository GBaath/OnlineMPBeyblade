using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Alteruna;
using UnityEngine.UIElements;

public class pmove : AttributesSync
{
    const string HORIZONTAL = "Horizontal";
    const string VERTICAL = "Vertical";


    public Alteruna.Avatar avatar;

    public GameObject ClashParticles;

    GameManager gm;

    private bool canIncrease = true;

    public float speed = 1;
    private float moveX, moveY;

    public float pushMultiplier = 1;

    private Vector2 move;
    private Vector2 automove;
    private Vector2 spawnpoint;

    private Animator anim;
    private Rigidbody2D rBody;
    private SpriteRenderer spriteRend;
    public Spinning spinnRef;
    public Spinning spinnSubRef;
    private Collider2D collision;

    private bool isDead = false;

    void Start()
    {
        avatar = GetComponent<Alteruna.Avatar>();
        spriteRend = GetComponentInChildren<SpriteRenderer>();
        rBody = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        collision = GetComponentInChildren<Collider2D>();

        gm = GameManager.Instance;
        spawnpoint = transform.position;

        gm.players[avatar.Possessor.Index] = this;

        SetColor();

    }

    void Update()
    {
        if (!avatar.IsMe)
            return;

        if (!isDead)
        {
            moveY = Input.GetAxis(VERTICAL);
            moveX = Input.GetAxis(HORIZONTAL);

            move = moveX * Vector3.right + moveY * Vector3.up;

            if (move == Vector2.zero)
            {
                spinnRef.moveSpinnModifier = 1;
                spinnSubRef.moveSpinnModifier = 1;
            }
            else
            {
                spinnRef.moveSpinnModifier = 1.2f;
                spinnSubRef.moveSpinnModifier = 1.2f;
            }
        }
        else
        {
            spinnRef.moveSpinnModifier = 0f;
            spinnSubRef.moveSpinnModifier = 0f;
        }

        if(Input.GetKeyDown(KeyCode.R))
        {
            GameManager.Instance.RestartGame();
        }

    }

    private void FixedUpdate()
    {
        if (!avatar.IsMe)
            return;

        if (!isDead)
            rBody.AddForce(move * speed + automove);
    }



    private void SetColor()
    {
        switch (avatar.Possessor.Index)
        {
            case 0:
                spriteRend.color = Color.blue;
                break;

            case 1:
                spriteRend.color = Color.red;
                break;

            case 2:
                spriteRend.color = Color.yellow;
                break;

            case 3:
                spriteRend.color = Color.green;
                break;

            default:
                break;
        }
    }
    public void Collide(Vector3 otherObject, float additionalForce)
    {
        rBody.velocity = Vector2.zero;
        Vector2 CollisionDir = (otherObject - transform.position).normalized;

        rBody.AddForce(CollisionDir * additionalForce * pushMultiplier * -1, ForceMode2D.Impulse);
        IncreaseSpin();

        Instantiate(ClashParticles, (otherObject + transform.position) / 2, Quaternion.FromToRotation(Vector2.up, CollisionDir));

    }
    //public void Collide(Vector3 otherObject)
    //{
    //    rBody.velocity = Vector2.zero;
    //    rBody.AddForce((otherObject - transform.position).normalized * 3 * pushMultiplier * -1, ForceMode2D.Impulse);
    //    IncreaseSpin();
    //}


    void IncreaseSpin()
    {
        if (!canIncrease)
            return;

        pushMultiplier += .3f;
        spinnRef.spinnSpeed += 1;
        canIncrease = false;
        Invoke(nameof(SpeedTimer), .2f);
    }
    void SpeedTimer()
    {
        canIncrease = true;
    }
    public void Death()
    {
        isDead = true;
        anim.enabled = true;
        anim.SetBool("IsDead?", true);

        collision.enabled = false;
        rBody.drag = 3;
        
        transform.rotation = Quaternion.Euler(0, 0, Random.Range(0,360));

        pushMultiplier = 1;
        spinnRef.spinnSpeed = 10;     
    }


    [SynchronizableMethod]
    public void Restart()
    {
        anim.SetBool("IsDead?", false);
        Invoke(nameof(DisableAnimator), 0.1f);
        isDead = false;

        rBody.velocity = Vector2.zero;
        collision.enabled = true;
        rBody.drag = 1;

        transform.rotation = Quaternion.Euler(0, 0, 0);

        pushMultiplier = 1;
        spinnRef.spinnSpeed = 10;

        transform.position = spawnpoint;

        GameManager.Instance.EnableStartPanel();
    }

    public void DisableAnimator()
    {
        anim.enabled = false;
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
