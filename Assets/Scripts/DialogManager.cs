using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogManager : MonoBehaviour {

    private Queue<string> content;
    public Text sentenceText;
    public Animator animator;
    public CharManager charManager;
    public PlayerCtrl playerCtrl;

    void Start() {
        content = new Queue<string>();
    }

    public void StartChating(Dialog dialog, CharManager charManager){
        this.charManager = charManager;
        animator.SetBool("isOpen",true);
        playerCtrl.canMove = false;
        playerCtrl.freezeMoving(true);
        content.Clear();

        foreach (string sentence in dialog.content){
            content.Enqueue(sentence);
        }
        NextSentence();
    }

    public bool NextSentence(){
        if(content.Count == 0){
            EndChating();
            return true;
        }

        string sentence = content.Dequeue();
        sentenceText.text = sentence;
        return false;
    }

    public void EndChating(){
        charManager.isChating = false;
        charManager.isChated = true;
        playerCtrl.canMove = true;
        playerCtrl.freezeMoving(false);
        animator.SetBool("isOpen",false);
        charManager.BehaviourAfterChating();
    }
}
