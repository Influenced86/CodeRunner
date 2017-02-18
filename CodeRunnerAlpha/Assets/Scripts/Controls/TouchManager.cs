using UnityEngine;
using System.Collections;

// - Provides the touch mechanisms for the UI. Anything that the players
// touches will inherit from this class and use the TouchInput method for
// whatever it is that the player is touching - //

public class TouchManager : MonoBehaviour {

    public static bool isGuiTouched = false;

    // - Gets the type of input from the user and sends an appropriate 
    // message to which PlayerControls takes care of
    public void TouchInput(GUITexture texture) {
        if(texture.HitTest(Input.GetTouch(0).position))
        {
            switch(Input.GetTouch(0).phase)
            {
                case TouchPhase.Began:
                    SendMessage("OnFirstTouchBegan");
                    //SendMessage("OnFirstTouch");
                    isGuiTouched = true;
                    break;

                case TouchPhase.Stationary:
                    SendMessage("OnFirstTouchStay");
                    isGuiTouched = true;
                    break;

                case TouchPhase.Moved:
                    //SendMessage("OnFirstTouchMoved");
                   // guiTouched = true;
                    break;

                case TouchPhase.Ended:
                    SendMessage("OnFirstTouchEnd");
                    isGuiTouched = false;
                    break;
            }
        }
    }

	// Use this for initialization
	void Start () {
	
	}
	    
	// Update is called once per frame
	void Update () {
	
	}
}
