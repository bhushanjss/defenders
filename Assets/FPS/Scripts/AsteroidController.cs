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
    [SerializeField]
    private AsteroidsManager asteroidsManager;
    

    void Start()
    {
        asteroidsManager = GameObject.Find("AsteroidsManager").GetComponent<AsteroidsManager>();
        if(asteroidsManager == null)
        {
            Debug.LogError("The AsteroidsManager is null");
        }
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
            asteroidsManager.addMiss();
        }        
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Missile")
        {
            Destroy(other.gameObject);
            Destroy(this.gameObject);
            asteroidsManager.addHit();
        }
    }
}
