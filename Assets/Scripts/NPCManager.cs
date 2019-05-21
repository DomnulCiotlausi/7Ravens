using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class NPCManager : CharManager{

    private bool ok = true;
    DialogManager dm;

    void Start(){
        dm = FindObjectOfType<DialogManager>();
    } 

    void OnTriggerEnter2D(Collider2D col){

　　	if (col.gameObject.tag == "Player" && !isChated) {
            talk.text = "Press E to talk";
　　	}
　　}


    void OnTriggerStay2D(Collider2D col){
　　	if (col.gameObject.tag == "Player") {
            if(Input.GetKeyDown (KeyCode.E)){
                if(!isChated&&!isChating){
                    isChating= true;
                    dm.StartChating(dialog,this);
                }else if(!isChated&&isChating){
                    dm.NextSentence();
                }         
            }
　　	}
　　}

    void OnTriggerExit2D(Collider2D col){
　　	if (col.gameObject.tag == "Player") {
            talk.text = "";
　　	}
　　}

    public override void BehaviourAfterChating(){
        if(gameObject.name == "father"){
            playerCtrl.haveRing = true;
            talk.text = "Use Left Mouse or J to shoot. \n You have limited ammo, use it wisely.";
        }
    }

}
