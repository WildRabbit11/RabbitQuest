using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public bool sensesOn;
    private MainRabbitControllerV2 player;

    // Start is called before the first frame update
    void Start()
    {
        sensesOn = false;
        player = FindObjectOfType<MainRabbitControllerV2>();
    }

    // Update is called once per frame
    void Update()
    {
        if (sensesOn == false && Input.GetKeyDown(KeyCode.E) == true)
        {
            sensesOn = true;
        }
        else if (sensesOn == true && Input.GetKeyDown(KeyCode.E) == true)
        {
            sensesOn = false;
        }
    }
}
