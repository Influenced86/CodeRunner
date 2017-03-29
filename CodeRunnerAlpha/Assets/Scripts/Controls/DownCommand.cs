using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DownCommand : TouchManager, ICommand
{
    private static GameObject   _playerObject;
    private static Player       _player;

    private static GameObject   _levelLayoutObject;
    private static LevelLayout  _levelLayout;

    private static GameObject   _compileObject;
    private static Compile      _compile;

    public GameObject       downIcon;

    private const int       _Down = -6;
    
    // Run each time player presses corresponding button (texture)
    public void Execute(Player aPlayer)
    {
        aPlayer.AddNewPosition(_Down);
        if (!_compile.GetIsCompile())
        {
            // Spawn move icon
            Instantiate(downIcon, _levelLayout.iconArray[_levelLayout.CurrentIconArrayIndex], Quaternion.identity);
            // Update array index if array isn't full
            if (_levelLayout.CurrentIconArrayIndex < 17) _levelLayout.CurrentIconArrayIndex++;
        }
    }

    private void OnFirstTouchBegan()
    {
        Debug.Log("Down touched");
        Execute(_player);
    }
    private void OnFirstTouchStay() { }
    private void OnFirstTouchEnd() { }

    void Start ()
    {
        _playerObject = GameObject.Find("Player");
        _player = _playerObject.GetComponent<Player>();

        _levelLayoutObject = GameObject.Find("BackTiles");
        _levelLayout = _levelLayoutObject.GetComponent<LevelLayout>();

        _compileObject = GameObject.Find("Compile");
        _compile = _compileObject.GetComponent<Compile>();
    }
	
	// Update is called once per frame
	void Update ()
    {
        TouchInput(buttonTexture);
	}
}
