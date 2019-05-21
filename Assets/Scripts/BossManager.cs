using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class BossManager : CharManager
{
    
    GameManager gm;
    Rigidbody2D rb2d;
    SpriteRenderer sr;
    DialogManager dm;

    //boss stats
    public int unitsToMove = 5;
    public int moveSpeed = 2;
    bool moveRight = true;
    public int enemyHP = 5;
    int damage = 1;
    float startPos;
    float endPos;
    
    public float cycle;
    public GameObject player;
    public Rigidbody2D bulletPrefab;
    public float minDist = 20f;
    public Sprite botherSprite;
    public MapManager mapManager;

    bool isBeated = false;

    public bool isSaved = false;
    public bool isKilled = false;
    public bool isChosen = false;
    public bool canTalkTo = true;
    private bool firstTalked = false;
    public int howMuchAmmo = 10;

    public Dialog saveDialog;
    public Dialog killDialog;
    public Dialog choseDialog;
    
    // Start is called before the first frame update
    void Start()
    {
        startPos = transform.position.x;
　　		endPos = startPos + unitsToMove;
        rb2d = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
        dm = FindObjectOfType<DialogManager>();
        gm = FindObjectOfType<GameManager>();
    }

    // Update is called once per frame
    void Update(){
    	cycle -= Time.deltaTime;
        
    }


    void FixedUpdate()
    {
        if (!isBeated){
            if (moveRight) {
            rb2d.position += Vector2.right * moveSpeed * Time.deltaTime;
            sr.flipX = true;	
            } else {
                rb2d.position -= Vector2.right * moveSpeed * Time.deltaTime;
                sr.flipX = false;	
            }

            if (rb2d.position.x >= endPos) {
                moveRight = false;
            } else if (rb2d.position.x <= startPos) {
    　　	    moveRight = true;
            }

            if (cycle <= 0 && Vector2.Distance(player.transform.position, transform.position) < minDist){
                cycle = 3;

                Rigidbody2D bPrefab;
                if (player.transform.position.x < transform.position.x) {
                    bPrefab = Instantiate (bulletPrefab, transform.position + new Vector3(-0.9f, -0.4f, 0f), Quaternion.identity) as Rigidbody2D;	
                } else {
                    bPrefab = Instantiate (bulletPrefab, transform.position + new Vector3(0.9f, -0.4f, 0f), Quaternion.identity) as Rigidbody2D;
                }
                Vector2 direction = player.transform.position - transform.position;
                bPrefab.AddForce (direction * 50);
            }
        }
    	
        
    }

    void OnTriggerEnter2D(Collider2D col){
　　	if (col.gameObject.tag == "Player"){
            if(!PlayerCtrl.isTakingDamage&&!isBeated) {
                gm.SendMessage("PlayerDamaged",damage,SendMessageOptions.DontRequireReceiver);
                gm.player.SendMessage("TakenDamage",SendMessageOptions.DontRequireReceiver);
            }else if(isBeated){
            	Debug.Log(canTalkTo);
                if (canTalkTo){
                	if (firstTalked){
                			talk.text = "Press 1 to save \nPress 2 to kill (+10 ammo)";
                		} else {
                			talk.text = "Press E to talk";
                		}
                }
            }
　　	}
　　}

    void OnTriggerStay2D(Collider2D col){
　　	if (col.gameObject.tag == "Player"&&isBeated) {

            //first chating
            if (canTalkTo){
                if(!isChosen){
                	isChosen = Chat(dialog);
        			firstTalked = true;
                }else{
                    if(Input.GetKeyDown (KeyCode.Alpha1)){
                        isSaved = true;
                    }else if(Input.GetKeyDown (KeyCode.Alpha2)){
                        isKilled = true;
                    }
                }
                if(isSaved){
                    talk.text = "";
                    if(!isChating){
                        isChating = true;
                        dm.StartChating(saveDialog,this);    
                        sr.sprite = botherSprite;    
                    }else{
                        if(Input.GetKeyDown (KeyCode.E)){
                            canTalkTo = !dm.NextSentence();
                        }
                        if(!canTalkTo){
                        	mapManager.addSaved(1);
                        }
                    }    
                }else if(isKilled){
                    talk.text = "";
                    playerCtrl.ammo += howMuchAmmo;
                    if(!isChating){
                        isChating = true;
                        dm.StartChating(killDialog,this);        
                    }else{
                        if(Input.GetKeyDown (KeyCode.E)){
                            canTalkTo = !dm.NextSentence();
                        }
                        if (!canTalkTo){
                        	mapManager.addKilled(1);
                        	Destroy(gameObject);
                        }
                    }
                }
            }
　　	}
　　}

    void OnTriggerExit2D(Collider2D col){
　　	if (col.gameObject.tag == "Player"&&isBeated) {
            talk.text = "";
　　	}
　　}

	void EnemyDamaged(int damage){
　　	if (enemyHP > 0) {
		    Debug.Log(enemyHP);
　　			enemyHP -= damage;		
　　    }
　　
    　　if (enemyHP <= 0) {
    　　	enemyHP = 0;
    　　	isBeated = true;
    　　}
　　}
    public override void BehaviourAfterChating(){
        if(gameObject.tag == "Enemy"&&!isChosen){
            talk.text = "Press 1 to save \nPress 2 to kill (+10 ammo)";
        }
        if (gameObject.tag == "Enemy" && !isKilled){
        }
    }

    public bool Chat (Dialog d){
        if(Input.GetKeyDown (KeyCode.E)){
            if(!isChating){
                isChating = true;
                dm.StartChating(d,this);   
                return false;       
            }else{
                return dm.NextSentence();
            }    
        }   
        return false; 
    }
}
