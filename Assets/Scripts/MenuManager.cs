using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuManager : MonoBehaviour
{
    public void OnStartGame(){
		Application.LoadLevel("Main");
	}
    
    public void OnQuitGame(){
		Application.Quit();
	} 
}