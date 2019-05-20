using UnityEngine;

public class CandySpawner : MonoBehaviour
{
    public GameObject normalCandy;
    public GameObject bonusCandy;
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
        InvokeRepeating("SpawnCandy", GameController.instance.GetCandySpawnTime(), GameController.instance.GetCandySpawnTime());
        InvokeRepeating("SpawnBonusCandy", GameController.instance.GetBonusCandySpawnTime(), GameController.instance.GetBonusCandySpawnTime());
    }

    private void Update()
    {        
        if (GameController.instance.IsGameover())
        {
            CancelInvoke("SpawnCandy");
        }
    }


    private void SpawnCandy()
    {
        if (GameController.instance.IsGameover() == false && GameController.instance.DifficultyChangeActive() == false)
        {
            int candyType = Random.Range(2, 5);
            Vector2 newPos = GetStartPosition();
            int lane = GetLane(newPos);
            GameObject candyObject;
            candyObject = Instantiate(normalCandy, newPos, normalCandy.transform.rotation) as GameObject;
            candyObject.GetComponent<Candy>().SetLane(lane);
            candyObject.GetComponent<Candy>().SetType(candyType);
            candyObject.GetComponentInChildren<SpriteRenderer>().color = SetColor(lane);
            candyObject.transform.SetParent(GameObject.FindGameObjectWithTag("Candies").transform, false);
            candyObject.GetComponentInChildren<SpriteRenderer>().sprite = SetCandyType(candyType);                  
            GameController.instance.AddActiveCandy();
        }
    }

    private void SpawnBonusCandy()
    {
        if (GameController.instance.IsGameover() == false && GameController.instance.DifficultyChangeActive() == false)
        {
            Vector2 newPos = GetStartPosition();
            int lane = GetLane(newPos);
            GameObject candyObject;
            candyObject = Instantiate(bonusCandy, newPos, bonusCandy.transform.rotation) as GameObject;
            candyObject.GetComponent<Candy>().SetLane(lane);
            candyObject.GetComponent<Candy>().SetType(1);
            candyObject.GetComponentInChildren<SpriteRenderer>().color = SetColor(lane);
            candyObject.transform.SetParent(GameObject.FindGameObjectWithTag("Candies").transform, false);
            candyObject.GetComponentInChildren<SpriteRenderer>().sprite = SetCandyType(1);
            GameController.instance.AddActiveCandy();
        }
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