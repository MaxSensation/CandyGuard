using UnityEngine;
using UnityEngine.UI;

public class CandySpawner : MonoBehaviour
{
    public GameObject candy;
    public float spawnTime = 1f;
    private int[,] spawnPoints = {
        {-400, -1058},
        {-200, -1058},
        {200, -1058},
        {400, -1058},
        {-400, 1058},
        {-200, 1058},
        {200, 1058},
        {400, 1058}
    };

    private void Start()
    {        
        InvokeRepeating("SpawnCandy", spawnTime, spawnTime);
    }

    private void SpawnCandy()
    {        
        GameObject candyObject = Instantiate(candy, GetStartPosition(), candy.transform.rotation) as GameObject;
        candyObject.transform.SetParent(GameObject.FindGameObjectWithTag("Canvas").transform, false);
    }

    private Vector2 GetStartPosition()
    {
        int pos = Random.Range(0, spawnPoints.GetLength(0));
        Debug.Log("X:" + pos + ", 0");
        Debug.Log("Y:" + pos + ", 1");
        int x = spawnPoints[pos, 0];
        int y = spawnPoints[pos, 1];
        return new Vector2(x, y);
    }
}