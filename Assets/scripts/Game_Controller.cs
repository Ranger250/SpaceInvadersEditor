using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class Game_Controller : MonoBehaviour
{
    Enemy_Script[] enemies;
    Enemy_Script rando;

    //shoot
    public float shootInterval = 3f;
    public float shootingSpeed = 2f;
    public GameObject enemyBulletPrefab;
    private float shootTimer;



    //move
    public float moveInterval;
    public float moveDistance = .25f;
    [SerializeField]
    private float elimit = 3.6f;
    [SerializeField]
    private float movingDir = 1;
    private float movingTimer;
    private float rightmostPosition = 0f;
    private float leftmostPosition = 0f;



    //diff
    public float maxMoveInterval = .4285f;
    public float minMoveInterval = .05f;
    public int enemyCount;
    public int maxEnemies = 55;

    //ui elemments
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI waveText;
    public TextMeshProUGUI livesText;

    private int score;
    private int wave;
    private int lives;


    // Start is called before the first frame update
    void Start()
    {
        //get score lives and wave from last level
        if (PlayerPrefs.HasKey("score"))
        {
            score = PlayerPrefs.GetInt("score");
            wave = PlayerPrefs.GetInt("wave");
            lives = PlayerPrefs.GetInt("lives");
        }
        else
        {
            score = 0;
            wave = 1;
            lives = 3;

        }

        update_ui();


        shootTimer = shootInterval;
        movingTimer = maxMoveInterval;
        enemies = GetComponentsInChildren<Enemy_Script>();
        enemyCount = enemies.Length;
    }

    public void addScore(int points)
    {
        score += points;
        update_ui();
    }

    public void player_die()
    {
        lives--;
        update_ui();
        save_data();
        if (lives <= 0)
        {
            //clear_data();
            SceneManager.LoadScene("GameOver");
        }
    }

    public void update_ui()
    {
        scoreText.text = score.ToString();
        waveText.text = wave.ToString();
        livesText.text = "Lives: " + lives.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        enemies = GetComponentsInChildren<Enemy_Script>();
        enemyCount = enemies.Length;

        if (enemyCount > 0)
        {



            //move logic________________
            // moving left and right
            movingTimer -= Time.deltaTime;
            if (movingTimer <= 0)
            {
                float diffsetting = 1f - (float)enemyCount / maxEnemies;
                moveInterval = maxMoveInterval - (maxMoveInterval - minMoveInterval) * diffsetting;
                movingTimer = moveInterval;
                transform.position = new Vector3(transform.position.x + (moveDistance * movingDir),
                    transform.position.y,
                    0f);


                if (movingDir != 0)
                {
                    foreach (Enemy_Script enemy in enemies)
                    {
                        if (enemy.transform.position.x < leftmostPosition)
                        {
                            leftmostPosition = enemy.transform.position.x;
                        }
                        if (enemy.transform.position.x > rightmostPosition)
                        {
                            rightmostPosition = enemy.transform.position.x;
                        }
                    }

                    if (rightmostPosition >= elimit || leftmostPosition <= -elimit)
                    {
                        movingDir *= -1;
                        rightmostPosition = 0f;
                        leftmostPosition = 0f;
                        transform.position = new Vector3(transform.position.x,
                        transform.position.y - moveDistance,
                        0f);
                    }
                }
            }




            shootTimer -= Time.deltaTime;
            if (shootTimer <= 0)
            {
                shootInterval = Random.Range(2, 6);
                shootingSpeed = Random.Range(2, 5);
                shootTimer = shootInterval;
                rando = enemies[Random.Range(0, enemies.Length)];
                GameObject enemyBullet = Instantiate(enemyBulletPrefab);
                enemyBullet.transform.position = rando.transform.position;
                enemyBullet.GetComponent<Rigidbody2D>().velocity = new Vector2(0, -shootingSpeed);
                Destroy(enemyBullet, 6f);
            }
        }
        else
        {
            wave++;
            save_data();

            SceneManager.LoadScene("SampleScene");
        }

    }

    public void save_data()
    {
        PlayerPrefs.SetInt("score", score);
        PlayerPrefs.SetInt("wave", wave);
        PlayerPrefs.SetInt("lives", lives);
    }
    public void clear_data()
    {
        PlayerPrefs.DeleteKey("score");
        PlayerPrefs.DeleteKey("wave");
        PlayerPrefs.DeleteKey("score");
    }








}
