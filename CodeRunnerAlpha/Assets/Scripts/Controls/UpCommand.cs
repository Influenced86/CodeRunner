using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpCommand : TouchManager, ICommand
{
    private GameObject      _playerObject;
    private Player          _player;
    
    private GameObject      _levelLayoutObject;
    private LevelLayout     _levelLayout;

    private GameObject      _compileObject;
    private Compile         _compile;

    public GameObject       upIcon;

    private const int       _Up = 6;

    // Run each time player presses corresponding button (texture)
    public void Execute(Player aPlayer)
    {
        aPlayer.AddNewPosition(_Up);
        if (!_compile.GetIsCompile())
        {
            // Spawn move icon
            Instantiate(upIcon, _levelLayout.iconArray[_levelLayout.CurrentIconArrayIndex], Quaternion.identity);
            // Update array index if array isn't full
            if (_levelLayout.CurrentIconArrayIndex < 17) _levelLayout.CurrentIconArrayIndex++;
        }
    }

    private void OnFirstTouchBegan()
    {
        Debug.Log("Up touched!");
        Execute(_player);
        
    }
    private void OnFirstTouchStay() { }
    private void OnFirstTouchEnd()  { }
 
    void Start () {
        _playerObject = GameObject.Find("Player");
        _player = _playerObject.GetComponent<Player>();

        _levelLayoutObject = GameObject.Find("BackTiles");
        _levelLayout = _levelLayoutObject.GetComponent<LevelLayout>();

        _compileObject = GameObject.Find("Compile");
        _compile = _compileObject.GetComponent<Compile>();
    }
	
	void Update () {
        TouchInput(buttonTexture);
	}
}
