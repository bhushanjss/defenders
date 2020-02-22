using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidController : MonoBehaviour
{
    [SerializeField]
    private float yThreshold = -100f;
    [SerializeField]
    private float _speed = 4.0f;
    [SerializeField]
    private float sizeRange = 10;
    private int _hit = 0;
    private int _miss = 0;

    void Start()
    {
        float randomSize = Random.Range(1f, sizeRange);
        transform.localScale = new Vector3(randomSize, randomSize, randomSize);
    }

    // Update is called once per frame
    void Update()
    {
        //transform.Translate(Vector3.down * _speed * Time.deltaTime);

        if(transform.position.y < yThreshold)
        {
            Destroy(this.gameObject);
            _miss++;
        }        
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Missile")
        {
            Destroy(other.gameObject);
            Destroy(this.gameObject);
            _hit++;
        }
    }
}
