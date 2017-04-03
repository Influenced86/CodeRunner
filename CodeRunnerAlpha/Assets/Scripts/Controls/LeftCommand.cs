using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeftCommand : TouchManager, ICommand
{
    private static GameObject   _playerObject;
    private static Player       _player;

    private static GameObject   _levelLayoutObject;
    private static LevelLayout  _levelLayout;

    private static GameObject   _compileObject;
    private static Compile      _compile;

    private static GameObject _repeatObject;
    private static RepeatCommand _repeat;

    public GameObject currentLeftIcon;
    public GameObject leftIcon;
    public GameObject leftIcon1;
    public GameObject leftIcon2;
    public GameObject leftIcon3;
    public GameObject leftIcon4;

    private const int       _Left = -1;

    // Run each time player presses corresponding button (texture)
    public void Execute(Player aPlayer)
    {
        aPlayer.AddNewPosition(_Left);
        if (!_compile.GetIsCompile())
        {
            // Spawn move icon
            Instantiate(currentLeftIcon, _levelLayout.iconArray[_levelLayout.CurrentIconArrayIndex], Quaternion.identity);
            // Update array index if array isn't full
            if (_levelLayout.CurrentIconArrayIndex < 17) _levelLayout.CurrentIconArrayIndex++;
        }
    }

    private void SetupLeftIcon(int repeatNum)
    {
        switch (repeatNum)
        {
            case 1:
                currentLeftIcon = leftIcon1;
                break;
            case 2:
                currentLeftIcon = leftIcon2;
                break;
            case 3:
                currentLeftIcon = leftIcon3;
                break;
            case 4:
                currentLeftIcon = leftIcon4;
                break;
            case 0:
                currentLeftIcon = leftIcon;
                break;
        }
    }

    private void OnFirstTouchBegan()
    {
        Debug.Log("Left touched");
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

        _compileObject = GameObject.Find("Compile");
        _compile = _compileObject.GetComponent<Compile>();

        _repeatObject = GameObject.Find("Repeat");
        _repeat = _repeatObject.GetComponent<RepeatCommand>();
    }
	
	// Update is called once per frame
	void Update () {
        SetupButtonTexture(_repeat.RepeatValue);
        SetupLeftIcon(_repeat.RepeatValue);
        TouchInput(currentButtonTexture);
	}
}
