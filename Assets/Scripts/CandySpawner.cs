using UnityEngine;
using UnityEngine.UI;

public class CandySpawner : MonoBehaviour
{
    public int candyDifficulty = 1;
    public float spawnTime = 3f;
    public GameObject candy;
    public Sprite[] candyTypes;

    private int[,] spawnPoints = {
        {-400, -1058, 1},
        {-400, 1058, 1},
        {-125, -1058, 2},
        {-125, 1058, 2},
        {125, -1058, 3},
        {125, 1058, 3},
        {400, -1058, 4},
        {400, 1058, 4}
    };

    private void Start()
    {
        InvokeRepeating("SpawnCandy", spawnTime, spawnTime);        
    }

    private void Update()
    {        
        if (GameController.instance.IsGameOver())
        {
            CancelInvoke("SpawnCandy");
        }
    }


    private void SpawnCandy()
    {
        int candyType = getCandyType();        
        Vector2 newPos = GetStartPosition();                
        GameObject candyObject = Instantiate(candy, newPos, candy.transform.rotation) as GameObject;
        candyObject.transform.SetParent(GameObject.FindGameObjectWithTag("Canvas").transform, false);
        int lane = GetLane(newPos);
        candyObject.GetComponent<Candy>().SetLane(lane);
        candyObject.GetComponent<Candy>().SetType(getCandyType());
        candyObject.GetComponent<Image>().color = SetColor(lane);
        candyObject.GetComponent<Image>().sprite = SetCandyType(getCandyType());
    }

    private int getCandyType()
    {
        if (candyDifficulty == 1)
        {
            return Random.Range(1, 3);
        }
        else if (candyDifficulty == 2)
        {
            return Random.Range(2, 4);
        }
        return -1;
    }

    private Sprite SetCandyType(int candyType)
    {
        return candyTypes[candyType - 1];
    }

    private int GetLane(Vector2 newPos)
    {
        for (int i = 0; i < spawnPoints.GetLength(0); i++)
        {
            if (spawnPoints[i, 0] == newPos.x && spawnPoints[i, 1] == newPos.y)
            {
                return spawnPoints[i, 2];
            }
        }
        return -1;
    }

    private Color32 SetColor(int lane)
    {
        if (Random.Range(0f,1f) >= 0.5f)
        {            
            return GameController.instance.GetTopColor(lane);
        }
        else
        {
            return GameController.instance.GetBottomColor(lane);
        }
    }

    private Vector2 GetStartPosition()
    {
        int pos = Random.Range(0, spawnPoints.GetLength(0));
        return new Vector2(spawnPoints[pos, 0], spawnPoints[pos, 1]);
    }
}