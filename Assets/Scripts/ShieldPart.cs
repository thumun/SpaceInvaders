using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldPart : MonoBehaviour
{
	bool initialCollision = true;
	public GameObject brokenPrefab;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

	private void OnCollisionEnter(Collision collision)
	{
		Collider collider = collision.collider;

		if (initialCollision && (collider.CompareTag("AlienBullet") || collider.CompareTag("Bullet")))
		{
			Instantiate(brokenPrefab, this.transform.position, Quaternion.identity);
			this.gameObject.SetActive(false);

			initialCollision = false;
		}

	}
}
