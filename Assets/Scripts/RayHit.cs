using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RayHit : MonoBehaviour{

    int damage = 1;

    void OnTriggerEnter2D(Collider2D col){
        //hit enemy
        if (col.gameObject.tag == "Enemy") {
            col.gameObject.SendMessage("EnemyDamaged",damage,SendMessageOptions.DontRequireReceiver);
    　　} 

        Destroy(gameObject);
    }

    void Update(){
        Destroy (gameObject, 3.0f);
　　}
}
