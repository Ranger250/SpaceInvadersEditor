using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Controller : MonoBehaviour
{

    public float speed;
    public Rigidbody2D rig;
    public float plimit;
    public GameObject bulletPrefab;
    public GameObject pBulletPrefab;
    public bool fired;
    public float bulletVel;

    public float coolDown;
    public float coolDownDir;


    public GameObject particlePrefab;
    public GameObject expPrefab;

    public Game_Controller controller_object;

    public Vector3 startPos;
    public Vector3 hidePos;
    public float hidetime = 3f;
    public float hideTimer;
    public bool dead;

    public bool pFired;
    public float pBulletVel;
    public float pCoolDownDir;
    public float pCoolDown;


    // Start is called before the first frame update
    void Start()
    {
        dead = false;
        hideTimer = hidetime;
        startPos = transform.position;
        hidePos = startPos + Vector3.up * 100;
        coolDownDir = 1.2f;
        rig = GetComponent<Rigidbody2D>();
        plimit = 3.6f;
    }


    private void FixedUpdate()
    {
        rig.velocity = new Vector2(
            Input.GetAxis("Horizontal") * speed, // x velocity
            0 // y velocity
            );
    }

    // Update is called once per frame
    void Update()
    {
        coolDown -= Time.deltaTime;
        pCoolDown -= Time.deltaTime;

        if (dead)
        {
            hideTimer -= Time.deltaTime;
            if(hideTimer <= 0)
            {
                dead = false;
                hideTimer = hidetime;
                transform.position = startPos;
            }
        }

        //check if the player is past the limit
        //if so move player to the limit
        if (transform.position.x > plimit)
        {
            transform.position = new Vector3(plimit, transform.position.y, transform.position.z);
        }
        else if (transform.position.x < -plimit)
        {
            transform.position = new Vector3(-plimit, transform.position.y, transform.position.z);
        }

        if (Input.GetAxis("Jump") == 1f)
        {
            if (!fired && coolDown <= 0)
            {
                coolDown = coolDownDir;
                fired = true;
                GameObject bullet = Instantiate(bulletPrefab);
                bullet.transform.SetParent(transform);
                bullet.transform.position = new Vector3(transform.position.x, transform.position.y + .2f, transform.position.z);
                    //transform.position;
                bullet.GetComponent<Rigidbody2D>().velocity = new Vector2(0, bulletVel);
                Destroy(bullet, 2f);
            }

        }
        else
        {
            fired = false;
        }

        if (Input.GetKeyDown(KeyCode.X))
        {
            if (!pFired && pCoolDown <= 0)
            {
                pCoolDown = pCoolDownDir;
                pFired = true;
                GameObject powerBullet = Instantiate(pBulletPrefab);
                powerBullet.transform.SetParent(transform);
                powerBullet.transform.position = new Vector3(transform.position.x, transform.position.y + .2f, transform.position.z);
                powerBullet.GetComponent<Rigidbody2D>().velocity = new Vector2(0, pBulletVel);
                Destroy(powerBullet, 2f);
            }
        }
        else
        {
            pFired = false;
        }
    }

    public void onDeath()
    {
        controller_object.player_die();
        dead = true;
        

        GameObject particle = Instantiate(particlePrefab);
        particle.transform.position = transform.position;

        GameObject explosion = Instantiate(expPrefab);
        explosion.transform.position = transform.position;

        Destroy(particle, 1.5f);
        Destroy(explosion, 1.5f);

        transform.position = hidePos;

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag != "bullet" && collision.tag != "powerBullet" )
        {
            onDeath();
            Destroy(collision.gameObject);
        }
    }

}
