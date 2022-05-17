using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    private Tilemap2D tilemap2D;
    private Movement2D movement2D;

    private float deathLimitY;


    public void Setup(Vector2Int position, int mapSizeY)
    {
        movement2D = GetComponent<Movement2D>();
        transform.position = new Vector3(position.x, position.y, 0);

        deathLimitY = -mapSizeY / 2;
    }

    private void Update()
    {
        if(transform.position.y <= deathLimitY)
        {
            //Debug.Log("You Die");
            SceneLoader.LoadScene();//ÇöÀç¾À ·Îµå
        }
        UpdateMove();
        UpdateCollision();
    }

    private void UpdateMove()
    {
        float x = Input.GetAxisRaw("Horizontal");

        movement2D.MoveTo(x);
    }

    private void UpdateCollision()
    {
        if(movement2D.IsCollision.up)
        {
            CollisionToTile(CollisionDirection.Up);
        }
        else if (movement2D.IsCollision.down)
        {
            CollisionToTile(CollisionDirection.Down);
        }
        else if (movement2D.IsCollision.left)
        {
            CollisionToTile(CollisionDirection.Left);
        }
        else if(movement2D.IsCollision.right)
        {
            CollisionToTile(CollisionDirection.Right);
        }
    }

    private void CollisionToTile(CollisionDirection direction)
    {
        Tile tile = movement2D.HitTransform.GetComponent<Tile>();
        if(tile != null)
        {
            if(direction == CollisionDirection.Down || direction == CollisionDirection.Up)
            {
                tile.Collision(direction);
            }
            else if(direction == CollisionDirection.Left || direction == CollisionDirection.Right)  
            {
                if (tile.GetComponent<Destination>() || tile.GetComponent<TileBoom>())
                {
                    tile.Collision(direction);
                }
            }
        }
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag.Equals("Item"))
        {
            //Destroy(collision.gameObject);

            tilemap2D.GetCoin(collision.gameObject);
        }
    }
}
