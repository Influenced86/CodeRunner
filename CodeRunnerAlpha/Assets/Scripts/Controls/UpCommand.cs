using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpCommand : TouchManager, ICommand
{
    private GameObject playerObject;
    private Player player;
    private static bool _upEnabled = true;

    

    private const int _Up = 6;

    public void Execute(Player aPlayer)
    {
       
        aPlayer.AddNewPosition(_Up);
        
    }

    private void OnFirstTouchBegan()
    {
        Debug.Log("Up touched!");
        Execute(player);
        
    }
    private void OnFirstTouchStay() { }
    private void OnFirstTouchEnd()  { }

    // Use this for initialization
    void Start () {
        playerObject = GameObject.Find("Player");
        player = playerObject.GetComponent<Player>();

        
    }
	
	// Update is called once per frame
	void Update () {
        TouchInput(buttonTexture);
	}
}
