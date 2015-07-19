using UnityEngine;
using System.Collections;

public class WeaponScript : MonoBehaviour {

    public int damage = 10;
    public float fireRate = 0.1f; // Firerate = Shots per Sec
    public float bulletSpeed = 1000f;
    public bool isEnemy = true;
    public GameObject owner;
    public GameObject bulletType;

    private float nextFire = 0.0f; 

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void fire()
    {
        if (Time.time > nextFire)
        {
            //owner.transform.rotation
            GameObject newBullet = (GameObject)Instantiate(bulletType);
			newBullet.GetComponent<BoxCollider2D>().enabled = true;
			//Debug.Log(this.transform.position.z);

            newBullet.transform.position = this.transform.position;
            float zRotation = owner.transform.rotation.eulerAngles.z + 90.0f;
            float x = Mathf.Cos(Mathf.Deg2Rad * zRotation);
            float y = Mathf.Sin(Mathf.Deg2Rad * zRotation);
            Vector2 direction = new Vector2(x, y);
            //direction.
            newBullet.GetComponent<Rigidbody2D>().AddForce(bulletSpeed * direction);
            newBullet.GetComponent<SpriteRenderer>().enabled = true;

            nextFire = Time.time + fireRate;
        }
    }
}
