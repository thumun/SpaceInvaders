using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{

    public Vector3 dir; 
    public float speed;

    public bool disable=false;

    public GameObject shipRef;

    private bool initialCollision = true;

    // Start is called before the first frame update
    void Start()
    {
        speed = 15.0f;
        shipRef = GameObject.Find("Ship");
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

			disable = true;
			shipRef.GetComponent<Ship>().noBullet = false;
			this.gameObject.GetComponent<Rigidbody>().useGravity = true;

            initialCollision = false;
		}

	}
}
