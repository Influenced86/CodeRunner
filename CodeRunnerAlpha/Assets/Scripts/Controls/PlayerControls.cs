using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;


// - Takes care of all the movement controls for the player. Sets up
// when it's elible for the player to press any button. If a button
// is successfully pressed, a specific bool for that button is set to
// true and is then dealt with inside the LevelLayout class - //

public class PlayerControls : MonoBehaviour {


    private GameObject _compileObject;
    private Compile _compile;

    private GameObject _playerObject;
    private Player _player;

    //// - VARIABLES -------------------------- ///// ----------
    // --------------------------------------------
    // - TEXTURES - // ----------------------------
    // - Button texture is set inside unity - //
     // Texture for this specific button
    public GUITexture upButtonTexture;
    public GUITexture downButtonTexture;
    public GUITexture rightButtonTexture;
    public GUITexture leftButtonTexture;
    public GUITexture repeatButtonTexture;

    // - Enable or disable the button controls - //
    private static bool _isForwardEnabled = true;
    private static bool _isRightEnabled = false;
    private static bool _isLeftEnabled = false;
    private static bool _isBackwardEnabled = false;
    private static bool _isRepeatEnabled = true;

    private GameObject levelLayoutObject;
    private LevelLayout levelLayout;

    public bool IsRightEnabled
    {
        get { return _isRightEnabled; }
        set { _isRightEnabled = value; }
    }
    public bool IsLeftEnabled
    {
        get { return _isLeftEnabled; }
        set { _isLeftEnabled = value; }
    }
    public bool IsBackwardEnabled
    {
        get { return _isBackwardEnabled; }
        set { _isBackwardEnabled = value; }
    }
    public bool IsRepeatEnabled
    {
        get { return _isRepeatEnabled; }
        set { _isRepeatEnabled = value; }
    }


   

    // - TOUCH MESSAGES - // - Implements the messages received from 
    // TouchManager - //
    

    private void OnFirstTouchStay() { 
          
    }

    private void OnFirstTouchEnd() {
        
    }


    // Checks whether the buttons have been unlocked by the player. 
    // Bools are static and are created within LevelLayout
    public void ButtonActiveCheck()
    {
        
        upButtonTexture.gameObject.SetActive(_isForwardEnabled);
        rightButtonTexture.gameObject.SetActive(_isRightEnabled);
        leftButtonTexture.gameObject.SetActive(_isLeftEnabled);
        downButtonTexture.gameObject.SetActive(_isBackwardEnabled);
        repeatButtonTexture.gameObject.SetActive(_isRepeatEnabled);
             
    }

   



    void Start () {
        
        levelLayoutObject = GameObject.Find("BackTiles");
        levelLayout = levelLayoutObject.GetComponent<LevelLayout>();

        _compileObject = GameObject.Find("Compile");
        _compile = _compileObject.GetComponent<Compile>();

        _playerObject = GameObject.Find("Player");
        _player = _playerObject.GetComponent<Player>();

    }

    void Update () {
        
        ButtonActiveCheck();
       
        
    }
}

