using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissileBullet : MonoBehaviour
{
    public float speed;

    public bool disable=false;

	public GameObject deactivatedState;

	public AudioClip bulletSound;


	private bool initialCollision = true;

    // Start is called before the first frame update
    void Start()
    {
        speed = 15.0f;
    }

    // Update is called once per frame
    void Update()
    {

		transform.position += speed * Time.deltaTime * Vector3.down;
    }

	private void OnCollisionEnter(Collision collision)
	{

		//disable = true;

		Instantiate(deactivatedState, transform.position, Quaternion.identity);
		Destroy(this.gameObject);
		
	}
}
