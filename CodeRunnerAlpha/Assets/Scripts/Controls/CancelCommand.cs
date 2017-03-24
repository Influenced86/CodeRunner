using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CancelCommand : TouchManager, ICommand
{
    private GameObject playerObject;
    private Player player;

    public void Execute(Player aPlayer)
    {
        aPlayer.Cancel();
    }

    private void OnFirstTouchBegan()
    {
        Debug.Log("Cancel touched!");
        Execute(player);

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
        TouchInput(buttonTexture);
	}
}
