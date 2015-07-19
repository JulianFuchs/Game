using UnityEngine;
using System.Collections;

public class CreatureScript : MonoBehaviour {

    public int health = 100;
    public bool isAllied = false;
    public int pushBackIntensity = 10;
	public GameObject parent;
	public bool isAI = true;
    

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (health <= 0) {
			DestroyObject(parent);
        }
	}

    void OnTriggerEnter2D(Collider2D collider)
    {
        //Debug.Log("collision of " + this.gameObject.ToString() + " with " + collider.gameObject.ToString());
        if (collider.gameObject.GetComponent<BulletScript>() != null) //determine if this object got hit by a bullet
        {
            BulletScript bulletScript = collider.gameObject.GetComponent<BulletScript>();
            int dmg = bulletScript.damage;
            bool bulletFromEnemy = bulletScript.isEnemy;
            
            if (isAllied && bulletFromEnemy || !isAllied && !bulletFromEnemy)
            {
                health -= dmg;
                Debug.Log(this.gameObject.ToString() + " got hit and took " + dmg + " damage. Is now at " + health + " health");

				// if this creature is controlled by an AI, rotate in direction being shot from
				if (isAI)
				{
					// rotate towards target
					float xdiff = collider.gameObject.transform.position.x - parent.transform.position.x;
					float ydiff = collider.gameObject.transform.position.y - parent.transform.position.y;
					
					float angle = Mathf.Atan2(ydiff, xdiff);
					
					float compensate = -90f;
					
					parent.transform.rotation = Quaternion.AngleAxis(Mathf.Rad2Deg * angle + compensate, new Vector3(0, 0, 1)); 

				}
            }

			DestroyObject(collider.gameObject);
        }
    }
}
