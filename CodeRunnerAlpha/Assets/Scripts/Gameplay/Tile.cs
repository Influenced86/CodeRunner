using UnityEngine;
using System.Collections;

// Each tile has its own data which is initialised here but defined
// within the LeveLLayout class and within Unity (Publicly). Each tile
// is used to keep track of the player's movement.

public class Tile : MonoBehaviour {

    public int      _positionIndex;
    public bool     isChestTile;
    public bool     isStartTile;
    public bool     isGoalTile;

    // Each type of tile is chosen within the engine meaning each
    // new level can easily be edited
    public enum TypeOfTile { Wall, Open, Slow, Hole, Chest };
    public TypeOfTile tileType;

    public void SetPosition(int position)
    {
        _positionIndex = position;
    }
    public void SetGoal(bool chest)
    {
        isChestTile = chest;
    }
    public void SetStart(bool start)
    {
        isStartTile = start;
    }
    public int GetPosition()
    {
        return _positionIndex;
    }

    public Tile() {
        _positionIndex = 0;
        isStartTile = false;
        isChestTile = false;
        isGoalTile = false;
        tileType = TypeOfTile.Wall;
    }
 
    

  
}
