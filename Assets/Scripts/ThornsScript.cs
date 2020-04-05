using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThornsScript : MonoBehaviour
{
    private MainRabbitControllerV2 player;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<MainRabbitControllerV2>();
    }

    //// Update is called once per frame
    //void Update()
    //{

    //}

    void OnTriggerEnter2D(Collider2D col)
    {   //col is a variable name of the Collision2D datatype
        if (col.gameObject.tag == "Player")
        {
            Debug.Log("Entered");
            player.TakenDamage();
        }

    }
}
