using UnityEngine;

public class TileBoom : Tile
{


    public override void Collision(CollisionDirection direction)
    {
           
        SceneLoader.LoadScene();
    }
}
