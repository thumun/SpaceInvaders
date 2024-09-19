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

	private float ratio = 0.0f; 

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
						this.transform.position -= new Vector3(0f, 1.0f, 0f);
						this.transform.position = new Vector3(transform.position.x - 1.0f, transform.position.y, transform.position.z);
						isDropped = true;
					}
					else if (alien.position.x <= leftEdge)
					{
						direction *= -1;

						this.transform.position -= new Vector3(0f, 1.0f, 0f);
                        this.transform.position = new Vector3(transform.position.x + .0f, transform.position.y, transform.position.z);
						isDropped = true;
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

		if (ratio <= (float)(transform.childCount - aliveAliens)/ (float) transform.childCount)
        {
            speed += 0.001f;
            ratio = (transform.childCount - aliveAliens) / transform.childCount;
		}

	}


}
