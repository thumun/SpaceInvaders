using System.Collections;
using System.Collections.Generic;
//using TreeEditor;
//using Unity.VisualScripting;
//using UnityEditor.Build;
using UnityEngine;

public class Ship : MonoBehaviour
{

	public Bullet bulletPrefab;

    public float speed;
	public bool noMovement;
	public bool noBullet = false;

	public float bulletTimer = 0f;
	public float bulletSpawner = 0.4f;

	public GameObject particles;
	public GameObject cameraObj;

	public AudioClip deathKnell;
	public AudioClip bulletSound;

	GameObject mesh;
	//MeshRenderer mesh;

	public bool bulletHell;
	public float bulletHellTimer; 

	private float meshTimer; 
	private float objVisible = 1.0f;
	bool meshDisabled = false; 


	// Start is called before the first frame update
	void Start()
    {
		speed = 8.0f;
		noMovement = false;

		bulletHell = false;

		particles = transform.GetChild(1).gameObject;
		mesh = transform.GetChild(0).gameObject;

		bulletHellTimer = 2.0f; 
	}

    // Update is called once per frame
    void Update()
    {
		Debug.Log($"bullethell: {bulletHell}");
        if (meshDisabled)
		{
			meshTimer += Time.deltaTime; 

			if (objVisible < meshTimer)
			{
				enableMesh(); 
			}
		}

        bulletTimer += Time.deltaTime;

		if (!noMovement)
		{
			if (Input.GetAxis("Horizontal") > 0)
			{
				//Debug.Log($"Old Pos: {transform.position}");
				this.transform.position += Vector3.right * speed * Time.deltaTime;
				//Debug.Log($"DeltaTime: {Time.deltaTime}");
				//Debug.Log($"New Pos: {transform.position}");
			}
			else if (Input.GetAxis("Horizontal") < 0)
			{
				this.transform.position += Vector3.left * speed * Time.deltaTime;
				//Debug.Log(transform.position);
			}
		}

		if (!noBullet)
		{
			if (Input.GetAxis("Vertical") > 0 && bulletSpawner < bulletTimer && !bulletHell)
			{
				Vector3 newPos = transform.position;
				newPos.y = -10.7f;
				var b = Instantiate(bulletPrefab, newPos, Quaternion.identity);
				b.dir = Vector3.up; // can change if can rotate ship 
				noBullet = true;
				bulletTimer = 0f;

				AudioSource.PlayClipAtPoint(bulletSound, gameObject.transform.position);
			}
		}

		if (bulletHell /*&& bulletHellTimer > 0f*/)
		{
			if (bulletHellTimer > 0f)
			{

				if (Input.GetAxis("Vertical") > 0 && bulletSpawner < bulletTimer)
				{
					Vector3 newPos = transform.position;
					newPos.y = -10.7f;
					var b = Instantiate(bulletPrefab, newPos, Quaternion.identity);
					b.dir = Vector3.up; // can change if can rotate ship 
										//noBullet = true;
					bulletTimer = 0f;

					AudioSource.PlayClipAtPoint(bulletSound, gameObject.transform.position);

					bulletHellTimer -= Time.deltaTime;
				}
			}
			else
			{
				bulletHell = false;
				bulletHellTimer = 2.0f; 
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

		//Debug.Log(collider.tag);

		if (collider.CompareTag("AlienBullet") || collider.CompareTag("MissileBullet"))
		{
			AudioSource.PlayClipAtPoint(deathKnell, gameObject.transform.position);

			// life lost 
			GlobalTracker g = GameObject.Find("GlobalObj").GetComponent<GlobalTracker>();
			g.shipLifeController();

			// add camera shake
			// has explosion 
			StartCoroutine(Explosion());
		}

	}

	void enableMesh()
	{
		mesh.SetActive(true);
		particles.SetActive(false);
		meshDisabled = false;
		meshTimer = 0f; 
	}

	IEnumerator Explosion()
	{
		//particles.SetActive(false);
		//this.gameObject.SetActive(false);
		particles.SetActive(true);

		CameraShake c = cameraObj.GetComponent<CameraShake>();
		StartCoroutine(c.Shake(0.4f, 2.4f));

		mesh.SetActive(false);
		meshDisabled = true;

		// add sound effect here 

		yield return null;
	}
}
