using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Script : MonoBehaviour
{
    public GameObject particlePrefab;
    public GameObject expPrefab;
    [SerializeField]
    private int points;
    private Game_Controller controller_object;


    // Start is called before the first frame update
    void Start()
    {
        controller_object = GetComponentInParent<Game_Controller>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "bullet")
        {
            controller_object.addScore(points);

            GameObject particle = Instantiate(particlePrefab);
            particle.transform.SetParent(transform.parent.parent);
            particle.transform.position = transform.position;

            GameObject explosion = Instantiate(expPrefab);
            explosion.transform.SetParent(transform.parent.parent);
            explosion.transform.position = transform.position;

            Destroy(particle, 1.5f);
            Destroy(explosion, 1.5f);
            Destroy(collision.gameObject);
            Destroy(gameObject);
        }
        if (collision.tag == "powerBullet")
        {
            controller_object.addScore(points);

            GameObject particle = Instantiate(particlePrefab);
            particle.transform.SetParent(transform.parent.parent);
            particle.transform.position = transform.position;

            GameObject explosion = Instantiate(expPrefab);
            explosion.transform.SetParent(transform.parent.parent);
            explosion.transform.position = transform.position;

            Destroy(particle, 1.5f);
            Destroy(explosion, 1.5f);
            Destroy(gameObject);
        }
    }
}
