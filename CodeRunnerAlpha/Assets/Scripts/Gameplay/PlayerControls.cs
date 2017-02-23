using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;


// WELL HELLO THERE!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!

// - Takes care of all the movement controls for the player. Sets up
// when it's elible for the player to press any button. If a button
// is successfully pressed, a specific bool for that button is set to
// true and is then dealt with inside the LevelLayout class - //

public class PlayerControls : TouchManager {

    public static int repeat;

    //// - VARIABLES -------------------------- ///// ----------
    // - ENUMS - // -------------------------------
    public enum ButtonTypes { Forward, Backward, Left, Right, Repeat };
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
    public GUITexture repeatButtonTexture0 = null;
    public GUITexture repeatButtonTexture1 = null;
    public GUITexture repeatButtonTexture2 = null;
    public GUITexture repeatButtonTexture3 = null;
    public GUITexture repeatButtonTexture4 = null;

    // -------------------------------------------

    // - STATICS - // ------------------------------
    // - Enable or disable the button controls - //
    public static bool isForwardEnabled = true;
    public static bool isRightEnabled = false; 
    public static bool isLeftEnabled = false;
    public static bool isBackwardEnabled = false;
    public static bool isRepeatEnabled = true;
    

    // - Checked in LevelLayout. Used to decide 
    // whether the player is eligible to move or not - //
    public static bool isForwardTouched = false;
    public static bool isBackwardTouched = false;
    public static bool isRightTouched = false;
    public static bool isLeftTouched = false;
    public static bool isRepeatTouched = false;
   
    // -------------------------------------------
    //// ------------------------------------------------------
    
    


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
            
        }
    }

    // - TOUCH MESSAGES - // - Implements the messages received from 
    // TouchManager - //
    private void OnFirstTouchBegan() {
        switch(buttonType)
        {
            case ButtonTypes.Forward:
            
                Debug.Log("Forward button touched!");
                // - Only allow the player to be able to move if the other buttons are not active AND
                // the next move is within the boundaries of the game world - //
                if (!isBackwardTouched && !isRightTouched && !isLeftTouched && LevelLayout.currentPositionIndex < 42)                 
                {
                    isForwardTouched = true;
                    
                }
                break;

            case ButtonTypes.Backward:
                Debug.Log("Backward button touched!");
                if (!isForwardTouched && !isRightTouched && !isLeftTouched && LevelLayout.currentPositionIndex > 5)
                {
                    isBackwardTouched = true;
                }
                break;

            case ButtonTypes.Right:
                Debug.Log("Right button touched!");
                if(!isForwardTouched && !isBackwardTouched && !isLeftTouched)
                {
                    isRightTouched = true;
                }
                break;

            case ButtonTypes.Left:
                Debug.Log("Left button touched!");
                if(!isForwardTouched && !isBackwardTouched && !isRightTouched)
                {
                    isLeftTouched = true;
                }
                break;

            case ButtonTypes.Repeat:
                Debug.Log("Repeat button touched!");
                if (!isForwardTouched && !isBackwardTouched && !isRightTouched && !isLeftTouched)
                {
                    // If repeat is pressed, then set it to zero and add 1
                    // Repeat is set back to -1 once faster movements are complete
                    if (repeat == -1)   repeat = 0;
                    repeat++;
                    // Don't allow the player to repeat more than four times
                    if (repeat >= 4)    repeat = 4;
                    Debug.Log(repeat);
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


        }
    }

    // Changes the repeat button texture to match the amount of repeats the player wishes to use
    public void SetRepeatButtonTexture()
    {
        switch (repeat)
        {
           
            case 1:
                repeatButtonTexture.texture = repeatButtonTexture1.texture;
                break;
            case 2:
                repeatButtonTexture.texture = repeatButtonTexture2.texture;
                break;
            case 3:
                repeatButtonTexture.texture = repeatButtonTexture3.texture;
                break;
            case 4:
                repeatButtonTexture.texture = repeatButtonTexture4.texture;
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
        // Resets repeat texture to 0
        if(repeat == 0 || repeat == -1)
        {
            repeatButtonTexture.texture = repeatButtonTexture0.texture;
        }
        TouchInput(buttonTexture);
        ButtonActiveCheck();
        SetRepeatButtonTexture();

    }
}
