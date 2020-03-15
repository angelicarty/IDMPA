using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DialogueManager : MonoBehaviour
{
    
    public Queue<string> sentences;
    string talkerName;
    public GameObject dialogBox;
    public GameObject nameBox;
    public bool endChat;
    public bool typing;
    string sentence;
    bool skip;
    bool respondRequired;

    private void Start()
    {
        sentences = new Queue<string>();
    }

    public void startDialogue(Dialogue dialogue)
    {
        endChat = false;
        talkerName = dialogue.talkerName;
        if(dialogue.triggerOptions)
        {
            respondRequired = true;
        }
        sentences.Clear();
        FindObjectOfType<KeyboardInputManager>().disableCharacterMovement();

        foreach (string sentence in dialogue.sentences)
        {
            sentences.Enqueue(sentence);
        }
        DisplayNextSentence();
    }

    public void DisplayNextSentence()
    {
        nameBox.GetComponent<UnityEngine.UI.Text>().text = talkerName;
        if (sentences.Count == 0)
        {
            endChat = true;
            endDialogue();
            return;
        }
        skip = false;
        typing = true;
        sentence = sentences.Dequeue();
        StartCoroutine(TypeSentence());

    }
    IEnumerator TypeSentence()
    {
        dialogBox.GetComponent<UnityEngine.UI.Text>().text = "";
        foreach (char letter in sentence.ToCharArray())
        {
            if(skip)
            {
                yield break;
            }
            dialogBox.GetComponent<UnityEngine.UI.Text>().text += letter;
            yield return new WaitForSeconds(0.02f);
        }
        typing = false;
    }

    public void skipTyping()
    {
        StopCoroutine(TypeSentence());
        skip = true;
        typing = false;
        dialogBox.GetComponent<UnityEngine.UI.Text>().text = sentence;
    }

    void endDialogue()
    {
        //dialogBox.SetActive(false);
        if(respondRequired)
        {
            Debug.Log("trigger the options box here");
        }
        dialogBox.GetComponent<UnityEngine.UI.Text>().text = "BYE"; //don't clear text if respondrequired
        FindObjectOfType<KeyboardInputManager>().enableCharacterMovement();
    }
}