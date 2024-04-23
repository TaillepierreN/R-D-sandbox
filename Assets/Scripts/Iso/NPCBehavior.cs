using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class NPCBehavior : MonoBehaviour
{
    [SerializeField] GameObject dialogueBox;
    [SerializeField] TextMeshProUGUI dialogueText;
    [SerializeField] GameObject GM;
    private GameObject canvas;
    private int currentLine = 0;
    private string[] dialogueLines =
    {
        "This is a dialogue",
        "The dialogue has advanced",
        "Now i'm done talking"
    };

    private void Awake()
    {
        canvas = gameObject.transform.Find("Canvas").gameObject;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name == "Player")
        {
            if (canvas != null)
            {
                canvas.SetActive(true);
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.name == "Player")
        {
            if (canvas != null)
            {
                canvas.SetActive(false);
                dialogueBox.SetActive(false);
            }
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.name == "Player")
        {
            if (Input.GetKeyUp(KeyCode.F) || Input.GetKeyUp(KeyCode.Return))
            {
                if (!dialogueBox.activeInHierarchy)
                    dialogueBox.SetActive(true);
                if (currentLine < dialogueLines.Length)
                {
                    Debug.Log("Key Pressed: " + Time.time);
                    dialogueText.text = dialogueLines[currentLine];
                    currentLine++;
                }
                else
                {
                    dialogueBox.SetActive(false);
                    currentLine = 0;
                    other.GetComponent<PlayerFade>().DissolvePlayer();
                    StartCoroutine(CallNextScene());
                }
            }
        }
    }

    private IEnumerator CallNextScene(){
        yield return new WaitForSeconds(3);
        GM.GetComponent<SceneSwitch>().SwitchScene(2);
    }
}
