public class TileBase : Tile
{
    public override void Collision(CollisionDirection direction)
    {
        if(direction == CollisionDirection.Down)
        {
            movement2D.JumpTo();
        }
    }
}
