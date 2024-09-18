using System.Collections;
using System.Collections.Generic;
using TreeEditor;
using Unity.VisualScripting;
using UnityEditor.Build;
using UnityEngine;

public class Ship : MonoBehaviour
{

	public Bullet bulletPrefab;

    public float speed;
	public bool noMovement;
	public bool noBullet = false;

	public float bulletTimer = 0f;
	public float bulletSpawner = 0.4f;

    // Start is called before the first frame update
    void Start()
    {
		speed = 5.0f;
		noMovement = false;
    }

    // Update is called once per frame
    void Update()
    {

		bulletTimer += Time.deltaTime;

		if (!noMovement)
		{
			if (Input.GetAxis("Horizontal") > 0)
			{
				Debug.Log($"Old Pos: {transform.position}");
				this.transform.position += Vector3.right * speed * Time.deltaTime;
				Debug.Log($"DeltaTime: {Time.deltaTime}");
				Debug.Log($"New Pos: {transform.position}");
			}
			else if (Input.GetAxis("Horizontal") < 0)
			{
				this.transform.position += Vector3.left * speed * Time.deltaTime;
				Debug.Log(transform.position);
			}
		}

		if (!noBullet)
		{
			if (Input.GetAxis("Vertical") > 0 && bulletSpawner < bulletTimer)
			{
				Vector3 newPos = transform.position;
				newPos.y = -11f;
				var b = Instantiate(bulletPrefab, newPos, Quaternion.identity);
				b.dir = Vector3.up; // can change if can rotate ship 
				noBullet = true;
				bulletTimer = 0f; 
			}
		}

		// can't move past edges logic add here

	}

	// move code here for physics (?)
	private void FixedUpdate()
	{

	}

	private void OnCollisionEnter(Collision collision)
	{
		Collider collider = collision.collider;

		Debug.Log(collider.tag);

		if (collider.CompareTag("AlienBullet"))
		{
			// has explosion 

			// life lost 
			GlobalTracker g = GameObject.Find("GlobalObj").GetComponent<GlobalTracker>();
			g.shipLifeController();

			// add camera shake
		}

	}
}
