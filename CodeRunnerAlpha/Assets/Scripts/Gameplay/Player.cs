using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

    // The player
    public GameObject playerObject;
    public Animator playerAnim;
    // The level
    private GameObject _level;
    private LevelLayout _levelLayout;
    

    public List<Transform> moveList = new List<Transform>();

    
    private const float _SlowSpeed = 0.2f;
    private const float _StandardSpeed = 0.4f;
    private const float _RepeatSpeed = 1.0f;
    
    private static int _currentGridPosition;
    private static int _currentcompilePosition;

    //public float MoveTime
    //{
    //    get { return _moveTime; }
    //    set { }
    //}
    //public void SetMoveCheck(bool val)
    //{
    //    _moveCheck = val;
    //}
    public int CurrentGridPosition
    {
        get { return _currentGridPosition; }
        set { _currentGridPosition = value; }
    }
    public int CurrentCompilePosition
    {
        get { return _currentcompilePosition; }
        set { _currentcompilePosition = value; }
    }

    // - MOVE CHECK - Provides the movement of the player with a static 
    // speed. Removes the problem of the smoothing when using linear
    // interpolation - //
    

    // Called each time a direction is pressed within the corresponding direction command classes
    public void AddNewPosition(int tileAmount)
    {
        _currentcompilePosition += tileAmount;
        moveList.Add(_levelLayout.tiles[_currentcompilePosition].transform);
        Debug.Log("Move count total: " + moveList.Count);
        Debug.Log("Current compile position: " + _currentcompilePosition);
    }
    
    // Cancel the compile setup by resetting position and clearing the list, only if it's not empty
    public void Cancel()
    {
        if (moveList.Count > 0)
        {
            _currentcompilePosition = _currentGridPosition;
            moveList.Clear();

            Debug.Log("Move count total: " + moveList.Count);
            Debug.Log("Current compile position: " + _currentcompilePosition);
        }
    }
   
    

    private void PlayerSetup()
    {
       
        
    }

    // Use this for initialization
    void Start () {
        _level = GameObject.Find("BackTiles");
        _levelLayout = _level.GetComponent<LevelLayout>();
        
        //PlayerSetup();
        
    }
	
	// Update is called once per frame
	void Update () {
       
    }
}
