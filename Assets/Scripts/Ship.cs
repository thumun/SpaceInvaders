using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.Build;
using UnityEngine;

public class Ship : MonoBehaviour
{

	public Bullet bulletPrefab;

    public float speed;
	public bool noMovement;
	public bool noBullet = false;

    // Start is called before the first frame update
    void Start()
    {
		speed = 5.0f;
		noMovement = false;
    }

    // Update is called once per frame
    void Update()
    {

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
			if (Input.GetAxis("Vertical") > 0)
			{
				var b = Instantiate(bulletPrefab, transform.position, Quaternion.identity);
				b.dir = Vector3.up; // can change if can rotate ship 
				noBullet = true; 
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
