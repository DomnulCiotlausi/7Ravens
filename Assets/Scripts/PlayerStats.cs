using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class PlayerStats : MonoBehaviour{

    public GameObject player;
    public TextMeshPro stats;
    private float lifeSpan = 3.0f;
    private bool ok = true;
    // Start is called before the first frame update
    void Start(){
        stats.text = "Use AD to move \n W or Space to jump";
    }


    // Update is called once per frame
    void Update(){
        //transform.position = player.transform.position + new Vector3(0,0.5f,0);

        lifeSpan -= Time.deltaTime;
        if (lifeSpan < 0 && ok){
            stats.text = "";
            ok = false;
        }
    }
}
