using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidsManager : MonoBehaviour
{
    [SerializeField]
    private GameObject asteroid1B;
    [SerializeField]
    private GameObject asteroid1C;
    [SerializeField]
    private GameObject asteroid1D;
    [SerializeField]
    private GameObject asteroid1E;
    [SerializeField]
    private GameObject asteroid4A;
    [SerializeField]
    private GameObject asteroid4B;
    [SerializeField]
    private GameObject asteroid5B;
    [SerializeField]
    private GameObject asteroid6A;
    [SerializeField]
    private GameObject asteroid6B;
    [SerializeField]
    private GameObject asteroid6C;
    [SerializeField]
    private GameObject asteroidContainer;

    private int _hit = 0;
    private int _miss = 0;

    [SerializeField]
    private float rangeXZ = 100f;
    [SerializeField]
    private float rangeMinY = 100f;
    [SerializeField]
    private float rangeMaxY = 500f;
    private GameObject[] asteroids;

    private ScoreDisplayManager scoreDisplayManager;

    // Start is called before the first frame update
    void Start()
    {
        scoreDisplayManager = GameObject.Find("ScoreCanvas").GetComponent<ScoreDisplayManager>();
        asteroids = new GameObject[]{ asteroid1B, asteroid1C, asteroid1D, asteroid1E,
            asteroid4A, asteroid4B, asteroid5B, asteroid6A, asteroid6B, asteroid6C };
        StartCoroutine(SpawnRoutine());      
    }

    IEnumerator SpawnRoutine()
    {
        while(true)
        {
            float randomPosX = Random.Range(-rangeXZ, rangeXZ);
            float randomPosZ = Random.Range(-rangeXZ, rangeXZ);
            float randomPosY = Random.Range(rangeMinY, rangeMaxY);
            Vector3 pos = new Vector3(randomPosX, randomPosY, randomPosZ);

            int i = Random.Range(0, 10);
            //GameObject asteroid = Instantiate(asteroids[i]);
            GameObject asteroid = Instantiate(asteroid1B);
            //asteroid.transform.SetParent(asteroidContainer.transform);
            asteroid.transform.Translate(pos);
            yield return new WaitForSeconds(5.0f);
        }        
    }

    public void addHit()
    {
        _hit++;
        scoreDisplayManager.UpdateScore(_hit, _miss);
    }

    public void addMiss()
    {
        _miss++;
        scoreDisplayManager.UpdateScore(_hit, _miss);
    }
}
