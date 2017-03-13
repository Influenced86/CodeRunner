using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

// - Provides all of the core gameplay mechanisms. Decides what type of tiles
// do what and how the player's actions are determined by those types.
// Also handles when the text for the chests are enabled on each level - //

public class LevelLayout : MonoBehaviour {
  
    //// - VARIABLES -------------------------- ///// ----------
    // - GAME OBJECTS - //
    public GameObject   playerObject = null;
    public GameObject   chestObject = null;
    public Animator     playerAnim;
        
    // - GUI TEXTURES - //
    public GUITexture chestTextTexture = null;

    // - PLAYER DATA - //
    private const float     _SlowSpeed = 0.2f;
    private const float     _StandardSpeed = 0.4f;
    private const float     _RepeatSpeed = 1.0f;
    private static float    _moveTime = 0;
    private static bool     _moveCheck = false;
    private static int      _currentPositionIndex;
    
    public float            moveSpeed;

    // - LEVEL DATA - //
    private const int       _Forward = 6, _Right = 1;
    private static Vector3  _vNextForwardPosition;
    private static Vector3  _vNextBackwardPosition;
    private static Vector3  _vNextRightPosition;
    private static Vector3  _vNextLeftPosition;
    private static bool     _isChestTextEnabled = false;
    private static int      _levelNumber = 1;

    public Tile[]           tiles = new Tile[48];       // WARNING : DO NOT CHANGE TO PRIVATE (would have to re-do all the tiles in the editor)
    public bool             isChestFound = false;
   

    private GameObject theControls;
    private PlayerControls controls;

    public float rewardTextTimer = 3.2f;

    //// ------------------------------------------------------

    // - GETTERS - SETTERS - PROPERTIES - // ------
    public int GetCurrentPositionIndex()
    {
        return _currentPositionIndex;
    }

    public bool IsChestTextEnabled
    {
        get { return _isChestTextEnabled; }
        set { _isChestTextEnabled = value; }
    }

    //// - METHODS -------------------------///////------------
    // - NEXT POSITIONS - Returns the tile positions
    //  up, down, left, and right of the player - //

    private void CountdownDisableText(GUITexture text)
    { 
        if (_isChestTextEnabled)
        {
            rewardTextTimer -= Time.deltaTime;
            if(rewardTextTimer < 0)
            {
                text.gameObject.SetActive(false);
                _isChestTextEnabled = false;
                rewardTextTimer = 3.2f;
            }
        }
    }

    private Vector3 NextTilePosition(int numberOfTiles)
    {
        var nextPosition = tiles[_currentPositionIndex + numberOfTiles].transform.position;
        return nextPosition;
    }
   
    

    // - MOVE CHECK - Provides the movement of the player with a static 
    // speed. Removes the problem of the smoothing when using linear
    // interpolation - //
    private void MoveCheck() {
        if (!_moveCheck)
        {
            _moveTime = 0.0f;
            _moveCheck = true;
        }
        _moveTime += Time.deltaTime * moveSpeed;
    }

    // - GOAL CHECK - Checks which level the player is currently on and 
    // loads the next level - //
    private void GoalCheck()    {
        if (tiles[_currentPositionIndex].isGoalTile)
        {
            controls.ResetRepeat();
            Debug.Log("You reached the end of the level!");
            switch (_levelNumber)
            {
                case 1:
                    SceneManager.LoadScene("LevelTwo");
                    _levelNumber = 2;
                    break;
                case 2:
                    SceneManager.LoadScene("LevelThree");
                    _levelNumber = 3;
                    break;
                case 3:
                    SceneManager.LoadScene("LevelFour");
                    _levelNumber = 4;
                    break;
                case 4:
                    SceneManager.LoadScene("LevelFive");
                    _levelNumber = 5;
                    break;
            }
        }
    }

