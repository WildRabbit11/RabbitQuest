using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartDialogScript : MonoBehaviour
{
    private bool dialogActive = false;
    private Dialog dialog;

    // Start is called before the first frame update
    void Start()
    {
        dialog = gameObject.GetComponentInParent<Dialog>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerStay2D(Collider2D col)
    {
        if (col.gameObject.tag == "Player")
        {
            if (Input.GetKeyDown(KeyCode.UpArrow) && dialogActive == false)
            {
                dialogActive = true;
                dialog.startTyping = true;
                Destroy(gameObject);
            }
        }
    }
}
