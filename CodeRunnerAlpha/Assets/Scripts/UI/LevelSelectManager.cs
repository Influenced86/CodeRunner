using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelSelectManager : TouchManager {

    public GUITexture menuButton;
    public GUITexture[] leveButton;

    private bool _isButtonsOn = false;
   
    private void OnFirstTouchBegan()
    {
        if (!_isButtonsOn) _isButtonsOn = true;
        else _isButtonsOn = false;
        foreach(GUITexture tex in leveButton)
        {
            tex.gameObject.SetActive(_isButtonsOn);
        }
    }

    // Use this for initialization
    void Start () {
        foreach (GUITexture tex in leveButton)
        {
            tex.gameObject.SetActive(false);
        }
        menuButton.gameObject.SetActive(true);
    }
	
	// Update is called once per frame
	void Update () {
        TouchInput(menuButton);
	}
}
