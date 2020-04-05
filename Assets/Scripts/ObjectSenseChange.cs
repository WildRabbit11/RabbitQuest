using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectSenseChange : MonoBehaviour
{
    private LevelManager LevelManager;
    //private Animator animator;
    private SpriteRenderer spriteRend;

    // Start is called before the first frame update
    void Start()
    {
        LevelManager = FindObjectOfType<LevelManager>();
        spriteRend = GetComponent<SpriteRenderer>();
        //animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        //animator.SetBool("SensesOn", LevelManager.sensesOn);

        if (LevelManager.sensesOn == true)
        {
            spriteRend.color = new Color(0.25f, 0.25f, 0.25f, 1);
        }
        else
        {
            spriteRend.color = new Color(1, 1, 1, 1);
        }
    }
}
