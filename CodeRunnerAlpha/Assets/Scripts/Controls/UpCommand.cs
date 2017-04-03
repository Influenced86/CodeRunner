using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpCommand : TouchManager, ICommand
{
    private static GameObject   _playerObject;
    private static Player       _player;

    private static GameObject   _levelLayoutObject;
    private static LevelLayout  _levelLayout;

    private static GameObject   _compileObject;
    private static Compile      _compile;

    private static GameObject       _repeatObject;
    private static RepeatCommand    _repeat;
 
    public GameObject       currentIcon;
    public GameObject       upIcon;
    public GameObject       upIcon1;
    public GameObject       upIcon2;
    public GameObject       upIcon3;
    public GameObject       upIcon4;


    private const int       _Up = 6;

    // Run each time player presses corresponding button (texture)
    public void Execute(Player aPlayer)
    {
        aPlayer.AddNewPosition(_Up);
        if (!_compile.GetIsCompile())
        {
            // Spawn move icon
            Instantiate(currentIcon, _levelLayout.iconArray[_levelLayout.CurrentIconArrayIndex], Quaternion.identity);
            // Update array index if array isn't full
            if (_levelLayout.CurrentIconArrayIndex < 17) _levelLayout.CurrentIconArrayIndex++;
        }
    }

    private void SetupUpIcon(int repeatNum)
    {
        switch (repeatNum)
        {
            case 1:
                currentIcon = upIcon1;
                break;
            case 2:
                currentIcon = upIcon2;
                break;
            case 3:
                currentIcon = upIcon3;
                break;
            case 4:
                currentIcon = upIcon4;
                break;
            case 0:
                currentIcon = upIcon;
                break;
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

        _repeatObject = GameObject.Find("Repeat");
        _repeat = _repeatObject.GetComponent<RepeatCommand>();

        
    }
	
	void Update () {
        SetupButtonTexture(_repeat.RepeatValue);
        SetupUpIcon(_repeat.RepeatValue);
        TouchInput(currentButtonTexture);
        
	}
}
