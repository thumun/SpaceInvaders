using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Missile : MonoBehaviour
{
    
    public float timer;
    public float tpTime = 3.0f;
	public AudioClip deathKnell;
    public GameObject missileBullet;

	private MeshRenderer mesh;
    private bool disable = false; 

    List<Vector3> possiblePositions = new List<Vector3>();

	// Start is called before the first frame update
	void Start()
    {
        //mesh = transform.GetComponent<MeshRenderer>();

        possiblePositions.Add(new Vector3(-15.6999998f, -4.5f, 0.0579999983f));
        possiblePositions.Add(new Vector3(-14.8999996f, 9.19999981f, 0.0579999983f));
		possiblePositions.Add(new Vector3(44f, 9.19999981f, 0.0579999983f));
		possiblePositions.Add(new Vector3(42f, -2f, 0.0579999983f));
		//possiblePositions.Add(new Vector3(25.7999992f, 20.5f, 0.0579999983f));
	}

    // Update is called once per frame
    void Update()
    {
        if (!disable)
        {
			timer += Time.deltaTime;

			if (timer > tpTime)
			{
				timer = 0;
                // teleport logic 

                int randNumm = Random.Range(0,possiblePositions.Count);
                this.transform.position = possiblePositions[randNumm];
			}
		}
       
	}

	private void OnCollisionEnter(Collision collision)
	{
		Collider collider = collision.collider;

		if (collider.CompareTag("Bullet"))
        {
			AudioSource.PlayClipAtPoint(deathKnell, gameObject.transform.position);
			disable = true; 

			//mesh.enabled = false;
			this.transform.position = new Vector3(12.6199999f, 38f, 0.0579999983f);

            initiateMissiles();
		}

	}

    private void initiateMissiles()
    {

		// instantiate 15 missiles on random places ontop of screen  
		for (int i = 0; i < 10; i++)
        {
			int rndNum = Random.Range(-6, 35);
			Instantiate(missileBullet, new Vector3((float)rndNum, 21.1000004f, 0f), Quaternion.identity);
        }

        Destroy(gameObject);
    }
}
