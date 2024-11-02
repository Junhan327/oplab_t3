using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    public float time;
    private Animator myanim;
    private PolygonCollider2D mycol;
    public GameObject deathani;
    public PlayerMove player;
    
    void Start()
    {
        myanim = GameObject.FindGameObjectWithTag("Player").GetComponent<Animator>();
        mycol = GetComponent<PolygonCollider2D>();
    }
    void Attack()
    {
         if (Input.GetKeyDown(KeyCode.J))
        {
            player.isAttack =true;
            mycol.enabled = true;
            myanim.SetTrigger("Attack");
            StartCoroutine(DisableHixbox());
        }
    }
    IEnumerator DisableHixbox()
    {
        yield return new WaitForSeconds(time);
        mycol.enabled = false;
        myanim.SetTrigger("StopAttack");
        player.isAttack = false;
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.CompareTag("enemy"))
        {
            Instantiate(deathani,other.transform.position,Quaternion.identity);
            Destroy(other.gameObject);
        }
    }
    void Update()
    {
        Attack();
    }
}
