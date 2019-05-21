using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerCtrl : PhysicsObject {

    public float maxSpeed = 7;
    public float jumpTakeOffSpeed = 10;

    public Rigidbody2D bulletPrefab;
    public GameManager gameManager;
    private SpriteRenderer spriteRenderer;
    
    float takenDamage = 0.2f;
    float attackRate = 0.5f;
    float coolDown;
    private int damage = 1;
    public bool canMove = true;
    public static bool isTakingDamage;

    public int hp;
    public bool haveRing;
    public int ammo;

    void Awake(){
        spriteRenderer = GetComponent<SpriteRenderer>(); 
        Init();
    }

    public void Init(){
        // haveRing = true;
        // ammo = 100;
        // hp = 3;
        isTakingDamage = false;
    }

    void Update(){

        if(canMove){
            Vector2 move = Vector2.zero;

            move.x = Input.GetAxis ("Horizontal");

            if ((Input.GetButtonDown ("Jump") || Input.GetKeyDown (KeyCode.W)) && grounded) {
                velocity.y = jumpTakeOffSpeed;
            }

            targetVelocity = move * maxSpeed; 
            
            //player facing
            if(move.x < -0.01f){
                if(spriteRenderer.flipX == true)
                {
                    spriteRenderer.flipX = false;
                }
            } else if (move.x > 0.01f){
                if(spriteRenderer.flipX == false)
                {
                    spriteRenderer.flipX = true;
                }
            }

            //shoot
            if (haveRing&&ammo>0&&Time.time >= coolDown) {
                if (Input.GetKeyDown (KeyCode.J)||Input.GetMouseButtonDown(0)) {
                    Shoot();	
                    ammo -= 1;
    　　        }
            }
        }      
    }

    void OnTriggerStay2D(Collider2D col){
        if ((col.gameObject.tag == "Bones" || col.gameObject.tag == "Ray") && !isTakingDamage) {
            gameManager.SendMessage("PlayerDamaged",damage,SendMessageOptions.DontRequireReceiver);
            gameManager.player.SendMessage("TakenDamage",SendMessageOptions.DontRequireReceiver);
        }
    }

    void Shoot(){
        Rigidbody2D bPrefab;
        if (!spriteRenderer.flipX) {
            bPrefab = Instantiate (bulletPrefab, transform.position + new Vector3(-1f, -0.4f, 0f), Quaternion.identity) as Rigidbody2D;   
        } else {
            bPrefab = Instantiate (bulletPrefab, transform.position + new Vector3(1f, -0.4f, 0f), Quaternion.identity) as Rigidbody2D;
        }

        Vector2 direction = spriteRenderer.flipX?Vector2.right:Vector2.left;
        bPrefab.AddForce (direction * 500);
        coolDown = Time.time + attackRate;
    }

    public void freezeMoving(bool ok){
        if(ok){
            GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezePositionX | RigidbodyConstraints2D.FreezePositionY;
        } else {
            GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.None;
            GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeRotation;
        }
    }

    public IEnumerator TakenDamage(){
        isTakingDamage = true;
        spriteRenderer.enabled = false;
        yield return new WaitForSeconds(takenDamage);
        spriteRenderer.enabled = true;
        yield return new WaitForSeconds(takenDamage);
        spriteRenderer.enabled = false;
    　　yield return new WaitForSeconds(takenDamage);
        spriteRenderer.enabled = true;
    　　yield return new WaitForSeconds(takenDamage);
        spriteRenderer.enabled = false;
    　　yield return new WaitForSeconds(takenDamage);
        spriteRenderer.enabled = true;
    　　yield return new WaitForSeconds(takenDamage);
        isTakingDamage = false;
　　} 　　 
}
