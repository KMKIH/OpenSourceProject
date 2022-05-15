using UnityEngine;

public enum TileType
{
    Empty = 0, Base, Broke, Boom, Jump, StraightLeft, StraightRight, Destination, LastIndex
}

public enum CollisionDirection { Up = 0, Down }

public abstract class Tile : MonoBehaviour//후에 abstract로 변경
{
    protected Movement2D movement2D;
    public virtual void Setup(Movement2D movement2D)
    {
        this.movement2D = movement2D;
    }

    public abstract void Collision(CollisionDirection direction);

}
