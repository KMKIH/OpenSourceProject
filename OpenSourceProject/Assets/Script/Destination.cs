using UnityEngine;


public class Destination : Tile
{
    private int maxStageCount;//최대 스테이지 개수
    private int stageIdx;
    
    private Tilemap2D tilemap2D;
    private void Awake()
    {
        maxStageCount = StageDataController.TotalStageNum;
        stageIdx = StageDataController.StageIdx;
        tilemap2D = FindObjectOfType<Tilemap2D>();
    }
    public override void Collision(CollisionDirection direction)//접촉하면 
    {
        if (stageIdx < maxStageCount - 1)//다음 스테이지가 남았을 때 
        {
            ClearWindowController.Instance.Clear(tilemap2D.maxCoinCount - tilemap2D.currentCoinCount,tilemap2D.maxCoinCount);
        }
        else
        {
            ClearWindow_lastController.Instance.Clear(tilemap2D.maxCoinCount - tilemap2D.currentCoinCount, tilemap2D.maxCoinCount);
        }
        // collider off
        GetComponent<BoxCollider2D>().enabled = false;
    }
}