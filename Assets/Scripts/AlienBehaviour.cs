using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class AlienBehaviour : MonoBehaviour
{

    public Alien[] aliens;

    public int rows; 
    public int cols;

    Vector3 direction = Vector3.right;
    public float speed = 15.0f;

    float leftEdge = -11.2f;
    float rightEdge = 41.9f;

    public int alienDeathCount = 0;

    public GameObject alienBulletPrefab;

    public float timer;
    public float bulletSpawn = 0.1f;

	public AudioClip bulletSound;

	private float ratio = 0f;

    public bool activate = true;
    public Dictionary<int, int> alienTracker = new Dictionary<int, int>();

    // Start is called before the first frame update
    void Start()
    {
        rows = 5;
        cols = 11;

		Vector3 pos = this.transform.position;

        for (int i = 0; i < rows; i++)
        {
            pos.y += (2.0f) /*+ (i * 2.0f)*/;

            for (int j = 0; j < cols; j++)
            {
                pos.x = j + (j * 2.0f);
                var alien = Instantiate(aliens[i], pos, Quaternion.identity);
                alien.transform.parent = this.transform; // adding as child 

                Alien a = alien.GetComponent<Alien>();
                a.identifier = i;
                if (i == 0 || i == 1)
                {
                    a.scoreVal = 10;
                }
                else if (i == 2 || i == 3)
                {
                    a.scoreVal = 20; 
                }
                else
                {
                    a.scoreVal = 30;
                }
            }
        }

        for (int j = 0; j < 11; j++)
        {
            alienTracker[j] = 0;
        }
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;

        this.transform.position += direction * speed * Time.deltaTime;

        int indx = UnityEngine.Random.Range(0, 11);
        //Debug.Log($"index: {indx}");
        int i = 0;

        bool isDropped = false;
        int aliveAliens = 0;

        if (activate)
        {

            foreach (Transform alien in transform)
            {

                Alien alienScript = alien.GetComponent<Alien>();
                if (!alienScript.isDead)
                {
                    aliveAliens++;
                    if (!isDropped)
                    {
                        if (alien.position.x >= rightEdge)
                        {
                            direction *= -1;

                            //direction *= -1;
                            //Debug.Log($"old pos  - right edge: {transform.position}");
                            this.transform.position -= new Vector3(0f, 1.0f, 0f);
                            this.transform.position = new Vector3(transform.position.x - 1.0f, transform.position.y, transform.position.z);
							//Debug.Log($"new pos - right edge: {transform.position}");

							isDropped = true;
                        }
                        else if (alien.position.x <= leftEdge)
                        {
                            direction *= -1;

							//Debug.Log($"old pos  - left edge: {transform.position}");
							this.transform.position -= new Vector3(0f, 1.0f, 0f);
                            this.transform.position = new Vector3(transform.position.x + 1.0f, transform.position.y, transform.position.z);
							//Debug.Log($"new pos  - left edge: {transform.position}");
							isDropped = true;
                        }

                        if (alien.position.y <= -9.0f)
                        {
                            // game over 
                            GlobalTracker g = GameObject.FindObjectOfType<GlobalTracker>();
                            activate = false;
                            g.GameOver(false);
                        }
                    }

                    if (i < 11)
                    {
                        if (i == indx && bulletSpawn <= timer)
                        {
                            AudioSource.PlayClipAtPoint(bulletSound, gameObject.transform.position);
                            // randomly spawn bullets 
                            Instantiate(alienBulletPrefab, new Vector3(alien.transform.position.x, transform.position.y, transform.position.z), Quaternion.identity);
                            timer = 0;
                        }

                    }


                    i += 1;

                }

            }

            for (int j = 0; j < 11; j++)
            {
                if (alienTracker[j] == 4)
                {
                    // bullet hell activate
                    Ship s = GameObject.FindObjectOfType<Ship>();
                    s.bulletHell = true; 
                    alienTracker[j]++;
                }
            }

            //Debug.Log($"childcount: {((float)(transform.childCount - aliveAliens) / (float)transform.childCount)}");
            //Debug.Log($"ratio: {ratio}");

            if (ratio < (float)(transform.childCount - aliveAliens) / (float)transform.childCount)
            {
                speed += 0.005f;
                ratio = (transform.childCount - aliveAliens) / transform.childCount;

                //Debug.Log($"speed: {speed}");
            }

        }
    }

}
