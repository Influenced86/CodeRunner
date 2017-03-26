using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct CompilePair
{
    public int cpCoordinate;
    public Transform cpTransform;
}

public class Player : MonoBehaviour {

    // The player
    public GameObject playerObject;
    public Animator playerAnim;
    // The level
    private GameObject _level;
    private LevelLayout _levelLayout;
    // The compiler
    private GameObject _compileObject;
    private Compile _compile;

    // Used to store each compile movement, grid index value and tile transform
    public List<CompilePair> moveList = new List<CompilePair>();

    private const float _SlowSpeed = 0.2f;
    private const float _StandardSpeed = 0.4f;
    private const float _RepeatSpeed = 1.0f;
    
    private static int _currentGridCoordinate;
    private static int _currentCompileCoordinate;

    public int CurrentGridCoordinate
    {
        get { return _currentGridCoordinate; }
        set { _currentGridCoordinate = value; }
    }
    public int CurrentCompileCoordinate
    {
        get { return _currentCompileCoordinate; }
        set { _currentCompileCoordinate = value; }
    }

    // - MOVE CHECK - Provides the movement of the player with a static 
    // speed. Removes the problem of the smoothing when using linear
    // interpolation - //
    

    // Called each time a direction is pressed within the corresponding direction command classes 
    // Adds a pair to a list, the tile grid index and the transform of that tile
    public void AddNewPosition(int tileAmount)
    {
        // Only add if player is not moving
        if (!_compile.GetIsCompile())
        {
            // Updates the grid coordindate for the compile 
            _currentCompileCoordinate += tileAmount;
            var gridPos = _currentCompileCoordinate;
            // Setup pair to add to compile list
            CompilePair compilePair;    
            compilePair.cpCoordinate = gridPos;
            compilePair.cpTransform = _levelLayout.tiles[gridPos].transform;
            moveList.Add(compilePair);

            Debug.Log("Move count total: " + moveList.Count);
            Debug.Log("Current compile position: " + _currentCompileCoordinate);
        }
       
    }
    
    // Cancel the compile setup by resetting position and clearing the list, only if it's not empty
    public void Cancel()
    {
        // Only cancel the move set IF there is more than one move in the list AND
        if (moveList.Count > 0 && !_compile.GetIsCompile())
        {     
            moveList.Clear();
            _currentCompileCoordinate = _currentGridCoordinate;
            Debug.Log("Move count total: " + moveList.Count);
            Debug.Log("Current compile position: " + _currentCompileCoordinate);
        }
    }
   
   

    private void PlayerSetup()
    {
       
        
    }

    // Use this for initialization
    void Start () {
        _level = GameObject.Find("BackTiles");
        _levelLayout = _level.GetComponent<LevelLayout>();

        _compileObject = GameObject.Find("Compile");
        _compile = _compileObject.GetComponent<Compile>();


    }
	
	// Update is called once per frame
	void Update () {
        
    }
}
