using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

// - Takes care of all the movement controls for the player. Sets up
// when it's elible for the player to press any button. If a button
// is successfully pressed, a specific bool for that button is set to
// true and is then dealt with inside the LevelLayout class - //

public class PlayerControls : TouchManager {



    //// - VARIABLES -------------------------- ///// ----------
    // - ENUMS - // -------------------------------
    public enum ButtonTypes { Forward, Backward, Left, Right, Repeat, Run };
    public ButtonTypes buttonType;
    // --------------------------------------------

    // - TEXTURES - // ----------------------------
    // - Button texture is set inside unity - //
    public GUITexture buttonTexture = null;         // Texture for this specific button
    public GUITexture forwardButtonTexture = null;
    public GUITexture backwardButtonTexture = null;
    public GUITexture rightButtonTexture = null;
    public GUITexture leftButtonTexture = null;
    public GUITexture repeatButtonTexture = null;
    public GUITexture runButtonTexture = null;
    // -------------------------------------------

    // - STATICS - // ------------------------------
    // - Enable or disable the button controls - //
    public static bool isForwardEnabled = true;
    public static bool isRightEnabled = false; 
    public static bool isLeftEnabled = false;
    public static bool isBackwardEnabled = false;
    public static bool isRepeatEnabled = true;
    public static bool isRunEnabled = true;

    // - Checked in LevelLayout. Used to decide 
    // whether the player is eligible to move or not - //
    public static bool isForwardTouched = false;
    public static bool isBackwardTouched = false;
    public static bool isRightTouched = false;
    public static bool isLeftTouched = false;
    public static bool isRepeatTouched = false;
    public static bool isRunTouched = false;
    // -------------------------------------------
    //// ------------------------------------------------------
    public static bool isRunForwardTouched = false;
    public static bool isRunBackwardTouched = false;
    public static bool isRunRightTouched = false;
    public static bool isRunLeftTouched = false;
    


    //// - METHODS --------------------------------///////------------
    // - SETUP BUTTONS - Connects the correct textures attached in Unity to the
    // corresponding texture variables. Used in ButtonActiveCheck() to
    // enable/disable them - //
    private void SetupButtons() { 
        switch(buttonType)
        {
            case ButtonTypes.Forward:
                forwardButtonTexture = this.GetComponent<GUITexture>();
                break;
            case ButtonTypes.Backward:
                backwardButtonTexture = this.GetComponent<GUITexture>();
                break;
            case ButtonTypes.Left:
                leftButtonTexture = this.GetComponent<GUITexture>();
                break;
            case ButtonTypes.Right:
                rightButtonTexture = this.GetComponent<GUITexture>();
                break;
            case ButtonTypes.Repeat:
                repeatButtonTexture = this.GetComponent<GUITexture>();
                break;
            case ButtonTypes.Run:
                runButtonTexture = this.GetComponent<GUITexture>();
                break;
        }
    }

    // - TOUCH MESSAGES - // - Implements the messages received from 
    // TouchManager - //
    private void OnFirstTouchBegan() {
        switch(buttonType)
        {
            case ButtonTypes.Forward:
               
                if(isRunTouched)    isRunForwardTouched = true;
                
                Debug.Log("Forward button touched!");
                // - Only allow the player to be able to move if the other buttons are not active AND
                // the next move is within the boundaries of the game world - //
                if (!isBackwardTouched && !isRightTouched && !isLeftTouched && LevelLayout.currentPositionIndex < 42 && !isRunTouched)                 
                {
                    isForwardTouched = true;
                }
                break;

            case ButtonTypes.Backward:
                Debug.Log("Backward button touched!");
                if (!isForwardTouched && !isRightTouched && !isLeftTouched && LevelLayout.currentPositionIndex > 5 && !isRunTouched)
                {
                    isBackwardTouched = true;
                }
                break;

            case ButtonTypes.Right:
                Debug.Log("Right button touched!");
                if(!isForwardTouched && !isBackwardTouched && !isLeftTouched && !isRunTouched)
                {
                    isRightTouched = true;
                }
                break;

            case ButtonTypes.Left:
                Debug.Log("Left button touched!");
                if(!isForwardTouched && !isBackwardTouched && !isRightTouched && !isRunTouched)
                {
                    isLeftTouched = true;
                }
                break;

            case ButtonTypes.Repeat:
                Debug.Log("Repeat button touched!");
                if (!isForwardTouched && !isBackwardTouched && !isRightTouched && !isLeftTouched && !isRunTouched)
                {
                    // If repeat is pressed, then set it to zero and add 1
                    // Repeat is set back to -1 once faster movements are complete
                    if (LevelLayout.repeat == -1) LevelLayout.repeat = 0;
                    LevelLayout.repeat++;
                    Debug.Log(LevelLayout.repeat);
                }
                break;

            case ButtonTypes.Run:
                Debug.Log("Run button touched!");
                if (!isForwardTouched && !isBackwardTouched && !isRightTouched && !isLeftTouched && !isRepeatTouched)
                {
                    if (!isRunTouched)  isRunTouched = true;
                    else                isRunTouched = false;
                }
                break;
        }
    }

    private void OnFirstTouchStay() { 
          
    }

    private void OnFirstTouchEnd() {
        switch(buttonType)
        {
            case ButtonTypes.Forward:
                Debug.Log("Forward button touch ended!");
                
                break;

            case ButtonTypes.Repeat:
                
                break;
        }
    }

    //---------------------------------------------------------------------------

    // - BUTTON ACTIVE CHECK - Checks whether the buttons have been unlocked by the player. 
    // Bools are static and are created within LevelLayout - //
    private void ButtonActiveCheck()
    {
        forwardButtonTexture.gameObject.SetActive(isForwardEnabled);
        rightButtonTexture.gameObject.SetActive(isRightEnabled);
        leftButtonTexture.gameObject.SetActive(isLeftEnabled);
        backwardButtonTexture.gameObject.SetActive(isBackwardEnabled);
        repeatButtonTexture.gameObject.SetActive(isRepeatEnabled);
    }

    //// ------------------------------------------------------

    void Start () {
        SetupButtons();
        ButtonActiveCheck();


        
    }

    void Update () {
        TouchInput(buttonTexture);
        ButtonActiveCheck();
    }
}
