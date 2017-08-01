using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Letter : MonoBehaviour
{
    public string letterName;

	private void Start ()
	{
	    GetComponent<SpriteRenderer>().enabled = true;
	}
	
	private void Update () 
	{
		
	}
}
