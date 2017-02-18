using UnityEngine;
using System.Collections;

// - Displays the correct text for the corresponding helper 
// button touched by the player - //

public class HelperText : TouchManager {

    //// - VARIABLES -------------------------- ///// ----------
    // - ENUMS - // -------------------------------
    public enum HelperTextType { textForward, textChest, textGoal, textChestReward, textMud };
    public HelperTextType helperText;
    // --------------------------------------------

    // - TEXTURES - // ----------------------------
    public GUITexture textHelperTexture;
    // --------------------------------------------

    // - DATA - // --------------------------------
    public static bool isTextEnabled = false;
    // --------------------------------------------

    private void OnFirstTouchBegan() { 

        switch (helperText)
        {
            case HelperTextType.textForward:
                Debug.Log("YOU TOUCHED THE FORWARD TEXT");
                isTextEnabled = false;
                break;
            case HelperTextType.textChest:
                Debug.Log("YOU TOUCHED THE CHEST TEXT");
                isTextEnabled = false;
                break;
            case HelperTextType.textGoal:
                Debug.Log("YOU TOUCHED THE GOAL TEXT");
                isTextEnabled = false;
                break;
            case HelperTextType.textChestReward:
                Debug.Log("YOU TOUCHED THE CHEST REWARD TEXT");
                LevelLayout.isChestTextEnabled = false;
                break;
            case HelperTextType.textMud:
                Debug.Log("YOU TOUCHED THE CHEST REWARD TEXT");
                isTextEnabled = false;
                break;
        }
    }

    private void TextActiveCheck() {
       textHelperTexture.gameObject.SetActive(isTextEnabled);
    }

    // Use this for initialization
    void Start () {     
        TextActiveCheck();
	}
	
	// Update is called once per frame
	void Update () {
        TouchInput(textHelperTexture);
        TextActiveCheck();
	}

}
