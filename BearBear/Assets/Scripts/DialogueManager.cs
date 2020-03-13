using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DialogueManager : MonoBehaviour
{
    
    public Queue<string> sentences;
    public GameObject dialogBox;
    public bool endChat;
    public bool typing;
    string sentence;
    bool skip;

    private void Start()
    {
        sentences = new Queue<string>();
    }

    public void startDialogue(Dialogue dialogue)
    {
        endChat = false;
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

        dialogBox.GetComponent<UnityEngine.UI.Text>().text = "BYE";
        FindObjectOfType<KeyboardInputManager>().enableCharacterMovement();
    }
}