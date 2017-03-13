using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Compile : MonoBehaviour {

    ArrayList moveList = new ArrayList();
   

    private void AddMoveLocation(Transform pos)
    {
        
        moveList.Add(pos);

    }

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
