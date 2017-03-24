using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeftCommand : TouchManager, ICommand
{
    private GameObject playerObject;
    private Player player;

    private const int _Left = -1;

    public void Execute(Player aPlayer)
    {
        aPlayer.AddNewPosition(_Left);
    }

    private void OnFirstTouchBegan()
    {
        //Execute(player);
    }
    private void OnFirstTouchStay() { }
    private void OnFirstTouchEnd() { }

    // Use this for initialization
    void Start () {
        playerObject = GameObject.Find("Player");
        player = playerObject.GetComponent<Player>();
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
