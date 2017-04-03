using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RightCommand : TouchManager, ICommand
{
    private static GameObject       _playerObject;
    private static Player           _player;

    private static GameObject       _levelLayoutObject;
    private static LevelLayout      _levelLayout;

    private static GameObject       _compileObject;
    private static Compile          _compile;

    private static GameObject _repeatObject;
    private static RepeatCommand _repeat;

    public GameObject       currentRightIcon;
    public GameObject       rightIcon;
    public GameObject       rightIcon1;
    public GameObject       rightIcon2;
    public GameObject       rightIcon3;
    public GameObject       rightIcon4;
   
    private const int       _Right = 1;

    // Run each time player presses corresponding button (texture)
    public void Execute(Player aPlayer)
    {
        aPlayer.AddNewPosition(_Right);
        if (!_compile.GetIsCompile())
        {
            // Spawn move icon
            Instantiate(currentRightIcon, _levelLayout.iconArray[_levelLayout.CurrentIconArrayIndex], Quaternion.identity);
            // Update array index if array isn't full
            if (_levelLayout.CurrentIconArrayIndex < 17) _levelLayout.CurrentIconArrayIndex++;
        }
    }

    private void SetupRightIcon(int repeatNum)
    {
        switch (repeatNum)
        {
            case 1:
                currentRightIcon = rightIcon1;
                break;
            case 2:
                currentRightIcon = rightIcon2;
                break;
            case 3:
                currentRightIcon = rightIcon3;
                break;
            case 4:
                currentRightIcon = rightIcon4;
                break;
            case 0:
                currentRightIcon = rightIcon;
                break;
        }
    }

    private void OnFirstTouchBegan()
    {
        Debug.Log("Right touched");
        Execute(_player);
    }
    private void OnFirstTouchStay() { }
    private void OnFirstTouchEnd() { }

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
	
	// Update is called once per frame
	void Update () {
        SetupButtonTexture(_repeat.RepeatValue);
        SetupRightIcon(_repeat.RepeatValue);
        TouchInput(currentButtonTexture);
    }
}
