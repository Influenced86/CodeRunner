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

    public List<Vector2> moveList = new List<Vector2>();

    private static int _currentGridPosition;
    private static int _currentcompilePosition;

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

    // 
    public void AddNewPosition(int tileAmount)
    {
        _currentcompilePosition += tileAmount;
        moveList.Add(_levelLayout.tiles[_currentcompilePosition].transform.position);
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
    public void Compile()
    {

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
