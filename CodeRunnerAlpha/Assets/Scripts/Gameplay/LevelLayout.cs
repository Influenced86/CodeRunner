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
        iconArray[0] =  tiles[6].transform.position;
        iconArray[1] =  tiles[12].transform.position;
        iconArray[2] =  tiles[18].transform.position;
        iconArray[3] =  tiles[24].transform.position;
        iconArray[4] =  tiles[30].transform.position;
        iconArray[5] =  tiles[36].transform.position;
        iconArray[6] =  tiles[42].transform.position;
        iconArray[7] =  tiles[43].transform.position;
        iconArray[8] =  tiles[44].transform.position;
        iconArray[9] =  tiles[45].transform.position;
        iconArray[10] = tiles[46].transform.position;
        iconArray[11] = tiles[47].transform.position;
        iconArray[12] = tiles[41].transform.position;
        iconArray[13] = tiles[35].transform.position;
        iconArray[14] = tiles[29].transform.position;
        iconArray[15] = tiles[23].transform.position;
        iconArray[16] = tiles[17].transform.position;
        iconArray[17] = tiles[11].transform.position;
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
