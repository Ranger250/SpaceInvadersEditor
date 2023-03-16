using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bunker : MonoBehaviour
{

    public Transform[] blocks;
    private int hits;
    private bool hit;
    private float hit_counter;
    public float counter_length;

    // Start is called before the first frame update
    void Start()
    {
        counter_length = 1f;
        hit_counter = counter_length;
        hit = false;
        hits = 0;
        blocks = GetComponentsInChildren<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        //blocks = GetComponentsInChildren<Transform>();
        if (hit)
        {
            hit_counter -= Time.deltaTime;

            if (hit_counter <= 0)
            {
                hit = false;
                hit_counter = counter_length;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "enemyBullet" || collision.tag == "bullet")
        {
            Destroy(collision.gameObject);
            if (!hit)
            {
                hit = true;
                hits++;
                if (hits >= 3)
                {
                    Destroy(gameObject);
                }
                else
                {
                    
                    for (int i = 30; i > 0; i--)
                    {
                        int index = Random.Range(1, blocks.Length);
                        Destroy(blocks[index].gameObject);
                        blocks = GetComponentsInChildren<Transform>();
                    }
                }
            }
        

        }

    }

}
