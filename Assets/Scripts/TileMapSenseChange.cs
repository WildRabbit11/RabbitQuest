using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TileMapSenseChange : MonoBehaviour
{
    private LevelManager LevelManager;
    //private Animator animator;
    private Tilemap tileMap;

    // Start is called before the first frame update
    void Start()
    {
        LevelManager = FindObjectOfType<LevelManager>();
        tileMap = GetComponent<Tilemap>();
        //animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        //animator.SetBool("SensesOn", LevelManager.sensesOn);

        if (LevelManager.sensesOn == true)
        {
            tileMap.color = new Color(0.25f, 0.25f, 0.25f, 1);
        }
        else
        {
            tileMap.color = new Color(1, 1, 1, 1);
        }
    }
}
