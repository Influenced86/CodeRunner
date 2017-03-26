using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DownCommand : TouchManager, ICommand
{
    private GameObject playerObject;
    private Player player;

    private const int _Down = -6;

    public void Execute(Player aPlayer)
    {
        aPlayer.AddNewPosition(_Down);
    }

    private void OnFirstTouchBegan()
    {
        Debug.Log("Down touched");
        Execute(player);
    }
    private void OnFirstTouchStay() { }
    private void OnFirstTouchEnd() { }

    void Start ()
    {
        playerObject = GameObject.Find("Player");
        player = playerObject.GetComponent<Player>();
    }
	
	// Update is called once per frame
	void Update ()
    {
        TouchInput(buttonTexture);
	}
}
