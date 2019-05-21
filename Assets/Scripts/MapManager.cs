using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapManager : MonoBehaviour
{

	public int saved = 0;
	public int killed = 0;
	public GameObject wall;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
     	if (saved + killed == 6){
     		Destroy(wall);
     	}
     	if (saved + killed == 7){
     		if (saved == 7){
     			Application.LoadLevel("GoodEnding");
     		} else {
     			Application.LoadLevel("BadEnding");
     		}
     	}
    }

    public void addSaved(int x){
    	saved += x;
    }

    public void addKilled(int x){
    	killed += x;
    }
}
