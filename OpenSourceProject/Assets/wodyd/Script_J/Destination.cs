using UnityEngine;


public class Destination : Tile
{
    public static int maxStageCount;//최대 스테이지 개수

    
    private Tilemap2D tilemap2D;
    private void Awake()
    {
        tilemap2D = FindObjectOfType<Tilemap2D>();
        Debug.Log(tilemap2D.name);
    }
    public override void Collision(CollisionDirection direction)//접촉하면 
    {
      
        int index = PlayerPrefs.GetInt("StageIndex");
        if(tilemap2D.A == 1)//코인이 다 모였다면 
        {
            if (index < maxStageCount - 1)//다음 스테이지가 남았을 때 
            {
                index++;
                PlayerPrefs.SetInt("StageIndex", index);

                SceneLoader.LoadScene();
              
            }
            else
            {
                SceneLoader.LoadScene("Intro");//intro이지만 추후에 Ending으로 교체 가능
            }

        }

    }
}