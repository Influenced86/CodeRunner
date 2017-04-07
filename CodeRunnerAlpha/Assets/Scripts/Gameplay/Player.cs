using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Pair of values, tile grid coordinate, and the transform for that tile
public struct CompilePair
{
    public int cpCoordinate;
    public Transform cpTransform;
}

public class Player : MonoBehaviour {

    // The player
    public GameObject       playerObject;
    public Animator         playerAnim;
    // The level
    private static GameObject      _level;
    private static LevelLayout     _levelLayout;
    // The compiler
    private static GameObject      _compileObject;
    private static Compile         _compile;
    // The repeat
    private static GameObject      _repeatObject;
    private static RepeatCommand   _repeat;

    // Used to store each compile movement, grid index value and tile transform
    public List<CompilePair> moveList = new List<CompilePair>();

    public float            moveSpeed = 1.0f;
    public const float      CompileSpeedIncrease = 0.014f;
    public const float      ReapeatSpeedIncrease = 0.15f;
    private const float     _SlowSpeed = 0.7f;
    private const float     _StandardSpeed = 1.0f;
    private const float     _RepeatSpeed = 1.0f;
    private const int       _MaxInBounds = 47;
    private const int       _MinInBounds = 0;
   
    private static int      _currentGridCoordinate;
    private static int      _currentCompileCoordinate;

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
    public float StandardSpeed
    {
        get { return _StandardSpeed; }
        set { }
    }
    public float SlowSpeed
    {
        get { return _SlowSpeed; }
        set { }
    }

    // Called each time a direction is pressed within the corresponding direction command classes 
    // Adds a pair to a list, the tile grid index and the transform of that tile
    public void AddNewPosition(int tileAmount)
    {
        // Only add if player is not moving
        if (!_compile.GetIsCompile())
        {
            // Updates the grid coordindate for the compile 
            _currentCompileCoordinate += tileAmount;
            // Only add to the list if it's not out of bounds
            if (_currentCompileCoordinate >= _MinInBounds && _currentCompileCoordinate <= _MaxInBounds)
            {
                var gridPos = _currentCompileCoordinate;
                // Setup pair to add to compile list
                CompilePair compilePair;
                compilePair.cpCoordinate = gridPos;
                compilePair.cpTransform = _levelLayout.tiles[gridPos].transform;
                moveList.Add(compilePair);
                // If repeat is active, add moves until repeat is zero
                if (_repeat.RepeatValue != 0)
                {
                    _repeat.RepeatValue--;
                    AddNewPosition(tileAmount);
                }                            
            }
            // If out of bounds, return to previous compile coordinate
            else
            {
                _currentCompileCoordinate -= tileAmount;
            }

            Debug.Log("Move count total: " + moveList.Count);
            Debug.Log("Current compile position: " + _currentCompileCoordinate);
        }
       
    }

 
    
    // Cancel the compile setup by resetting position and clearing the list, only if it's not empty
    public void Cancel()
    {
        _repeat.RepeatValue = 0;
        // Only cancel the move set IF there is more than one move in the list AND is not compiling
        if (moveList.Count > 0 && !_compile.GetIsCompile())
        {     
            moveList.Clear();            
            _currentCompileCoordinate = _currentGridCoordinate;
            Debug.Log("Move count total: " + moveList.Count);
            Debug.Log("Current compile position: " + _currentCompileCoordinate);
        }
        else
        {
            if(_levelLayout.IsChestTextEnabled)
            {
                _levelLayout.IsChestTextEnabled = false;
            }
        }
    }
      
    void Start () {
        _level = GameObject.Find("BackTiles");
        _levelLayout = _level.GetComponent<LevelLayout>();

        _compileObject = GameObject.Find("Compile");
        _compile = _compileObject.GetComponent<Compile>();

        _repeatObject = GameObject.Find("Repeat");
        _repeat = _repeatObject.GetComponent<RepeatCommand>();

        moveSpeed = _StandardSpeed;
    }
	
	void Update () {
        
    }
}
