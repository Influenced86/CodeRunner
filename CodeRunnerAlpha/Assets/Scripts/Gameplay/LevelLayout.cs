using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

// Setups up the layout of the level, including - tile positions, button list icon positions,
// chests, chest text.
public class LevelLayout : MonoBehaviour {
  
    private GameObject      _playerObject;
    private Player          _player;

    public GameObject       chestObject;
    public Animator         playerAnim;

    private const int       _IconArrayLength = 18;
    public Vector2[]        iconArray = new Vector2[_IconArrayLength];
    private static int      _currentIconArrayIndex = 0;
    
    public GUITexture       chestTextTexture;
    public GameObject       hole;

    public Tile[]           tiles = new Tile[48];       // WARNING : DO NOT CHANGE TO PRIVATE (would have to re-do all the tiles in the editor)
   
    private static int      _levelNumber = 1;
    private static bool     _isChestTextEnabled = false;
    
    public float            rewardTextTimer = 3.2f;
   

    public int CurrentIconArrayIndex
    {
        get { return _currentIconArrayIndex; }
        set { _currentIconArrayIndex = value; }
    }
    public bool IsChestTextEnabled
    {
        get { return _isChestTextEnabled; }
        set { _isChestTextEnabled = value; }
    }
    public int LevelNumber
    {
        get { return _levelNumber; }
        set { _levelNumber = value; }
    }

    // Disables unlockable chest texts
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

    // Runs each time level starts. Positions depend on what is set within 
    // the editor. Also initiates all tiles in chronological order from left to right.
    private void LevelSetup()
    {       
        var pos = 0;
        foreach(Tile aTile in tiles)
        {
            aTile.SetPosition(pos);

            if (aTile.isStartTile)
            {
                _player.CurrentGridCoordinate = aTile.GetPosition();
                _player.CurrentCompileCoordinate = aTile.GetPosition();
                _player.transform.position = aTile.transform.position;
                Debug.Log(_player.CurrentGridCoordinate);       
            }
            if (aTile.isChestTile)
            {               
                chestObject.transform.position = aTile.transform.position;
            }    
            if(aTile.tileType == Tile.TypeOfTile.Hole)
            {
                Instantiate(hole, aTile.transform.position, Quaternion.identity);
            }    
            pos++;
        }
    }

    // Sets up the positions for the direction icon spawns in the level
    private void SetupIconDirectionPosition()
    {
        var iconIndex = 0;
        for(int i = 42; i <= 47; ++i)
        {
            iconArray[iconIndex] = tiles[i].transform.position;
            iconIndex++;
        }
        for (int i = 36; i <= 41; ++i)
        {
            iconArray[iconIndex] = tiles[i].transform.position;
            iconIndex++;
        }
        for (int i = 30; i <= 35; ++i)
        {
            iconArray[iconIndex] = tiles[i].transform.position;
            iconIndex++;
        }
        
    }
  
    void Start () {
        _playerObject = GameObject.Find("Player");
        _player = _playerObject.GetComponent<Player>();
       
        LevelSetup();
        SetupIconDirectionPosition();
    }
	
	// Update is called once per frame
	void Update () {
        SetupIconDirectionPosition();
        CountdownDisableText(chestTextTexture);
        chestTextTexture.gameObject.SetActive(_isChestTextEnabled);
   
    }

}
