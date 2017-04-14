using UnityEngine;
using System.Collections;

// Sets up the user guide buttons labelled as '?' within
// the game

public class Helper : TouchManager
{
    public enum HelperType { helperForward, helperChest, helperGoal, helperMud };
    public HelperType helperType;
   
    public GUITexture       helperTexture = null;
    public GUITexture       textTexture;
   
    private Vector3         _vInitialPosition;
    private Vector3         _vYMax;
    private Vector3         _vYMin;
  
    public GameObject       helperButtonObject;
  
    private static float    _floatSpeed;
    private bool            _isStartMove;

    //  Implements the messages received from TouchManager. Enables the associated text 
    //  popups for helper buttons
    private void OnFirstTouchBegan()
    { 
        switch(helperType)
        {
            case HelperType.helperForward:
                textTexture.gameObject.SetActive(true);
                break;

            case HelperType.helperChest:
                textTexture.gameObject.SetActive(true);
                Debug.Log("YOU TOUCHED THE CHEST HELPER");
                break;

            case HelperType.helperGoal:
                textTexture.gameObject.SetActive(true);
                Debug.Log("YOU TOUCHED THE GOAL HELPER");
                break;

            case HelperType.helperMud:
                textTexture.gameObject.SetActive(true);
                Debug.Log("YOU TOUCHED THE MUD HELPER"); 
                break;
        } 
    }
    private void OnFirstTouchMoved()
    {
        OnFirstTouchEnd();
    }
   
    private void OnFirstTouchStay()
    {

    }
    private void OnFirstTouchEnd()
    {
        switch (helperType)
        {
            case HelperType.helperForward:
                textTexture.gameObject.SetActive(false);
                
                break;

            case HelperType.helperChest:
                textTexture.gameObject.SetActive(false);
                Debug.Log("YOU TOUCHED THE CHEST HELPER");
                break;

            case HelperType.helperGoal:
                textTexture.gameObject.SetActive(false);
                Debug.Log("YOU TOUCHED THE GOAL HELPER");

                break;

            case HelperType.helperMud:
                textTexture.gameObject.SetActive(false);
                Debug.Log("YOU TOUCHED THE MUD HELPER");

                break;
        }
    }

    // - FLOAT - Sets up the movement animation - //
    private void Float()
    {
        if (_isStartMove)
        {
            // Move the button upwards until it reaches yMax
            helperButtonObject.transform.position = Vector2.Lerp(helperButtonObject.transform.position, _vYMax, _floatSpeed * Time.deltaTime);
            if (helperButtonObject.transform.position == _vYMax)
            {
                _isStartMove = false;
            }   
        }
        else
        {
            // Once the button has reached yMax, move it downward untill it reaches yMin
            helperButtonObject.transform.position = Vector2.Lerp(helperButtonObject.transform.position, _vYMin, _floatSpeed * Time.deltaTime);
            if (helperButtonObject.transform.position == _vYMin)
            {
                _isStartMove = true;
            }  
        }         
    }

    // Use this for initialization
    void Start ()
    {
        _floatSpeed = 10.0f;
        _isStartMove = true;
        _vInitialPosition = helperButtonObject.transform.position;
        _vYMax = new Vector2(_vInitialPosition.x, _vInitialPosition.y + 0.022f);
        _vYMin = new Vector2(_vInitialPosition.x, _vInitialPosition.y + 0.011f);
    }
	
	// Update is called once per frame
	void Update ()
    {
        Float();
        TouchInput(helperTexture);
        
          
	}
}
