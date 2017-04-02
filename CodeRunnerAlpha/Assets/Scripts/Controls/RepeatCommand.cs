using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RepeatCommand : TouchManager, ICommand
{
    private static GameObject _playerObject;
    private static Player _player;

    private static GameObject _levelLayoutObject;
    private static LevelLayout _levelLayout;

    private static GameObject _compileObject;
    private static Compile _compile;

    private const int _RepeatCap = 5;
    private int _repeatValue = 0;
    public int RepeatValue
    {
        get { return _repeatValue; }
        set { _repeatValue = value; }
    }
    // Textures for each numbered repeat button
    public GUITexture repeatButtonTexture0;
    public GUITexture repeatButtonTexture1;
    public GUITexture repeatButtonTexture2;
    public GUITexture repeatButtonTexture3;
    public GUITexture repeatButtonTexture4;

    // Sets the sprite of the repeat button depending on how many times its been touched
    private void SetRepeatTexture()
    {
        switch (_repeatValue)
        {
            case 1:
                buttonTexture.texture = repeatButtonTexture1.texture;
                break;
            case 2:
                buttonTexture.texture = repeatButtonTexture2.texture;
                break;
            case 3:
                buttonTexture.texture = repeatButtonTexture3.texture;
                break;
            case 4:
                buttonTexture.texture = repeatButtonTexture4.texture;
                break;
            case 0:
                buttonTexture.texture = repeatButtonTexture0.texture;
                break;
        }
    }

    // Run each time player presses corresponding button (texture)
    public void Execute(Player aPlayer)
    {      
        if (!_compile.GetIsCompile())
        {
            _repeatValue++;
            if(_repeatValue == _RepeatCap)
            {
                _repeatValue = 0;
            }
            //Changes the repeat button texture to match the amount of repeats the player wishes to use
            SetRepeatTexture();
            Debug.Log("Repeat value: " + _repeatValue);
            
        }
    }

    private void OnFirstTouchBegan()
    {
        Debug.Log("Repeat touched");
        Execute(_player);
    }
    private void OnFirstTouchStay() { }
    private void OnFirstTouchEnd() { }

    void Start()
    {
        _playerObject = GameObject.Find("Player");
        _player = _playerObject.GetComponent<Player>();

        _levelLayoutObject = GameObject.Find("BackTiles");
        _levelLayout = _levelLayoutObject.GetComponent<LevelLayout>();

        _compileObject = GameObject.Find("Compile");
        _compile = _compileObject.GetComponent<Compile>();
    }

    // Update is called once per frame
    void Update () {
        TouchInput(buttonTexture);
        SetRepeatTexture();
	}
}