    // - CHEST CHECK - Checks the current level to give out the  
    // correct reward from that level's chest - //
    private void ChestCheck()
    {
        Debug.Log("You reached a chest!");
        switch (_levelNumber)
        {
            case 1:
                controls.IsRightEnabled = true;
                Debug.Log(controls.IsRightEnabled);
                break;
            case 2:
                controls.IsBackwardEnabled = true;
                break;
            case 4:
                controls.IsLeftEnabled = true;
                break;

        }
    }

    // - DIRECTION FUNCTION CHECK - If repeat is active, we need to know which action is being used
    // in order to be able to use recursion - //
    private void DirectionFunctionCheck(string animationName, ref Vector3 nextTransPos)
    {
        switch (animationName)
        {
            case "Forward":
                nextTransPos = NextTilePosition(_Forward);
                break;
            case "Down":
                nextTransPos = NextTilePosition(-_Forward);
                break;
            case "Right":
                nextTransPos = NextTilePosition(_Right);
                break;
            case "Left":
                nextTransPos = NextTilePosition(-_Right);
                break;
        }
    }

    private void NextMoveCheck(ref bool isButtonTouched, int tileAmount, ref Vector3 nextTransformPosition, string animName)
    {
        
        switch (tiles[_currentPositionIndex + tileAmount].tileType)
        {

            // If the next tile is accessible
            case Tile.TypeOfTile.Open:
                if (isButtonTouched)
                {
                        
                    if (controls.GetRepeat() >= 0) moveSpeed = _RepeatSpeed;
                    else moveSpeed = _StandardSpeed;

                    // Setup the movement from one tile to the next
                    MoveCheck();
                    playerObject.transform.position = Vector3.Lerp(playerObject.transform.position, nextTransformPosition, _moveTime);
                    playerAnim.SetBool(animName, isButtonTouched);

                    // If player has reached next tile, then set new current position and stop player moving
                    if (playerObject.transform.position == nextTransformPosition)
                    {
                        _currentPositionIndex += tileAmount;
                        GoalCheck();
                        isButtonTouched = false;
                        _moveCheck = false;
                        playerAnim.SetBool(animName, false);
                        

                        // Cancel the repeat if the next position is out of bounds  
                        if (controls.GetRepeat() > 0 && animName == "Forward" && _currentPositionIndex >= 42)        controls.ResetRepeat();
                        else if (controls.GetRepeat() > 0 && animName == "Down" && _currentPositionIndex <= 5)       controls.ResetRepeat();

                        // If the PlayerControls.repeat button has been pressed, keep recalling the method untill PlayerControls.repeat = 0
                        if (controls.GetRepeat() > 0)
                        {
                            
                            controls.DecrementRepeat();
                            Debug.Log(controls.GetRepeat());
                            isButtonTouched = true;
                            // Setup the next tile position for the recursion
                            DirectionFunctionCheck(animName, ref nextTransformPosition);
                            NextMoveCheck(ref isButtonTouched, tileAmount, ref nextTransformPosition, animName);
                        }
                        // If PlayerControls.repeat is now zero, make sure the player no longer has speed increase
                        else controls.ResetRepeat();
                    }
                }
                break;

            // If the next tile is blocked
            case Tile.TypeOfTile.Wall:
                if (isButtonTouched)
                {

                    // If the player's next move is a wall, stop the PlayerControls.repeat speed bonus
                    if (controls.GetRepeat() > 0)   controls.ResetRepeat();

                    Debug.Log("Cannot move! Wall ahead!");
                    // - Makes sure current position remains the same in this case (Player was originally
                    // automatically moving forward once the tile was later set to open - //
                    _currentPositionIndex -= tileAmount;
                }
                break;

            case Tile.TypeOfTile.Slow:
                if (isButtonTouched)
                {
                    if (controls.GetRepeat() >= 0) moveSpeed = _RepeatSpeed * 0.5f;
                    else moveSpeed = _SlowSpeed;

                    MoveCheck();
                    playerObject.transform.position = Vector3.Lerp(playerObject.transform.position, nextTransformPosition, _moveTime);
                    playerAnim.SetBool(animName, isButtonTouched);

                    if (playerObject.transform.position == nextTransformPosition)
                    {
                        _currentPositionIndex += tileAmount;
                        isButtonTouched = false;
                        moveSpeed = _StandardSpeed;
                        _moveCheck = false;
                        playerAnim.SetBool(animName, false);

                        if (controls.GetRepeat() > 0 && animName == "Forward" && _currentPositionIndex >= 42)    controls.ResetRepeat();
                        else if (controls.GetRepeat() > 0 && animName == "Down" && _currentPositionIndex <= 5)   controls.ResetRepeat();

                        if (controls.GetRepeat() > 0)
                        {
                            controls.DecrementRepeat();
                            Debug.Log(controls.GetRepeat());
                            isButtonTouched = true;
                            // Setup the next tile position for the recursion
                            DirectionFunctionCheck(animName, ref nextTransformPosition);
                            NextMoveCheck(ref isButtonTouched, tileAmount, ref nextTransformPosition, animName);
                        }
                        else    controls.ResetRepeat();
                    }
                }
                break;

            // If next tile is a hole
            case Tile.TypeOfTile.Hole:
                if (isButtonTouched)
                {
                    Debug.Log("Oh dear, you fell down a hole");
                }
                break;

            // If next tile is the goal
            case Tile.TypeOfTile.Chest:
                if (isButtonTouched)
                {
                    if (controls.GetRepeat() > 0)   controls.ResetRepeat();

                    ChestCheck();
                    isButtonTouched = false;

                    // Don't allow text to be shown again after moving towards chest
                    if (!isChestFound)
                    {
                        _isChestTextEnabled = true;
                        isChestFound = true;
                    }
                }
                break;
        }
    }

