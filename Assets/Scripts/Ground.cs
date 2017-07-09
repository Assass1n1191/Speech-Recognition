using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ground : MonoBehaviour {

    public int myIndexInGroundPool;

	private void Start () 
	{
		
	}

    private void OnBecameInvisible()
    {
        try
        {
            RoadController.Instance.MoveGorundPart(myIndexInGroundPool);
        }
        catch (Exception e)
        {
            //Debug.Log(e.Message);
        }
    }
}
