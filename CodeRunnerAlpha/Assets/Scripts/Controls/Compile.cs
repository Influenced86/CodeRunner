using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Compile : TouchManager
{
    private GameObject _playerObject;
    private Player _player;

    private GameObject _level;
    private LevelLayout _levelLayout;

    private static float _moveTime = 0;
    private static bool _moveCheck = false;
    public float moveSpeed = 0.4f;

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

    public void MoveCheck()
    {
        if (!_moveCheck)
        {
            _moveTime = 0.0f;
            _moveCheck = true;
        }
        _moveTime += Time.deltaTime * moveSpeed;
    }

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

    private void Go()
    {
        if (_isCompile && _player.moveList.Count > 0)
        {
            if(_compileListIterator < _player.moveList.Count)
            {
                MoveCheck();
                _player.transform.position = Vector2.Lerp(_levelLayout.tiles[_player.CurrentGridPosition].transform.position, _player.moveList[_compileListIterator].cpTransform.position, _moveTime);
                if (_player.transform.position == _player.moveList[_compileListIterator].cpTransform.position)
                {
                    _player.CurrentGridPosition = _player.moveList[_compileListIterator].cpIndex;
                    _player.transform.position = _player.moveList[_compileListIterator].cpTransform.position;
                    _moveCheck = false;
                    _compileListIterator = _compileListIterator + 1;
                    if (_compileListIterator == _player.moveList.Count)
                    {                        
                        _player.moveList.Clear();
                        _player.CurrentCompilePosition = _player.CurrentGridPosition;
                        
                        _compileListIterator = 0;
                        _isCompile = false;
                    }
                    Go();

                }
            }
          
        }
        else
        {
            _isCompile = false;
        }
    }

    // Use this for initialization
    void Start () {
        _playerObject = GameObject.Find("Player");
        _player = _playerObject.GetComponent<Player>();

        _level = GameObject.Find("BackTiles");
        _levelLayout = _level.GetComponent<LevelLayout>();
	}
    
	// Update is called once per frame
	void Update () {
        Go();
        //if (_compile)
        //{
        //    MoveCheck();
        //    _player.transform.position = Vector2.Lerp(_levelLayout.tiles[_player.CurrentGridPosition].transform.position, _player.moveList[0].cpTransform.position, _moveTime);
        //    if(_player.transform.position == _player.moveList[0].cpTransform.position)
        //    {
        //        _player.CurrentGridPosition = _player.moveList[0].cpIndex;
        //        _moveCheck = false;
        //        test = true;

        //    }
            
        //        MoveCheck();
        //        _player.transform.position = Vector2.Lerp(_levelLayout.tiles[_player.CurrentGridPosition].transform.position, _player.moveList[1].cpTransform.position, _moveTime);
            


        //}
        //else
        //{
        //    _player.CurrentGridPosition = _player.CurrentCompilePosition;
        //}
        TouchInput(buttonTexture);
        
    }
}
