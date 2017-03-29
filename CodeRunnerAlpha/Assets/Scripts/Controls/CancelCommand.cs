using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CancelCommand : TouchManager, ICommand
{
    private static GameObject      _playerObject;
    private static Player          _player;

    private static GameObject      _levelLayoutObject;
    private static LevelLayout     _levelLayout;

    // Run each time player presses corresponding button (texture)
    public void Execute(Player aPlayer)
    { 
        aPlayer.Cancel();
        // Remove all direction icons from the screen
        var clones = GameObject.FindGameObjectsWithTag("Clone");
        _levelLayout.CurrentIconArrayIndex = 0;
        foreach(var clone in clones)
        {
            Destroy(clone);  
        } 
    }

    private void OnFirstTouchBegan()
    {
        Debug.Log("Cancel touched!");
        Execute(_player);

    }
    private void OnFirstTouchStay() { }
    private void OnFirstTouchEnd() { }

    // Use this for initialization
    void Start () {
        _playerObject = GameObject.Find("Player");
        _player = _playerObject.GetComponent<Player>();

        _levelLayoutObject = GameObject.Find("BackTiles");
        _levelLayout = _levelLayoutObject.GetComponent<LevelLayout>();
    }
	
	// Update is called once per frame
	void Update () {
        TouchInput(buttonTexture);
	}
}
