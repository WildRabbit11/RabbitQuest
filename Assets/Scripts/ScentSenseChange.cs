using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScentSenseChange : MonoBehaviour
{
    private LevelManager LevelManager;
    private SpriteRenderer spriteRend;

    // Start is called before the first frame update
    void Start()
    {
        LevelManager = FindObjectOfType<LevelManager>();
        spriteRend = GetComponent<SpriteRenderer>();
        StartCoroutine("coRoutineSelfDestruct");
    }

    // Update is called once per frame
    void Update()
    {
        if (LevelManager.sensesOn == true)
        {
            spriteRend.enabled = true;
        }
        else
        {
            spriteRend.enabled = false;
        }
    }

    IEnumerator coRoutineSelfDestruct()
    {
        yield return new WaitForSeconds(15f);
        Destroy(gameObject);
    }
}
