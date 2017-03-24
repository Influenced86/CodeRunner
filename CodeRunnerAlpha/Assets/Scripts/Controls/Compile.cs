using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Compile : TouchManager
{
    private GameObject playerObject;
    private Player player;

    private static float _moveTime = 0;
    private static bool _moveCheck = false;
    public float moveSpeed = 0.4f;

    private bool _compile = false;

    public void MoveCheck()
    {
        if (!_moveCheck)
        {
            _moveTime = 0.0f;
            _moveCheck = true;
        }
        _moveTime += Time.deltaTime * moveSpeed;
    }

    public void CompileStart()
    {
        _compile = true;
    }
        
    private void OnFirstTouchBegan()
    {
        Debug.Log("Compile Touched!");
        CompileStart();

    }
    private void OnFirstTouchStay() { }
    private void OnFirstTouchEnd() { }
   
    // Use this for initialization
    void Start () {
        playerObject = GameObject.Find("Player");
        player = playerObject.GetComponent<Player>();
	}
	
	// Update is called once per frame
	void Update () {
        
        if (_compile)
        {      
            MoveCheck();
            player.transform.position = Vector2.Lerp(player.transform.position, player.moveList[0].transform.position, _moveTime);
            
        }
        TouchInput(buttonTexture);
    }
}
