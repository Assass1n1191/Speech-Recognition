using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameScreen : MonoBehaviour {

	private void Start () 
	{
		
	}
	
	private void Update () 
	{
		if(Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }
	}
}
