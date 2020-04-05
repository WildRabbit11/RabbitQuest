using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainRabbitController : MonoBehaviour
{
    private float timeBtwAttack;
    [HideInInspector] public bool isAttacking;
    public Transform attackPos; //objects being attacked
    public float attackRange;
    public LayerMask whatIsEnemy; //using layermasks allows us to ignore colliders we dont want to interact with
    public LayerMask whatIsDiggable;
    public float startTimeBtwAttack;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (timeBtwAttack <= 0)
        {
            if (Input.GetMouseButtonDown(0) && Input.GetKey(KeyCode.S) == false)
            {
                isAttacking = true;
                //StartCoroutine("coRoutineAttack");
                //view https://www.youtube.com/watch?v=1QfxdUpVh5I
                //Collider2D[] enemiesToDamage = Physics2D.OverlapCircleAll(attackPos.position, attackRange, whatIsEnemy);
                //for (int i = 0; i < enemiesToDamage.Length; i++)
                //{
                //    enemiesToDamage[i].GetComponent<Enemy>().TakeDamage(damage);
                //}
                Collider2D[] objectsToDig = Physics2D.OverlapCircleAll(attackPos.position, attackRange, whatIsDiggable);
                for (int i = 0; i < objectsToDig.Length; i++)
                {
                    objectsToDig[i].GetComponent<DiggingGround>().Dug();
                }

                timeBtwAttack = startTimeBtwAttack;
            }
        }
        else
        {
            timeBtwAttack -= Time.deltaTime;
        }

    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(attackPos.position, attackRange);
        //Gizmos.color = Color.blue;
        //Gizmos.DrawWireSphere(groundCheckPoint.position, groundCheckRadius);
    }
}
