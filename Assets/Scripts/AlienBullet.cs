using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlienBullet : MonoBehaviour
{

    public Vector3 dir; 
    public float speed;

	public GameObject deactivatedState;

	// Start is called before the first frame update
	void Start()
    {
        speed = 15.0f;
		dir = Vector3.down;
    }

    // Update is called once per frame
    void Update()
    {

		transform.position += speed * Time.deltaTime * dir;
        
    }

	private void OnCollisionEnter(Collision collision)
	{

		Instantiate(deactivatedState, transform.position, Quaternion.identity);
		Destroy(this.gameObject);

	}
}
