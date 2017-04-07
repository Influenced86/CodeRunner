using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelSelect : TouchManager {

    public enum LevelButton { One, Two, Three, Four, Five, Six, Seven, Eight, Nine, Ten };
    public LevelButton levelButton;

    public GUITexture levelSelectButton;

    private GameObject _levelLayoutObject;
    private LevelLayout _levelLayout;

    private static GameObject _compileObject;
    private static Compile _compile;

    private void OnFirstTouchBegan()
    {
        switch (levelButton)
        {
            case LevelButton.One:
                SceneManager.LoadScene("LevelOne");
                _levelLayout.LevelNumber = 1;
                break;
            case LevelButton.Two:
                SceneManager.LoadScene("LevelTwo");
                _levelLayout.LevelNumber = 2;
                break;
            case LevelButton.Three:
                SceneManager.LoadScene("LevelThree");
                _levelLayout.LevelNumber = 3;
                break;
            case LevelButton.Four:
                SceneManager.LoadScene("LevelFour");
                _levelLayout.LevelNumber = 4;
                break;
            case LevelButton.Five:
                SceneManager.LoadScene("LevelFive");
                _levelLayout.LevelNumber = 5;
                break;
            case LevelButton.Six:
                SceneManager.LoadScene("LevelSix");
                _levelLayout.LevelNumber = 6;
                break;
            case LevelButton.Seven:
                SceneManager.LoadScene("LevelSeven");
                _levelLayout.LevelNumber = 7;
                break;
            //case LevelButton.Eight:
            //    SceneManager.LoadScene("LevelEight");
            //    _levelLayout.LevelNumber = 8;
            //    break;
            //case LevelButton.Nine:
            //    SceneManager.LoadScene("LevelNine");
            //    _levelLayout.LevelNumber = 9;
            //    break;
            //case LevelButton.Ten:
            //    SceneManager.LoadScene("LevelTen");
            //    _levelLayout.LevelNumber = 10;
            //    break;


        }
    }

    // Use this for initialization
    void Start () {
        _levelLayoutObject = GameObject.Find("BackTiles");
        _levelLayout = _levelLayoutObject.GetComponent<LevelLayout>();

        _compileObject = GameObject.Find("Compile");
        _compile = _compileObject.GetComponent<Compile>();

        
    }
	
	// Update is called once per frame
	void Update () {
        // Only allow player to change level is they are not compiling (causes movement issue otherwise)
        if (!_compile.GetIsCompile())
        {
            TouchInput(levelSelectButton);
        }




    }
}
