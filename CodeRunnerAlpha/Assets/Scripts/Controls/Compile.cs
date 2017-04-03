using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class Compile : TouchManager
{
    private static GameObject       _playerObject;
    private static Player           _player;

    private static GameObject       _levelObject;
    private static LevelLayout      _levelLayout;

    private static GameObject       _playerControlsObject;
    private static PlayerControls   _playerControls;

    private static GameObject       _cancelObject;
    private static CancelCommand    _cancel;

    public GameObject       PathIcon;
    public GameObject       WallPathIcon;
    
    private static float    _moveTime = 0;
    private static bool     _moveCheck = false;   
    private static bool     _isCompile = false;
    private static int      _compileListIterator = 0;

    public int CompileListIterator
    {
        get { return _compileListIterator; }
        set { }
    }
    public bool GetIsCompile()
    {
        return _isCompile;
    }

    // Creates a static speed for the player
    public void MoveCheck()
    {
        if (!_moveCheck)
        {
            _moveTime = 0.0f;
            _moveCheck = true;
        }
        _moveTime += Time.deltaTime * _player.moveSpeed;
    }

    // Called when compile button is pressed. 
    public void CompileStart()
    {
        _isCompile = true;
        _levelLayout.CurrentIconArrayIndex = 0;
        DestroyClones();
        
    }
    // When the player touches the texture belonging to this object    
    private void OnFirstTouchBegan()
    {
        Debug.Log("Compile Touched!");
        CompileStart();
    }
    private void OnFirstTouchStay() { }
    private void OnFirstTouchEnd() { }

    
    // - GOAL CHECK - Checks which level the player is currently on and 
    // loads the next level - //
    private void GoalCheck()
    {
        if (_levelLayout.tiles[_player.CurrentGridCoordinate].isGoalTile)
        {
            Debug.Log("You reached the end of the level!");
            switch (_levelLayout.LevelNumber)
            {
                case 1:
                    EndOfCompile();
                    SceneManager.LoadScene("LevelTwo");
                    _levelLayout.LevelNumber = 2;
                    break;
                case 2:
                    EndOfCompile();
                    SceneManager.LoadScene("LevelThree");
                    _levelLayout.LevelNumber = 3;
                    break;
                case 3:
                    EndOfCompile();
                    SceneManager.LoadScene("LevelFour");
                    _levelLayout.LevelNumber = 4;
                    break;
                case 4:
                    EndOfCompile();
                    SceneManager.LoadScene("LevelFive");
                    _levelLayout.LevelNumber = 5;
                    break;
                case 5:
                    EndOfCompile();
                    SceneManager.LoadScene("LevelSix");
                    _levelLayout.LevelNumber = 6;
                    break;
                case 6:
                    EndOfCompile();
                    SceneManager.LoadScene("LevelSeven");
                    _levelLayout.LevelNumber = 7;
                    break;

            }
        }
    }
    // Checks the current level to give out the  
    // correct reward from that level's chest
    private void ChestCheck()
    {
        Debug.Log("You reached a chest!");
        switch (_levelLayout.LevelNumber)
        {
            case 1:
                EndOfCompile();
                _levelLayout.IsChestTextEnabled = true;             
                _playerControls.IsRightEnabled = true;
                break;
            case 2:
                EndOfCompile();
                _levelLayout.IsChestTextEnabled = true;
                _playerControls.IsBackwardEnabled = true;
                break;
            case 4:
                EndOfCompile();
                _levelLayout.IsChestTextEnabled = true;
                _playerControls.IsLeftEnabled = true;
                break;
            case 7:
                EndOfCompile();
                // TODO: Unlock repeat and enable reward text
                break;

        }
    }

    private void HoleCheck()
    {
        switch (_levelLayout.LevelNumber)
        {
            case 6:
            case 7:
                if (_levelLayout.tiles[_player.CurrentGridCoordinate].tileType == Tile.TypeOfTile.Hole)
                {
                    DestroyClones();
                    _player.transform.position = _levelLayout.tiles[6].transform.position;
                    _player.CurrentGridCoordinate = 6;
                    _player.CurrentCompileCoordinate = 6;
                    EndOfCompile();        
                }
                break;
          
        }
    }

    // Sets up the correct animation bools depending on the difference value
    // between the current coordinate and the next
    private void SetUpAnim()
    {
        _player.playerAnim.SetBool("Forward", true);
        _player.playerAnim.SetBool("Down", false);
        _player.playerAnim.SetBool("Right", false);
        _player.playerAnim.SetBool("Left", false);
    }
    private void SetDownAnim()
    {
        _player.playerAnim.SetBool("Forward", false);
        _player.playerAnim.SetBool("Down", true);
        _player.playerAnim.SetBool("Right", false);
        _player.playerAnim.SetBool("Left", false);
    }
    private void SetRightAnim()
    {
        _player.playerAnim.SetBool("Forward", false);
        _player.playerAnim.SetBool("Down", false);
        _player.playerAnim.SetBool("Right", true);
        _player.playerAnim.SetBool("Left", false);
    }
    private void SetLeftAnim()
    {
        _player.playerAnim.SetBool("Forward", false);
        _player.playerAnim.SetBool("Down", false);
        _player.playerAnim.SetBool("Right", false);
        _player.playerAnim.SetBool("Left", true);
    }
    private void CancelAnim()
    {
        _player.playerAnim.SetBool("Forward", false);
        _player.playerAnim.SetBool("Down", false);
        _player.playerAnim.SetBool("Right", false);
        _player.playerAnim.SetBool("Left", false);
    }

    // Determines which player animation to play
    private void SetPlayerAnimation(int nextMoveIndex)
    {
        switch (nextMoveIndex)
        {
            case 6:
                SetUpAnim();
                break;
            case -6:
                SetDownAnim();
                break;
            case 1:
                SetRightAnim();
                break;
            case -1:
                SetLeftAnim();
                break;
        }
    }

    // Clears the movelist and sets up data ready for the next move. 
    private void EndOfCompile()
    {
        _player.moveList.Clear();
        _player.CurrentCompileCoordinate = _player.CurrentGridCoordinate;
        _player.moveSpeed = _player.StandardSpeed;
        _compileListIterator = 0;
        
        _isCompile = false;
        CancelAnim();
    }

    // Sets up next path icon during compile movement. Determined by the difference in value between the current
    // grid coordinate and the next one.
    private void SetupPathIcons(int nextMoveIndex)
    {
        switch (nextMoveIndex)
        {
            case 6:
                if (_levelLayout.tiles[_player.moveList[_compileListIterator].cpCoordinate].tileType == Tile.TypeOfTile.Wall)
                {
                    Instantiate(WallPathIcon, _player.moveList[_compileListIterator].cpTransform.position - new Vector3(0, 1f, 0), Quaternion.Euler(0, 0, 0));
                }
                Instantiate(PathIcon, _player.moveList[_compileListIterator].cpTransform.position - new Vector3(0, 1f, 0), Quaternion.Euler(0, 0, 0));
                break;
            case -6:
                if (_levelLayout.tiles[_player.moveList[_compileListIterator].cpCoordinate].tileType == Tile.TypeOfTile.Wall)
                {
                    Instantiate(WallPathIcon, _player.moveList[_compileListIterator].cpTransform.position - new Vector3(0, 0f, 0), Quaternion.Euler(0, 0, 0));
                }
                Instantiate(PathIcon, _player.moveList[_compileListIterator].cpTransform.position - new Vector3(0, 0f, 0), Quaternion.Euler(0, 0, 0));
                break;
            case 1:
                if (_levelLayout.tiles[_player.moveList[_compileListIterator].cpCoordinate].tileType == Tile.TypeOfTile.Wall)
                {
                    Instantiate(WallPathIcon, _player.moveList[_compileListIterator].cpTransform.position - new Vector3(1f, 0, 0), Quaternion.Euler(0, 0, 270));
                }
                Instantiate(PathIcon, _player.moveList[_compileListIterator].cpTransform.position - new Vector3(1f, 0, 0), Quaternion.Euler(0, 0, 270));
                break;
            case -1:
                if (_levelLayout.tiles[_player.moveList[_compileListIterator].cpCoordinate].tileType == Tile.TypeOfTile.Wall)
                {
                    Instantiate(WallPathIcon, _player.moveList[_compileListIterator].cpTransform.position - new Vector3(-1f, 0f, 0), Quaternion.Euler(0, 0, 90));
                }
                Instantiate(PathIcon, _player.moveList[_compileListIterator].cpTransform.position - new Vector3(-1f, 0, 0), Quaternion.Euler(0, 0, 90));
                break;
        }
    }

    // Destroy all objects which were spawned dynamically
    private void DestroyClones()
    {
        var clones = GameObject.FindGameObjectsWithTag("Clone");
        foreach (var clone in clones)
        {
            Destroy(clone);
        }
    }
    // Called when player presses compile button. Reads the list that has been created from the player's directional movement
    // and moves the player accordingly
    private void Go()
    {
        // Only go if compile is true and the list isn't empty
        if (_isCompile && _player.moveList.Count > 0)
        {
            // Keep going until the last move has complete
            if(_compileListIterator < _player.moveList.Count)
            {
               
                // Work out the value difference between current tile index and next tile index
                var nextGridCoordinate = _player.moveList[_compileListIterator].cpCoordinate - _player.CurrentGridCoordinate;
                // Set player animation
                SetPlayerAnimation(nextGridCoordinate);
                // Set next path movement icon
                SetupPathIcons(nextGridCoordinate);
                // If the next tile on the list is a wall, stop
                if (_levelLayout.tiles[_player.moveList[_compileListIterator].cpCoordinate].tileType == Tile.TypeOfTile.Wall)    EndOfCompile();              
                if (_levelLayout.tiles[_player.moveList[_compileListIterator].cpCoordinate].tileType == Tile.TypeOfTile.Chest)   ChestCheck();
                if (_levelLayout.tiles[_player.moveList[_compileListIterator].cpCoordinate].tileType == Tile.TypeOfTile.Slow)    _player.moveSpeed = _player.SlowSpeed;
            
                // Setup and perform movement to current tile in the list
                MoveCheck();              
                // Move player to next tile in list
                _player.transform.position = Vector2.Lerp(_levelLayout.tiles[_player.CurrentGridCoordinate].transform.position, _player.moveList[_compileListIterator].cpTransform.position, _moveTime);
                // The more moves the player makes, the faster the player moves
                _player.moveSpeed += Player.CompileSpeedIncrease;
                // Once the player has reached the next tile, update grid index position of player
                if (_player.transform.position == _player.moveList[_compileListIterator].cpTransform.position)
                {
                    
                    CancelAnim();
                    _player.CurrentGridCoordinate = _player.moveList[_compileListIterator].cpCoordinate;                   
                    _player.transform.position = _player.moveList[_compileListIterator].cpTransform.position;
                    _moveCheck = false;
                    _compileListIterator = _compileListIterator + 1;
                    // If there's a hole, reset the level
                    HoleCheck();
                    // If it's the goal, load the next level                 
                    GoalCheck();
                    // If it's the last position in the list, stop
                    if (_compileListIterator == _player.moveList.Count)
                    {
                        DestroyClones();
                        EndOfCompile();
                       
                    }                       
                    // Recall function if there are still more items left in the list
                    Go();

                }
            }
          
        }
        // Compile is false if the player presses when the list is empty
        else
        {
            _isCompile = false;
        }
    }

    // Use this for initialization
    void Start () {
        _playerObject = GameObject.Find("Player");
        _player = _playerObject.GetComponent<Player>();

        _levelObject = GameObject.Find("BackTiles");
        _levelLayout = _levelObject.GetComponent<LevelLayout>();

        _playerControlsObject = GameObject.Find("Buttons");
        _playerControls = _playerControlsObject.GetComponent<PlayerControls>();

        _cancelObject = GameObject.Find("Cancel");
        _cancel = _cancelObject.GetComponent<CancelCommand>();
    }
    
	// Update is called once per frame
	void Update () {
        
        // If buttons are usable (moves are declared), brighten button colour
        if(_player.moveList.Count > 0 && !_isCompile)
        {
            currentButtonTexture.color = Color.white;
            _cancel.currentButtonTexture.color = Color.white;
        }
        // Otherwise darken out buttons
        else 
        {
            currentButtonTexture.color = new Color(0.7f, 0.7f, 0.7f);
            _cancel.currentButtonTexture.color = new Color(0.4f, 0.4f, 0.4f);
        }
        // Compile
        Go();       
        TouchInput(currentButtonTexture);
        
    }
}
