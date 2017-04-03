using UnityEngine;
using System.Collections;

// Provides the touch mechanisms for the UI. Anything that the players
// touches will inherit from this class and use the TouchInput method for
// whatever it is that the player is touching //

public class TouchManager : MonoBehaviour {

    public GUITexture   currentButtonTexture;
    public GUITexture   buttonTexture0;
    public GUITexture   buttonTexture1;
    public GUITexture   buttonTexture2;
    public GUITexture   buttonTexture3;
    public GUITexture   buttonTexture4;

    public void SetupButtonTexture(int repeatNum)
    {
        switch(repeatNum)
        {
            case 1:
                currentButtonTexture.texture = buttonTexture1.texture;
                break;
            case 2:
                currentButtonTexture.texture = buttonTexture2.texture;
                break;
            case 3:
                currentButtonTexture.texture = buttonTexture3.texture;
                break;
            case 4:
                currentButtonTexture.texture = buttonTexture4.texture;
                break;
            case 0:
                currentButtonTexture.texture = buttonTexture0.texture;
                break;
        }
    }


    // Gets the type of input from the user and sends an appropriate 
    // message to which PlayerControls takes care of
    public void TouchInput(GUITexture texture) {
        if(texture.HitTest(Input.GetTouch(0).position))
        {
            switch(Input.GetTouch(0).phase)
            {
                case TouchPhase.Began:
                    SendMessage("OnFirstTouchBegan");                                    
                    break;
                case TouchPhase.Stationary:
                    SendMessage("OnFirstTouchStay");                   
                    break;
                case TouchPhase.Moved:
                    SendMessage("OnFirstTouchMoved");                   
                    break;
                case TouchPhase.Ended:
                    SendMessage("OnFirstTouchEnd");                 
                    break;
            }
        }
    }

  
    void Start () {
        
    }
	    
	
	void Update () {
	
	}
}
