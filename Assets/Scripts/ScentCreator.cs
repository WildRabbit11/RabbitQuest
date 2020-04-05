using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScentCreator : MonoBehaviour
{
    public GameObject scentPrefab;
    private bool startTimer = true;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(startTimer == true)
        {
            startTimer = false;
            StartCoroutine("coRoutineCreateScent");
        }
    }

    IEnumerator coRoutineCreateScent()
    {
        yield return new WaitForSeconds(5f);
        Instantiate(scentPrefab, transform.position, transform.rotation);
        startTimer = true;
    }
}
