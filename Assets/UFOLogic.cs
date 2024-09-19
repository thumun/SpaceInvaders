using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UFOLogic : MonoBehaviour
{
	public int scoreVal = 100;

	public bool initialCollision = true;
	public float speed = 5.0f; 

	public bool isDead = false;

	GlobalTracker g;

	// Start is called before the first frame update
	void Start()
    {
		g = GameObject.FindObjectOfType<GlobalTracker>();
	}

    // Update is called once per frame
    void Update()
    {
        if (!isDead)
		{
			this.transform.position += Vector3.right * speed * Time.deltaTime;
		}

		if (transform.position.x > 74.0f)
		{
			g.ufoActive = false; 
			Destroy(gameObject);
		}
    }

	private void OnCollisionEnter(Collision collision)
	{
		Collider collider = collision.collider;

		if (collider.CompareTag("Bullet") && initialCollision)
		{
			isDead = true;

			// change color 
			this.gameObject.GetComponent<Rigidbody>().useGravity = true;
			this.gameObject.GetComponent<Rigidbody>().constraints &= ~RigidbodyConstraints.FreezePosition;

			initialCollision = false;

			g.currScore += scoreVal;
			g.ufoActive = false;
		}
	}
}
