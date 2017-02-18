using UnityEngine;
using System.Collections;

// - Each tile has its own data which is initialised here but defined
// within the LeveLLayout class and within Unity (Publicly). Each tile
// is used to keep track of the player's movement - //

public class Tile : MonoBehaviour {

    //// - VARIABLES --------------- //// ----
    // - TILE DATA - //
    public int  positionIndex;
    public bool isChestTile;
    public bool isStartTile;
    public bool isGoalTile;

    // - Each type of tile is chosen within the engine meaning each
    // new level can easily be edited
    public enum TypeOfTile { Wall, Open, Slow, Hole, Chest };
    public TypeOfTile tileType;

    // - DEFAULT CONSTRUCTOR - //
    public Tile() {
        positionIndex = 0;
        isStartTile = false;
        isChestTile = false;
        isGoalTile = false;
        tileType = TypeOfTile.Wall;
    }
    // - SETTERS - //
    public void SetPosition(int position) {
        positionIndex = position;
    }
    public void SetGoal(bool chest) {
        isChestTile = chest;
    }
    public void SetStart(bool start) {
        isStartTile = start;
    }

    // - GETTERS - //
    public int GetPosition() {
        return positionIndex;
    }

  
}
