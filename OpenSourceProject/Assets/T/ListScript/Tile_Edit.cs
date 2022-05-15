using UnityEngine;


public enum TileType_Edit
{
    Empty = 0, Base, Broke, Boom, Jump, StraightLeft, StraightRight, Blink,     // 타일
    ItemCoin = 10,                                                              //아이템
    Player = 100                                                                  //플레이어
}


public class Tile_Edit : MonoBehaviour
{
    [SerializeField]
    private Sprite[] tileImages;    // 타일 이미지 배열
    [SerializeField]
    private Sprite[] itemImages;    // 아이템 이미지 배열
    [SerializeField]
    private Sprite playerImage;    // 플레이어 이미지

    private TileType_Edit tileType;

    private SpriteRenderer spriteRenderer;

    public void Setup(TileType_Edit tileType)
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        TileType_Edit = tileType;
    }

    public TileType_Edit TileType_Edit
    {
        set
        {
            tileType = value;

            // 타일 (Empty, Base, Broke, Boom, Jump, StraightLeft, StraightRight, Blink)
            if ((int)TileType_Edit < (int)TileType_Edit.ItemCoin)
            {
                spriteRenderer.sprite = tileImages[(int)tileType];
            }
            // 아이템 (Coin)
            else if ((int)TileType_Edit < (int)TileType_Edit.Player)
            {
                spriteRenderer.sprite = itemImages[(int)tileType - (int)TileType_Edit.ItemCoin];
            }
            // 플레이어 캐릭터 (맵 에디터에 보여주기 위해 설정 하였으며,
            // 저장할 땐 위치정보를 저장하고 플레이어 위치의 타일은 Empty로 설정
            else if ((int)TileType_Edit == (int)TileType_Edit.Player)
            {
                spriteRenderer.sprite = playerImage;
            }
        }
        get => tileType;
    }
}


