using UnityEngine;


public enum TileType_Edit
{
    Empty = 0, Base, Broke, Boom, Jump, StraightLeft, StraightRight, Blink,     // Ÿ��
    ItemCoin = 10,                                                              //������
    Player = 100                                                                  //�÷��̾�
}


public class Tile_Edit : MonoBehaviour
{
    [SerializeField]
    private Sprite[] tileImages;    // Ÿ�� �̹��� �迭
    [SerializeField]
    private Sprite[] itemImages;    // ������ �̹��� �迭
    [SerializeField]
    private Sprite playerImage;    // �÷��̾� �̹���

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

            // Ÿ�� (Empty, Base, Broke, Boom, Jump, StraightLeft, StraightRight, Blink)
            if ((int)TileType_Edit < (int)TileType_Edit.ItemCoin)
            {
                spriteRenderer.sprite = tileImages[(int)tileType];
            }
            // ������ (Coin)
            else if ((int)TileType_Edit < (int)TileType_Edit.Player)
            {
                spriteRenderer.sprite = itemImages[(int)tileType - (int)TileType_Edit.ItemCoin];
            }
            // �÷��̾� ĳ���� (�� �����Ϳ� �����ֱ� ���� ���� �Ͽ�����,
            // ������ �� ��ġ������ �����ϰ� �÷��̾� ��ġ�� Ÿ���� Empty�� ����
            else if ((int)TileType_Edit == (int)TileType_Edit.Player)
            {
                spriteRenderer.sprite = playerImage;
            }
        }
        get => tileType;
    }
}


