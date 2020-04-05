using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HawkGFX : MonoBehaviour
{
    private Animator animator;
    private HawkAI hawkAI;

    void Awake()
    {
        animator = GetComponent<Animator>();
        hawkAI = gameObject.GetComponentInParent<HawkAI>();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        animator.SetBool("Flying", hawkAI.flying);
    }
}
