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
    }

    private void CollisionToTile(CollisionDirection direction)
    {
        Tile tile = movement2D.HitTransform.GetComponent<Tile>();
        if(tile != null)
        {
            tile.Collision(direction);
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
