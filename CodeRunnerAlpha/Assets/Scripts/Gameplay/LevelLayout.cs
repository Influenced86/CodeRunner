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
    public float        moveSpeed;
    private float       slowSpeed = 0.2f;
    private float       standardSpeed = 0.4f;
    private float       repeatSpeed = 1.0f;
    private float       moveTime = 0;
    private bool        moveCheck = false;
    public static int   currentPositionIndex;
    
    // - LEVEL DATA - //
    public Tile[]       tiles = new Tile[48];       // WARNING : DO NOT CHANGE TO PRIVATE (would have to re-do all the tiles in the editor)
    public static bool  isChestTextEnabled = false;
    public bool         isChestFound = false;
    private static int  levelNumber = 1;
    
    //// ------------------------------------------------------

    //// - METHODS -------------------------///////------------
    // - NEXT POSITIONS - Returns the tile positions
    //  up, down, left, and right of the player - //
    private Vector3 NextForwardPosition()
    {
        var forwardPosition = tiles[currentPositionIndex + 6].transform.position;
        return forwardPosition;
    }
    private Vector3 NextRightPosition()
    {
        var rightPosition = tiles[currentPositionIndex + 1].transform.position;
        return rightPosition;
    }
    private Vector3 NextLeftPosition()
    {
        var leftPosition = tiles[currentPositionIndex - 1].transform.position;
        return leftPosition;
    }
    private Vector3 NextBackwardPosition()
    {
        var backwardPosition = tiles[currentPositionIndex - 6].transform.position;
        return backwardPosition; 
    }

    // - MOVE CHECK - Provides the movement of the player with a static 
    // speed. Removes the problem of the smoothing when using linear
    // interpolation - //
    private void MoveCheck() {
        if (!moveCheck)
        {
            moveTime = 0.0f;
            moveCheck = true;
        }
        moveTime += Time.deltaTime * moveSpeed;
    }

    // - GOAL CHECK - Checks which level the player is currently on and 
    // loads the next level - //
    private void GoalCheck()    {
        if (tiles[currentPositionIndex].isGoalTile)
        {
            Debug.Log("You reached the end of the level!");
            switch (levelNumber)
            {
                case 1:
                    SceneManager.LoadScene("LevelTwo");
                    levelNumber = 2;
                    break;
                case 2:
                    SceneManager.LoadScene("LevelThree");
                    levelNumber = 3;
                    break;
                case 3:
                    SceneManager.LoadScene("LevelFour");
                    levelNumber = 4;
                    break;
                case 4:
                    SceneManager.LoadScene("LevelFive");
                    levelNumber = 5;
                    break;
            }
        }
    }

    // - CHEST CHECK - Checks the current level to give out the  
    // correct reward from that level's chest - //
    private void ChestCheck()
    {
        Debug.Log("You reached a chest!");
        switch (levelNumber)
        {
            case 1:
                PlayerControls.isRightEnabled = true;
                Debug.Log(PlayerControls.isRightEnabled);
                break;
            case 2:
                PlayerControls.isBackwardEnabled = true;
                break;
            case 4:
                PlayerControls.isLeftEnabled = true;
                break;

        }
    }

   


    // - TILE CHECKS - Each function checks the tiles closest to the current tile to find out  
    // whether the player can move to that tile and if so, make the appropriate movement and update
    // the player's new current tile - //
    private void ForwardCheck() {
        switch (tiles[currentPositionIndex + 6].tileType)
        {
            // If the next tile is accessible
            case Tile.TypeOfTile.Open:
                if (PlayerControls.isForwardTouched)
                {
                    // Make sure that if PlayerControls.PlayerControls.repeat is active, the player moves faster
                    if (PlayerControls.repeat >= 0)        moveSpeed = repeatSpeed;
                    else                                   moveSpeed = standardSpeed;

                    // Setup the movement from one tile to the next
                    MoveCheck();
                    playerObject.transform.position = Vector3.Lerp(playerObject.transform.position, NextForwardPosition(), moveTime);
                    playerAnim.SetBool("Forward", PlayerControls.isForwardTouched);

                    // If player has reached next tile, then set new current position and stop player moving
                    if (playerObject.transform.position == NextForwardPosition())
                    {
                        currentPositionIndex += 6;                                         
                        GoalCheck();
                        PlayerControls.isForwardTouched = false;
                        moveCheck = false;
                        playerAnim.SetBool("Forward", false);

                        // If the PlayerControls.repeat button has been pressed, keep recalling the method untill PlayerControls.repeat = 0
                        if (PlayerControls.repeat > 0 && currentPositionIndex <= 43)
                        {
                            PlayerControls.repeat--;
                            Debug.Log(PlayerControls.repeat);
                            PlayerControls.isForwardTouched = true;
                            ForwardCheck();
                        }
                        // If PlayerControls.repeat is now zero, make sure the player no longer has speed increase
                        else    PlayerControls.repeat = -1;
                    }
                }
                break;

            // If the next tile is blocked
            case Tile.TypeOfTile.Wall:
                if (PlayerControls.isForwardTouched)
                {
                    // If the player's next move is a wall, stop the PlayerControls.repeat speed bonus
                    if (PlayerControls.repeat > 0)     PlayerControls.repeat = -1;
                    
                    Debug.Log("Cannot move forward! Wall ahead!");
                    // - Makes sure current position remains the same in this case (Player was originally
                    // automatically moving forward once the tile was later set to open - //
                    currentPositionIndex -= 6;
                }
                break;

            // If the next tile is a slow (i.e. mud)
            case Tile.TypeOfTile.Slow:
                if (PlayerControls.isForwardTouched)
                {
                    if (PlayerControls.repeat >= 0)     moveSpeed = repeatSpeed * 0.5f;
                    else                 moveSpeed = slowSpeed;
                    
                    MoveCheck();
                    playerObject.transform.position = Vector3.Lerp(playerObject.transform.position, NextForwardPosition(), moveTime);
                    playerAnim.SetBool("Forward", PlayerControls.isForwardTouched);

                    if (playerObject.transform.position == NextForwardPosition())
                    {
                        currentPositionIndex += 6;
                        PlayerControls.isForwardTouched = false;
                        moveSpeed = standardSpeed;
                        moveCheck = false;
                        playerAnim.SetBool("Forward", false);

                        if (PlayerControls.repeat > 0)
                        {
                            PlayerControls.repeat--;
                            Debug.Log(PlayerControls.repeat);
                            PlayerControls.isForwardTouched = true;
                            ForwardCheck();
                        }
                        else    PlayerControls.repeat = -1;
                    }
                }
                break;

            // If next tile is a hole
            case Tile.TypeOfTile.Hole:
                if (PlayerControls.isForwardTouched)    Debug.Log("Oh dear, you fell down a hole");
                break;

            // If next tile is the goal
            case Tile.TypeOfTile.Chest:
                if (PlayerControls.isForwardTouched)
                {
                    if (PlayerControls.repeat > 0)     PlayerControls.repeat = -1;

                    ChestCheck();                   
                    PlayerControls.isForwardTouched = false;
                    
                    // Don't allow text to be shown again after moving towards chest
                    if (!isChestFound)
                    {
                        isChestTextEnabled = true;
                        isChestFound = true;
                    }                                    
                }
                break;           
        }
    }
    private void RightCheck() {
       
        switch(tiles[currentPositionIndex + 1].tileType)
        {
            case Tile.TypeOfTile.Open:
                if(PlayerControls.isRightTouched)
                {
                    // WHAT
                    if (PlayerControls.repeat >= 0)    moveSpeed = repeatSpeed;
                    else                moveSpeed = standardSpeed;

                    MoveCheck();
                    playerObject.transform.position = Vector3.Lerp(playerObject.transform.position, NextRightPosition(), moveTime);
                    playerAnim.SetBool("Right", PlayerControls.isRightTouched);

                    if (playerObject.transform.position == NextRightPosition())
                    {
                        currentPositionIndex += 1;
                        Debug.Log(currentPositionIndex);                       
                        GoalCheck();                                        
                        PlayerControls.isRightTouched = false;
                        moveCheck = false;
                        playerAnim.SetBool("Right", false);

                        if (PlayerControls.repeat > 0)
                        {
                            PlayerControls.repeat--;
                            Debug.Log(PlayerControls.repeat);
                            PlayerControls.isRightTouched = true;
                            RightCheck();
                        }
                        // If PlayerControls.repeat is now zero, make sure the player no longer has speed increase
                        else PlayerControls.repeat = -1;
                    }
                   
                }
                break;

            case Tile.TypeOfTile.Wall:
                if (PlayerControls.isRightTouched)
                {
                    if (PlayerControls.repeat > 0)      PlayerControls.repeat = -1;
                    Debug.Log("Cannot move right! Wall!");
                    currentPositionIndex -= 1;
                }
                break;

            case Tile.TypeOfTile.Slow:
                if (PlayerControls.isRightTouched)
                {
                    moveSpeed = slowSpeed;
                    MoveCheck();
                    playerObject.transform.position = Vector3.Lerp(playerObject.transform.position, NextRightPosition(), moveTime);
                    playerAnim.SetBool("Right", PlayerControls.isRightTouched);
                    if (playerObject.transform.position == NextRightPosition())
                    {
                        currentPositionIndex += 1;
                        PlayerControls.isRightTouched = false;
                        moveSpeed = standardSpeed;
                        moveCheck = false;
                        playerAnim.SetBool("RIght", false);

                        if (PlayerControls.repeat > 0)
                        {
                            PlayerControls.repeat--;
                            Debug.Log(PlayerControls.repeat);
                            PlayerControls.isRightTouched = true;
                            RightCheck();
                        }
                        else PlayerControls.repeat = -1;
                    }

                   
                }
                break;

            // If next tile is a hole
            case Tile.TypeOfTile.Hole:
                if (PlayerControls.isRightTouched)
                {
                    Debug.Log("Oh dear, you fell down a hole");
                }
                break;

            // If next tile is the goal
            case Tile.TypeOfTile.Chest:
                if (PlayerControls.isRightTouched)
                {
                    if (PlayerControls.repeat > 0)     PlayerControls.repeat = -1;
                    ChestCheck();
                    PlayerControls.isRightTouched = false;
                    // Don't allow text to be shown again after moving towards chest
                    if (!isChestFound)
                    {
                        isChestTextEnabled = true;
                        isChestFound = true;
                    }
                }
                break;          
        }
    }
    private void LeftCheck() {
        switch(tiles[currentPositionIndex - 1].tileType)
        {
            case Tile.TypeOfTile.Open:
                if(PlayerControls.isLeftTouched)
                {
                    if (PlayerControls.repeat >= 0)    moveSpeed = repeatSpeed;
                    else                moveSpeed = standardSpeed;

                    MoveCheck();
                    playerObject.transform.position = Vector3.Lerp(playerObject.transform.position, NextLeftPosition(), moveTime);
                    playerAnim.SetBool("Left", PlayerControls.isLeftTouched);
                    if(playerObject.transform.position == NextLeftPosition())
                    {
                        currentPositionIndex -= 1;
                        Debug.Log(currentPositionIndex);
                        GoalCheck();
                        PlayerControls.isLeftTouched = false;
                        moveCheck = false;
                        playerAnim.SetBool("Left", false);

                        if (PlayerControls.repeat > 0)
                        {
                            PlayerControls.repeat--;
                            Debug.Log(PlayerControls.repeat);
                            PlayerControls.isLeftTouched = true;
                            LeftCheck();
                        }
                    }
                    
                }
                break;

            case Tile.TypeOfTile.Wall:
                if(PlayerControls.isLeftTouched)
                {
                    if (PlayerControls.repeat > 0)     PlayerControls.repeat = -1;
                    Debug.Log("Cannot move left! Wall!");
                    currentPositionIndex += 1;
                }
                break;

            case Tile.TypeOfTile.Slow:
                if (PlayerControls.isLeftTouched)
                {
                    if (PlayerControls.repeat >= 0)    moveSpeed = repeatSpeed;
                    else                moveSpeed = standardSpeed;

                    moveSpeed = slowSpeed;
                    MoveCheck();
                    playerObject.transform.position = Vector3.Lerp(playerObject.transform.position, NextLeftPosition(), moveTime);
                    playerAnim.SetBool("Left", PlayerControls.isLeftTouched);
                    if (playerObject.transform.position == NextLeftPosition())
                    {
                        currentPositionIndex -= 1;
                        PlayerControls.isLeftTouched = false;
                        moveSpeed = standardSpeed;
                        moveCheck = false;
                        playerAnim.SetBool("Left", false);

                        if (PlayerControls.repeat > 0)
                        {
                            PlayerControls.repeat--;
                            Debug.Log(PlayerControls.repeat);
                            PlayerControls.isLeftTouched = true;
                            LeftCheck();
                        }
                        else    PlayerControls.repeat = -1;
                    }
                }
                break;

            // If next tile is a hole
            case Tile.TypeOfTile.Hole:
                if (PlayerControls.isLeftTouched)
                {
                    Debug.Log("Oh dear, you fell down a hole");
                }
                break;

            // If next tile is the goal
            case Tile.TypeOfTile.Chest:
                if (PlayerControls.isLeftTouched)
                {
                    if (PlayerControls.repeat >= 0)    PlayerControls.repeat = -1;
                    ChestCheck();
                    PlayerControls.isLeftTouched = false;
                    // Don't allow text to be shown again after moving towards chest
                    if (!isChestFound)
                    {
                        isChestTextEnabled = true;
                        isChestFound = true;
                    }
                }
                break;     
        }
    }
    private void BackwardCheck() {  
        switch (tiles[currentPositionIndex - 6].tileType)
        {
            // If the next tile is accessible
            case Tile.TypeOfTile.Open:
                if (PlayerControls.isBackwardTouched)
                {
                    if (PlayerControls.repeat >= 0)     moveSpeed = repeatSpeed;
                    else                                moveSpeed = standardSpeed;

                    MoveCheck();
                    playerObject.transform.position = Vector2.Lerp(playerObject.transform.position, NextBackwardPosition(), moveTime);
                    playerAnim.SetBool("Down", PlayerControls.isBackwardTouched);  
                      
                    // If player has reached next tile, then set new current position and stop player moving
                    if (playerObject.transform.position == NextBackwardPosition())
                    {
                        currentPositionIndex -= 6;
                        Debug.Log(currentPositionIndex);                        
                        GoalCheck();
                        PlayerControls.isBackwardTouched = false;
                        moveCheck = false;
                        playerAnim.SetBool("Down", false);

                        // - If the PlayerControls.repeat button has been pressed and the next move 
                        // is inbounds, keep recalling the method untill PlayerControls.repeat = 0 - //
                        if (PlayerControls.repeat > 0 && currentPositionIndex >= 6)
                        {
                            PlayerControls.repeat--;
                            Debug.Log(PlayerControls.repeat);
                            PlayerControls.isBackwardTouched = true;
                            BackwardCheck();
                        }
                        else    PlayerControls.repeat = -1;
                    }
                }
                break;

            // If the next tile is blocked
            case Tile.TypeOfTile.Wall:
                if (PlayerControls.isBackwardTouched)
                {
                    if (PlayerControls.repeat > 0)      PlayerControls.repeat = -1;
                    Debug.Log("Cannot move backward! Wall behind!");
                    // - Makes sure current position remains the same in this case (Player was originally 
                    // automatically moving forward once the tile was later set to open - //
                    currentPositionIndex += 6;
                }
                break;

            // If the next tile is a slow (i.e. mud)
            case Tile.TypeOfTile.Slow:
                if (PlayerControls.isBackwardTouched)
                {
                    if (PlayerControls.repeat >= 0)     moveSpeed = repeatSpeed * 0.5f;
                    else                 moveSpeed = slowSpeed;
                   
                    MoveCheck();
                    playerObject.transform.position = Vector3.Lerp(playerObject.transform.position, NextBackwardPosition(), moveTime);
                    playerAnim.SetBool("Down", PlayerControls.isBackwardTouched);
                    if (playerObject.transform.position == NextBackwardPosition())
                    {
                        currentPositionIndex -= 6;
                        PlayerControls.isBackwardTouched = false;
                        moveSpeed = standardSpeed;
                        moveCheck = false;
                        playerAnim.SetBool("Down", false);

                        if (PlayerControls.repeat > 0)
                        {
                            PlayerControls.repeat--;
                            Debug.Log(PlayerControls.repeat);
                            PlayerControls.isBackwardTouched = true;
                            BackwardCheck();
                        }
                        else    PlayerControls.repeat = -1;
                    }
                }
                break;

            // If next tile is a hole
            case Tile.TypeOfTile.Hole:
                if (PlayerControls.isBackwardTouched)
                {
                    Debug.Log("Oh dear, you fell down a hole");
                }
                break;

            // If next tile is the goal
            case Tile.TypeOfTile.Chest:
                if (PlayerControls.isBackwardTouched)
                {
                    if (PlayerControls.repeat > 0)     PlayerControls.repeat = -1;

                    ChestCheck();
                    PlayerControls.isBackwardTouched = false;
                    // Don't allow text to be shown again after moving towards chest
                    if (!isChestFound)
                    {
                        isChestTextEnabled = true;
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
        PlayerControls.repeat = -1;
        moveSpeed = standardSpeed;
        isChestTextEnabled = false;
        isChestFound = false;
        foreach(Tile aTile in tiles) {
            aTile.SetPosition(pos);
        
            if(aTile.isStartTile)
            {
                playerObject.transform.position = aTile.transform.position;
                // - Makes sure the players current position tile is the same 
                // as the start tile each time the level is loaded - //
                currentPositionIndex = aTile.GetPosition();
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
        playerObject.transform.position = tiles[currentPositionIndex].transform.position;
    }

    //// ------------------------------------------------------

    // Use this for initialization
    void Start () {
        LevelSetup();
       // Run();
    }
	
	// Update is called once per frame
	void Update () {
        // Make sure we can only make a move if the next position is inbounds     
        if (currentPositionIndex <= 42)
        {
            ForwardCheck();
        }
        if (currentPositionIndex >= 6)
        {
            BackwardCheck();
        }
        RightCheck();
        LeftCheck();
        chestTextTexture.gameObject.SetActive(isChestTextEnabled);


        //Run();
        




    }
}
