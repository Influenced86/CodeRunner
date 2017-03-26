using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;


// - Takes care of all the movement controls for the player. Sets up
// when it's elible for the player to press any button. If a button
// is successfully pressed, a specific bool for that button is set to
// true and is then dealt with inside the LevelLayout class - //

public class PlayerControls : MonoBehaviour {

    private static int _repeat;

    //// - VARIABLES -------------------------- ///// ----------
    // --------------------------------------------
    // - TEXTURES - // ----------------------------
    // - Button texture is set inside unity - //
     // Texture for this specific button
    public GUITexture upButtonTexture;
    public GUITexture downButtonTexture;
    public GUITexture rightButtonTexture;
    public GUITexture leftButtonTexture;
    
    // - Enable or disable the button controls - //
    private static bool _isForwardEnabled = true;
    private static bool _isRightEnabled = false;
    private static bool _isLeftEnabled = false;
    private static bool _isBackwardEnabled = false;
    private static bool _isRepeatEnabled = false;

    private GameObject theLayout;
    private LevelLayout levelLayout;

    // -------------------------------------------

    //public GUITexture repeatButtonTexture = null;
    //public GUITexture repeatButtonTexture0 = null;
    //public GUITexture repeatButtonTexture1 = null;
    //public GUITexture repeatButtonTexture2 = null;
    //public GUITexture repeatButtonTexture3 = null;
    //public GUITexture repeatButtonTexture4 = null;

    // -------------------------------------------


    //// ------------------------------------------------------

    // - GETTERS - SETTERS - PROPERTIES - // ------
    //public int GetRepeat()
    //{
    //    return _repeat;
    //}
    //public void DecrementRepeat()
    //{
    //    _repeat -= 1;
    //}
    //public void ResetRepeat()
    //{
    //    _repeat = -1;
    //}

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


    //// - METHODS --------------------------------///////------------
    // - SETUP BUTTONS - Connects the correct textures attached in Unity to the
    // corresponding texture variables. Used in ButtonActiveCheck() to
    // enable/disable them - //
    //private void SetupButtons() { 
    //    switch(buttonType)
    //    {
    //        case ButtonTypes.Forward:
    //            upButtonTexture = this.GetComponent<GUITexture>();
    //            break;
    //        case ButtonTypes.Backward:
    //            downButtonTexture = this.GetComponent<GUITexture>();
    //            break;
    //        case ButtonTypes.Left:
    //            leftButtonTexture = this.GetComponent<GUITexture>();
    //            break;
    //        case ButtonTypes.Right:
    //            rightButtonTexture = this.GetComponent<GUITexture>();
    //            break;
    //        case ButtonTypes.Repeat:
    //            //repeatButtonTexture = this.GetComponent<GUITexture>();
    //            break;
            
    //    }
    //}

    // - TOUCH MESSAGES - // - Implements the messages received from 
    // TouchManager - //
    

    private void OnFirstTouchStay() { 
          
    }

    private void OnFirstTouchEnd() {
        
    }

    // Changes the repeat button texture to match the amount of repeats the player wishes to use
    //public void SetRepeatButtonTexture()
    //{
    //    switch (_repeat)
    //    {  
    //        case 1:
    //            repeatButtonTexture.texture = repeatButtonTexture1.texture;
    //            break;
    //        case 2:
    //            repeatButtonTexture.texture = repeatButtonTexture2.texture;
    //            break;
    //        case 3:
    //            repeatButtonTexture.texture = repeatButtonTexture3.texture;
    //            break;
    //        case 4:
    //            repeatButtonTexture.texture = repeatButtonTexture4.texture;
    //            break;
    //    }
    //}

    //---------------------------------------------------------------------------

    // - BUTTON ACTIVE CHECK - Checks whether the buttons have been unlocked by the player. 
    // Bools are static and are created within LevelLayout - //
    public void ButtonActiveCheck()
    {
        upButtonTexture.gameObject.SetActive(_isForwardEnabled);
        rightButtonTexture.gameObject.SetActive(_isRightEnabled);
        leftButtonTexture.gameObject.SetActive(_isLeftEnabled);
        downButtonTexture.gameObject.SetActive(_isBackwardEnabled);
        //repeatButtonTexture.gameObject.SetActive(_isRepeatEnabled);
    }

   

    //public bool IsRightTouched
    //{
    //    get { return isRightTouched; }
    //    set { isRightTouched = value; }
    //}
    //public bool IsLeftTouched
    //{
    //    get { return isLeftTouched; }
    //    set { isLeftTouched = value; }
    //}
    //public bool IsBackwardTouched
    //{
    //    get { return isBackwardTouched; }
    //    set { isBackwardTouched = value; }
    //}
    //public bool IsForwardTouched
    //{
    //    get { return isForwardTouched; }
    //    set { isForwardTouched = value; }
    //}
    //public bool IsRepeatTouched
    //{
    //    get { return isRepeatTouched; }
    //    set { isRepeatTouched = value; }
    //}

    //// ------------------------------------------------------

    void Start () {
        //SetupButtons();
        //ButtonActiveCheck();
        theLayout = GameObject.Find("BackTiles");
        levelLayout = theLayout.GetComponent<LevelLayout>();

    }

    void Update () {
        // Resets repeat texture to 0
        //if(_repeat == 0 || _repeat == -1)
        //{
        //    repeatButtonTexture.texture = repeatButtonTexture0.texture;
        //}
        //TouchInput(buttonTexture);
        ButtonActiveCheck();
        //SetRepeatButtonTexture();
        
    }
}

