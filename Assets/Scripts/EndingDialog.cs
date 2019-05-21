using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EndingDialog : MonoBehaviour
{
    public Dialog dialog;
    public Animator animator;
    private Queue<string> content;
    public Text sentenceText;

    void Start(){
        content = new Queue<string>();
        StartChating(dialog);
    }

    void Update(){
        if(Input.GetKeyDown (KeyCode.E)){
            if(content.Count > 0){
                NextSentence();
            }
            else if(content.Count == 0){
                EndChating();
            }
        }
    }

    public void StartChating(Dialog dialog){
        animator.SetBool("isOpen",true);

        content.Clear();

        foreach (string sentence in dialog.content){
            content.Enqueue(sentence);
        }
        NextSentence();
    }

    public void NextSentence(){
        string sentence = content.Dequeue();
        sentenceText.text = sentence;
    }

    public void EndChating(){
        animator.SetBool("isOpen",false);
        Application.LoadLevel ("Start");
    }
}
