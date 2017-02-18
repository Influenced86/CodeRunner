using UnityEngine;
using System.Collections;

// - Sets up the user guide buttons labelled as '?' within
// the game - //

public class Helper : TouchManager
{
    //// - VARIABLES -------------------------- ///// ----------
    // - ENUMS - // -------------------------------
    public enum HelperType { helperForward, helperChest, helperGoal, helperMud };
    public HelperType helperType;
    // --------------------------------------------

    // - TEXTURES - // ----------------------------
    public GUITexture helperTexture = null;
    public GUITexture textTexture = null; 
    // --------------------------------------------

    // - VECTORS - // ----------------------------
    private Vector3 vInitialPosition;
    private Vector3 vYMax;
    private Vector3 vYMin;
    // --------------------------------------------

    // - GAMEOBJECTS - // -------------------------
    public GameObject helperButtonObject = null;    
    // --------------------------------------------

    // - HELPER DATA - // -------------------------
    private bool    isStartMove;
    private float   floatSpeed;
    // --------------------------------------------

    //// - METHODS -------------------------///////------------
    // - TOUCH MESSAGES - // - Implements the messages received from 
    // TouchManager. Enables the associated text popups for helper 
    // buttons - //
    private void OnFirstTouchBegan() { 
        switch(helperType)
        {
            case HelperType.helperForward:
                HelperText.isTextEnabled = true;
                Debug.Log("YOU TOUCHED THE FORWARD HELPER");
                textTexture.gameObject.SetActive(true);
                helperTexture.gameObject.SetActive(false);
                Debug.Log(HelperText.isTextEnabled);
                break;

            case HelperType.helperChest:
                HelperText.isTextEnabled = true;
                Debug.Log("YOU TOUCHED THE CHEST HELPER");
                textTexture.gameObject.SetActive(true);
                helperTexture.gameObject.SetActive(false);
                break;

            case HelperType.helperGoal:
                HelperText.isTextEnabled = true;
                Debug.Log("YOU TOUCHED THE GOAL HELPER");
                textTexture.gameObject.SetActive(true);
                helperTexture.gameObject.SetActive(false);
                break;

            case HelperType.helperMud:
                HelperText.isTextEnabled = true;
                Debug.Log("YOU TOUCHED THE MUD HELPER");
                textTexture.gameObject.SetActive(true);
                helperTexture.gameObject.SetActive(false);
                break;
        }    
    }

    private void OnFirstTouchStay()
    {

    }
    private void OnFirstTouchEnd()
    {
        
    }

    // - FLOAT - Sets up the movement animation - //
    private void Float() {
        if (isStartMove)
        {
            // Move the button upwards until it reaches yMax
            helperButtonObject.transform.position = Vector2.Lerp(helperButtonObject.transform.position, vYMax, floatSpeed * Time.deltaTime);
            if (helperButtonObject.transform.position == vYMax)
            {
                isStartMove = false;
            }   
        }
        else
        {
            // Once the button has reached yMax, move it downward untill it reaches yMin
            helperButtonObject.transform.position = Vector2.Lerp(helperButtonObject.transform.position, vYMin, floatSpeed * Time.deltaTime);
            if (helperButtonObject.transform.position == vYMin)
            {
                isStartMove = true;
            }  
        }         
    }

    // Use this for initialization
    void Start () {
        floatSpeed = 10.0f;
        isStartMove = true;
        vInitialPosition = helperButtonObject.transform.position;
        vYMax = new Vector2(vInitialPosition.x, vInitialPosition.y + 0.022f);
        vYMin = new Vector2(vInitialPosition.x, vInitialPosition.y + 0.011f);
    }
	
	// Update is called once per frame
	void Update () {
        Float();
        TouchInput(helperTexture);	    
	}
}
