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

    private static GameObject _repeatObject;
    private static RepeatCommand _repeat;

    public GameObject currentDownIcon;
    public GameObject downIcon;
    public GameObject downIcon1;
    public GameObject downIcon2;
    public GameObject downIcon3;
    public GameObject downIcon4;

    private const int       _Down = -6;
    
    // Run each time player presses corresponding button (texture)
    public void Execute(Player aPlayer)
    {
        aPlayer.AddNewPosition(_Down);
        if (!_compile.GetIsCompile())
        {
            // Spawn move icon
            Instantiate(currentDownIcon, _levelLayout.iconArray[_levelLayout.CurrentIconArrayIndex], Quaternion.identity);
            // Update array index if array isn't full
            if (_levelLayout.CurrentIconArrayIndex < 17) _levelLayout.CurrentIconArrayIndex++;
        }
    }

    private void SetupDownIcon(int repeatNum)
    {
        switch (repeatNum)
        {
            case 1:
                currentDownIcon = downIcon1;
                break;
            case 2:
                currentDownIcon = downIcon2;
                break;
            case 3:
                currentDownIcon = downIcon3;
                break;
            case 4:
                currentDownIcon = downIcon4;
                break;
            case 0:
                currentDownIcon = downIcon;
                break;
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

        _repeatObject = GameObject.Find("Repeat");
        _repeat = _repeatObject.GetComponent<RepeatCommand>();
    }
	
	// Update is called once per frame
	void Update ()
    {
        SetupButtonTexture(_repeat.RepeatValue);
        SetupDownIcon(_repeat.RepeatValue);
        TouchInput(currentButtonTexture);


	}
}
