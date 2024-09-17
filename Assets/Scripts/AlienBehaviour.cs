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
    public float speed = 3.0f;

    float leftEdge = -11.2f;
    float rightEdge = 41.9f;

    public int alienDeathCount = 0;

    public GameObject alienBulletPrefab;

    public float timer;
    public float bulletSpawn = 2.0f; 

    //float leftEdge = Camera.main.ViewportToWorldPoint;
    //float rightEdge = 41.9f;


    // Start is called before the first frame update
    void Start()
    {
        rows = 5;
        cols = 11;

		Vector3 pos = this.transform.position;
        //Debug.Log(pos);


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
		//speed += transform.position.y;

		this.transform.position += direction * speed * Time.deltaTime;
		SpeedUp();


		Debug.Log($"pos: {transform.position}");
		//Debug.Log($"dir: {direction}");
		Debug.Log($"speed: {speed}");
        //Debug.Log($"time: {Time.deltaTime}");

        int indx = UnityEngine.Random.Range(0, 11);
        int i = 0; 

		foreach (Transform alien in transform)
        {

            Alien alienScript = alien.GetComponent<Alien>();
			if (alien.gameObject.activeInHierarchy)
            {
                if (alien.position.x >= rightEdge + 1.0f || alien.position.x <= leftEdge - 1.0f) 
                {
                    direction *= -1;
                    this.transform.position -= new Vector3(0f, 0.5f, 0f);

				}

				if (i < 11)
				{
                    if (i == indx && bulletSpawn <= timer)
                    {
						// randomly spawn bullets 
						Instantiate(alienBulletPrefab, transform.position, Quaternion.identity);
                        timer = 0; 
					}
					
				}
				

				i += 1;

			}

        }
    }

    // may need to change this
    private void SpeedUp()
    {
        speed += (float) 5.0 * (alienDeathCount / transform.childCount);
	}


}