    // - LEVEL SETUP - Runs each time level starts. Positions depend on what is set within 
    // the editor. Also initiates all tiles in chronological order from left to right - //
    private void LevelSetup() {
        
        var pos = 0;
        controls.ResetRepeat();
        moveSpeed = _StandardSpeed;
        _isChestTextEnabled = false;
        isChestFound = false;
        foreach(Tile aTile in tiles) {
            aTile.SetPosition(pos);
        
            if(aTile.isStartTile)
            {
                playerObject.transform.position = aTile.transform.position;
                // - Makes sure the players current position tile is the same 
                // as the start tile each time the level is loaded - //
                _currentPositionIndex = aTile.GetPosition();
            }
            if(aTile.isChestTile)
            {
                // Loads chest on goal tile (for now)
                chestObject.transform.position = aTile.transform.position;
            }
            if(aTile.isGoalTile)
            {

            }
            pos++;
        }
    }

    // - CHANGE PLAYER POSITION (Not currently in use) - //
    public void ChangePlayerPosition() {
        playerObject.transform.position = tiles[_currentPositionIndex].transform.position;
    }

    //// ------------------------------------------------------

   

    

    // Use this for initialization
    void Start () {
        
        theControls = GameObject.Find("Forward");
        controls = theControls.GetComponent<PlayerControls>();
        LevelSetup();
    }
	
	// Update is called once per frame
	void Update () {
        

        if (_currentPositionIndex <= 42)
        {
            _vNextForwardPosition = NextTilePosition(_Forward);
            NextMoveCheck(ref PlayerControls.isForwardTouched, _Forward, ref _vNextForwardPosition, "Forward");
        }
        
        if (_currentPositionIndex >= 6)
        {
            _vNextBackwardPosition = NextTilePosition(-_Forward);
            NextMoveCheck(ref PlayerControls.isBackwardTouched, -_Forward, ref _vNextBackwardPosition, "Down");
        }

        _vNextRightPosition = NextTilePosition(_Right);
        NextMoveCheck(ref PlayerControls.isRightTouched, _Right, ref _vNextRightPosition, "Right");

        _vNextLeftPosition = NextTilePosition(-_Right);
        NextMoveCheck(ref PlayerControls.isLeftTouched, -_Right, ref _vNextLeftPosition, "Left");

        CountdownDisableText(chestTextTexture);
        chestTextTexture.gameObject.SetActive(_isChestTextEnabled);

        controls.ButtonActiveCheck();
    }

}
