using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct CompilePair
{
    public int cpIndex;
    public Transform cpTransform;
}

public class Player : MonoBehaviour {

    // The player
    public GameObject playerObject;
    public Animator playerAnim;
    // The level
    private GameObject _level;
    private LevelLayout _levelLayout;

    private GameObject compileObject;
    private Compile compile;

    // Used to store each compile movement, grid index value and tile transform
    public List<CompilePair> moveList = new List<CompilePair>();

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
        if (!compile.GetIsCompile())
        {
            CompilePair compilePair;
            _currentcompilePosition += tileAmount;
            var gridPos = _currentcompilePosition;
            compilePair.cpIndex = gridPos;
            compilePair.cpTransform = _levelLayout.tiles[gridPos].transform;
            moveList.Add(compilePair);
            Debug.Log("Move count total: " + moveList.Count);
            Debug.Log("Current compile position: " + _currentcompilePosition);
        }
       
    }
    
    // Cancel the compile setup by resetting position and clearing the list, only if it's not empty
    public void Cancel()
    {
        // Only cancel the move set IF there is more than one move in the list AND
        if (moveList.Count > 0 && !compile.GetIsCompile())
        {     
            moveList.Clear();
            _currentcompilePosition = _currentGridPosition;
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

        compileObject = GameObject.Find("Compile");
        compile = compileObject.GetComponent<Compile>();

        //PlayerSetup();

    }
	
	// Update is called once per frame
	void Update () {
       
    }
}
