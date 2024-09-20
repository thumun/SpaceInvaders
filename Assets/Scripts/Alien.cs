using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Alien : MonoBehaviour
{
    //public System.Action alienDeath;
    public int scoreVal = 10;

    public bool initialCollision = true;

    public bool isDead = false;

	public AudioClip deathKnell;

    public int identifier;

    AlienBehaviour aBehave;

	// Start is called before the first frame update
	void Start()
    {
		aBehave = GameObject.FindObjectOfType<AlienBehaviour>();

	}

	// Update is called once per frame
	void Update()
    {
        
    }

	private void OnCollisionEnter(Collision collision)
	{
		Collider collider = collision.collider;

		if ((collider.CompareTag("Bullet") || collider.CompareTag("MissileBullet")) && initialCollision)
		{
			AudioSource.PlayClipAtPoint(deathKnell, gameObject.transform.position);

			this.gameObject.tag = "DeadAlien";

			aBehave.alienTracker[identifier] += 1;

			isDead = true;
			//alienDeath.Invoke();
			AlienBehaviour a = GameObject.Find("Aliens").GetComponent<AlienBehaviour>();
            a.alienDeathCount++;

            // change color 
			this.gameObject.GetComponent<Rigidbody>().useGravity = true;
            this.gameObject.GetComponent<Rigidbody>().constraints &= ~RigidbodyConstraints.FreezePosition;

            initialCollision = false;

            GlobalTracker g = GameObject.FindObjectOfType<GlobalTracker>();
            g.currScore += scoreVal;
		}
	}
}
