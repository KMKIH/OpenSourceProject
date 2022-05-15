using UnityEngine;


public enum TileType
{
    Empty = 0, Base, Broke, Boom, Jump, StraightLeft, StraightRight, Blink,     // Ÿ��
    ItemCoin = 10,                                                              //������
    Player = 100                                                                  //�÷��̾�
}


public class Tile : MonoBehaviour
{
    [SerializeField]
    private Sprite[] tileImages;    // Ÿ�� �̹��� �迭
    [SerializeField]
    private Sprite[] itemImages;    // ������ �̹��� �迭
    [SerializeField]
    private Sprite playerImage;    // �÷��̾� �̹���

    private TileType tileType;

    private SpriteRenderer spriteRenderer;

    public void Setup(TileType tileType)
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        TileType = tileType;
    }

    public TileType TileType
    {
        set
        {
            tileType = value;

            // Ÿ�� (Empty, Base, Broke, Boom, Jump, StraightLeft, StraightRight, Blink)
            if ((int)TileType < (int)TileType.ItemCoin)
            {
                spriteRenderer.sprite = tileImages[(int)tileType];
            }
            // ������ (Coin)
            else if ((int)TileType < (int)TileType.Player)
            {
                spriteRenderer.sprite = itemImages[(int)tileType - (int)TileType.ItemCoin];
            }
            // �÷��̾� ĳ���� (�� �����Ϳ� �����ֱ� ���� ���� �Ͽ�����,
            // ������ �� ��ġ������ �����ϰ� �÷��̾� ��ġ�� Ÿ���� Empty�� ����
            else if ((int)TileType == (int)TileType.Player)
            {
                spriteRenderer.sprite = playerImage;
            }
        }
        get => tileType;
    }
}


