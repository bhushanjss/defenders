using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidController : MonoBehaviour
{
    [SerializeField]
    private float _speed = 4.0f;
    [SerializeField]
    private float rangeXZ= 100f;
    [SerializeField]
    private float rangeMinY = 100f;
    [SerializeField]
    private float rangeMaxY = 500f;
    private float sizeRange = 10;
    void Start()
    {
        float randomPosX = Random.Range(-rangeXZ, rangeXZ);
        float randomPosZ = Random.Range(-rangeXZ, rangeXZ);
        float randomPosY = Random.Range(rangeMinY, rangeMaxY);
        float randomSize = Random.Range(1f, sizeRange);
        transform.position = new Vector3(randomPosX, randomPosY, randomPosZ);
        transform.localScale = new Vector3(randomSize, randomSize, randomSize);
    }

    // Update is called once per frame
    void Update()
    {
        //transform.Translate(Vector3.down * _speed * Time.deltaTime);
        
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Hit: "+ other.tag);

        if(other.tag == "Missile")
        {
            Destroy(other.gameObject);
            Destroy(this.gameObject);
        }
    }
}
