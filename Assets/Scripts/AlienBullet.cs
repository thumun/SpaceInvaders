using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlienBullet : MonoBehaviour
{

    public Vector3 dir; 
    public float speed;

	public bool disable = false;
	private bool initialCollision = true;

	// Start is called before the first frame update
	void Start()
    {
        speed = 15.0f;
		dir = Vector3.down;
    }

    // Update is called once per frame
    void Update()
    {
        
        if (!disable)
        {
			transform.position += speed * Time.deltaTime * dir;
		}
        
    }

	private void OnCollisionEnter(Collision collision)
	{
		if (initialCollision)
		{
			// change color !!

			this.gameObject.GetComponent<Rigidbody>().useGravity = true;
			initialCollision = false;
		}

	}
}
