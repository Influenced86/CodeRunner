using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class Compile : TouchManager
{
    private GameObject _playerObject;
    private Player _player;

    private GameObject _levelObject;
    private LevelLayout _levelLayout;

    private GameObject _playerControlsObject;
    private PlayerControls _playerControls;

    private GameObject _cancelObject;
    private CancelCommand _cancel;

    private static float _moveTime = 0;
    private static bool _moveCheck = false;
    

    private static bool _isCompile = false;
    private static int _compileListIterator = 0;

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
    // Called when compile button is pressed
    public void CompileStart()
    {
        _isCompile = true;
    }
        
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
            //controls.ResetRepeat();
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
            }
        }
    }
    // - CHEST CHECK - Checks the current level to give out the  
    // correct reward from that level's chest - //
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

        }
    }

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

    private void EndOfCompile()
    {
        _player.moveList.Clear();
        _player.CurrentCompileCoordinate = _player.CurrentGridCoordinate;
        _player.moveSpeed = _player.StandardSpeed;
        _compileListIterator = 0;
        _isCompile = false;
        CancelAnim();
        
        
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
                // If the next tile on the list is a wall, stop
                if (_levelLayout.tiles[_player.moveList[_compileListIterator].cpCoordinate].tileType == Tile.TypeOfTile.Wall)    EndOfCompile();
                if (_levelLayout.tiles[_player.moveList[_compileListIterator].cpCoordinate].tileType == Tile.TypeOfTile.Chest)   ChestCheck();
                if (_levelLayout.tiles[_player.moveList[_compileListIterator].cpCoordinate].tileType == Tile.TypeOfTile.Slow)    _player.moveSpeed = _player.SlowSpeed;
                //if (_levelLayout.tiles[_player.moveList[_compileListIterator].cpCoordinate].tileType == Tile.TypeOfTile.Open)   _player.moveSpeed = _player.StandardSpeed;  

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
                    GoalCheck();
                    // If it's the last position in the list, stop
                    if (_compileListIterator == _player.moveList.Count)
                    {
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
            buttonTexture.color = Color.white;
            _cancel.buttonTexture.color = Color.white;
        }
        // Otherwise darken out buttons
        else 
        {
            buttonTexture.color = new Color(0.7f, 0.7f, 0.7f);
            _cancel.buttonTexture.color = new Color(0.4f, 0.4f, 0.4f);
        }
        Go();       
        TouchInput(buttonTexture);
        
    }
}
