using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Security;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class PlayerMove : MonoBehaviour
{
    private Rigidbody2D rb;
    public InputController ic;
    public LayerMask targetLayerMask;
    public LayerMask groundLayer;
    public Animator myanim;
    private bool onGround = false;
    public static int health = 20;
     public Text Health;
    public GameObject deathmessage;
    public float deadtime;
    public bool isAttack;



        

    private void Awake() {
        deathmessage.SetActive(false);
        rb = GetComponent<Rigidbody2D>();
        myanim = GetComponent<Animator>();
    }

    private void OnEnable() {
        ic.inputJson.Basic.Jump.performed += Jump;
        ic.inputJson.Basic.Interact.performed += Interact;
    }

    private void OnDisable() {
        ic.inputJson.Basic.Jump.performed -= Jump;
        ic.inputJson.Basic.Interact.performed -= Interact;
    }

    private void Interact(InputAction.CallbackContext context)
    {
        Collider2D[] results = new Collider2D[10];
        Physics2D.OverlapCircle(transform.position, 1.5f, new ContactFilter2D{useLayerMask=true, useTriggers=true, layerMask=targetLayerMask}, results);
        if(results[0]!=null) 
        {
            onGround = true;
            results[0].GetComponent<ShowContent>()?.ShowIt();
        }
        else{
            onGround = false;
        }
    }
     private void CheckGrounded() {
        Vector2 origin = (Vector2)transform.position;
        RaycastHit2D hit = Physics2D.Raycast(origin, Vector2.down , 1f, groundLayer);
        Debug.DrawRay(origin, Vector2.down * 1f, Color.red);
        if (hit.collider != null)
        {
            myanim.SetBool("jump",false);
            onGround = true;
            jumptime = 0;
        }
        else
        {
            onGround = false;
        }

    }
    static int jumptime = 0;
    private void Jump(InputAction.CallbackContext context)
    {
        if(onGround || jumptime <1)
        {
            myanim.SetBool("jump",true);
            rb.velocity = new Vector2(rb.velocity.x,10);
            onGround = false;
            jumptime++;
        }  
    }

    void Flip()
    {
         bool playSpeed = Mathf.Abs(rb.velocity.x) > Mathf.Epsilon;
         if(rb.velocity.x > 0.1f)
         {
            transform.localRotation = Quaternion.Euler(0,0,0);
         }
         if(rb.velocity.x < -0.1f)
         {
            transform.localRotation = Quaternion.Euler(0,180,0);
         }
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.CompareTag("enemy") && !isAttack)
        {
            health = health -4;
            Health.text = "Health:" + health.ToString();
            if(health <= 0)
            {
                myanim.SetTrigger("dead");
                Invoke("ShowDeath",deadtime);
            }
        }
        if(other.gameObject.CompareTag("Fall"))
        {
            Health.text = "Health:" + health.ToString();
            myanim.SetTrigger("dead");
            Invoke("ShowDeath",deadtime);
        }
    }
    void ShowDeath()
    {
        deathmessage.SetActive(true);
        Time.timeScale = 0;
    }

    private void Update() 
    {
        Vector2 dir = ic.inputJson.Basic.Move.ReadValue<Vector2>();
        rb.velocity = new Vector2(5*dir.x, rb.velocity.y);
        Flip();
        bool playSpeed = Mathf.Abs(rb.velocity.x) > Mathf.Epsilon;
        myanim.SetBool("run",playSpeed);
        CheckGrounded();
    }




}
