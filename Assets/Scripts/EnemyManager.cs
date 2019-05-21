using UnityEngine;
using System.Collections;
　　
public class EnemyManager : MonoBehaviour {

    public GameManager gameManager;
    float startPos;
    float endPos;

    public int unitsToMove = 5;
    public int moveSpeed = 2;
    bool moveRight = true;

    int enemyHP = 1;
    int damage = 1;

    public bool enemy1;
    public bool enemy2;

    Rigidbody2D rb2d;
    SpriteRenderer sr;

    void Start(){
　　	startPos = transform.position.x;
　　	endPos = startPos + unitsToMove;
        rb2d = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
        if(enemy1){
            enemyHP = 2;
            damage = 1;
        }else if(enemy2){
            enemyHP = 5;
            damage =2;
        }
　　}　　	

　　	
　　void FixedUpdate(){

        //enemy moving
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
    }
　　
　　void OnTriggerEnter2D(Collider2D col){
　　	if (col.gameObject.tag == "Player"&&!PlayerCtrl.isTakingDamage) {
            gameManager.SendMessage("PlayerDamaged",damage,SendMessageOptions.DontRequireReceiver);
            gameManager.player.SendMessage("TakenDamage",SendMessageOptions.DontRequireReceiver);
　　	     }
　　}

    void EnemyDamaged(int damage){
　　	if (enemyHP > 0) {
　　		enemyHP -= damage;		
　　    }
　　
    　　	if (enemyHP <= 0) {
    　　		enemyHP = 0;
    　　		Destroy(gameObject);
    　　	}
　　}
}
