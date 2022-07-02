using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;


public class DialogueManager2 : MonoBehaviour
{
    public static DialogueManager2 instance;
    [SerializeField] Text nameText;
    [SerializeField] Text dialogueText;
    [SerializeField] Animator animator;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Debug.Log("Removing the copy of DialogueManager2 instance");
            Destroy(this);
        }
    }

    private Queue<string> sentences;

    // Start is called before the first frame update
    void Start()
    {
        sentences = new Queue<string>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void StartDialogue(Dialogue dialogue)
    {
        animator.SetBool("isOpen", true); // open animate

        Debug.Log("Starting conversation with " + dialogue.name);
        
        nameText.text = dialogue.name; // update name ui



        sentences.Clear();

        foreach(string sentence in dialogue.sentences)
        {
            sentences.Enqueue(sentence);
        }

        DisplayNextSentence();
    }

    public void DisplayNextSentence()
    {
        // check if theres more sentences in queue
        
        if (sentences.Count == 0) // no sentences left
        {
            EndDialogue();

            return;
        }

        string sentence = sentences.Dequeue();
        StopAllCoroutines();

        StartCoroutine(TypeSentence(sentence));

        // update dialogue ui
       // dialogueText.text = sentence;
        Debug.Log(sentence);
    }

    private void EndDialogue()
    {
        Debug.Log("End of Conversation");
        animator.SetBool("isOpen", false);

    }

    IEnumerator TypeSentence(string sentence)
    {
        dialogueText.text = "";
        
        foreach (char letter in sentence.ToCharArray())
        {
            yield return new WaitForSeconds(0.1f);
            dialogueText.text += letter;
        }
    }

}
