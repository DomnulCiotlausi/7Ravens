using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour{
    public Texture hpTexture;
    public PlayerCtrl playerCtrl;
    public GameObject player;
    public float screenPositionX = 10;
    public float screenPositionY = 10;
    
    void OnGUI(){
　　	//show hp
　　	for (int h =0; h < playerCtrl.hp; h++) {
　　		GUI.DrawTexture(new Rect(screenPositionX + (h*25),screenPositionY,25,25),hpTexture,ScaleMode.ScaleToFit,true,0);
        }
        if(playerCtrl.haveRing){
            GUI.Label(new Rect(screenPositionX, screenPositionY+30, 100, 50), "Ammo: "+playerCtrl.ammo.ToString());
        }     
　　}

    public void PlayerDamaged(int damage){ 
        if (!PlayerCtrl.isTakingDamage) {
            if (playerCtrl.hp > 0) {
                playerCtrl.hp -= damage;	
            }
　　
            if (playerCtrl.hp <= 0) {
    　          GameEnd();
            }
        }
　　}

    void GameEnd(){
        Application.LoadLevel ("End");
        playerCtrl.Init();
    }
}
