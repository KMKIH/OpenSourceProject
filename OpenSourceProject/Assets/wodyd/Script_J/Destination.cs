using UnityEngine;


public class Destination : Tile
{
    public static int maxStageCount;//�ִ� �������� ����

    
    private Tilemap2D tilemap2D;
    private void Awake()
    {
        tilemap2D = FindObjectOfType<Tilemap2D>();
        Debug.Log(tilemap2D.name);
    }
    public override void Collision(CollisionDirection direction)//�����ϸ� 
    {
      
        int index = PlayerPrefs.GetInt("StageIndex");
        if(tilemap2D.A == 1)//������ �� �𿴴ٸ� 
        {
            if (index < maxStageCount - 1)//���� ���������� ������ �� 
            {
                index++;
                PlayerPrefs.SetInt("StageIndex", index);

                SceneLoader.LoadScene();
              
            }
            else
            {
                SceneLoader.LoadScene("Intro");//intro������ ���Ŀ� Ending���� ��ü ����
            }

        }

    }
}