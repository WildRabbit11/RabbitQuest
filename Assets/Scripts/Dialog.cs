using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Dialog : MonoBehaviour
{
    public TextMeshProUGUI textDisplay;
    public string[] sentences;
    private int index;
    public float typingSpeed;

    public GameObject continueButton;

    private bool hasStarted = false;
    [HideInInspector] public bool startTyping = false;

    // Start is called before the first frame update
    //void Start()
    //{
        
    //}

    // Update is called once per frame
    void Update()
    {
        if(hasStarted == false && startTyping == true)
        {
            hasStarted = true;
            StartCoroutine(Type());
        }

        if(textDisplay.text == sentences[index])
        {
            continueButton.SetActive(true);
        }
    }

    public void NextSentence()
    {
        continueButton.SetActive(false);

        if (index < sentences.Length - 1)
        {
            index++;
            textDisplay.text = "";
            StartCoroutine(Type());
        }
        else
            textDisplay.text = "";
    }

    IEnumerator Type()
    {
        foreach (char letter in sentences[index].ToCharArray())
        {
            textDisplay.text += letter;
            yield return new WaitForSeconds(typingSpeed);
        }
    }
}
