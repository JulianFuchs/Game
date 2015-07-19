using UnityEngine;
using System.Collections;

public class EnemyScript : MonoBehaviour {

	// public variables
	public GameObject parent;
	public GameObject weapon;
    public float fieldOfView = 90.0f;
	public CircleCollider2D viewSphere;

	// private variables
	private Animator animator;
	private bool allyInSight = false;

	// Use this for initialization
	void Start ()
	{
		animator = parent.GetComponent<Animator> ();
	}
	
	// Update is called once per frame
	void Update () {
	
	}


	void FixedUpdate()
	{
		if (allyInSight)
		{
			animator.SetBool("isWalking", false);
			animator.SetBool("isShooting", true);
		}

		else
		{
			animator.SetBool("isShooting",false);
		}
	}

    


	void OnTriggerStay2D(Collider2D collider)
	{
		if (collider.gameObject.GetComponent<CreatureScript> () != null) {

			CreatureScript creatureScript = collider.gameObject.GetComponent<CreatureScript> ();

			if (creatureScript.isAllied)
			{
				// an ally is in the viewSphere of this entity
				GameObject allyParent = creatureScript.parent;

                // check if the ally is in the Field of View of the entity
                Vector3 direction = allyParent.transform.position - parent.transform.position;
                float angleFOV = Vector3.Angle(direction, parent.transform.up);

				if (angleFOV < fieldOfView * 0.5f)
				{
					// if ally is in Field of View and leaves the

					Vector2 origin = new Vector2(parent.transform.position.x , parent.transform.position.y);
					Vector2 direction2d = new Vector2(direction.x, direction.y);

					//1.7f > 1.6f (x component of hitbox), 1.4f > 1.3f (y component of hitbox)
					origin += Mathf.Sqrt(Mathf.Pow(1.7f,2.0f) + Mathf.Pow(1.4f, 2.0f)) * direction2d.normalized;

					// we need to deactivate the circle collider (the possible view) of this entity, because otherwise raycast
					// doesnt work. We need to activate it later on again
					viewSphere.enabled = false;

					RaycastHit2D ray = Physics2D.Raycast(origin, direction2d.normalized, viewSphere.radius-0.1f); // -0.1f ???

					// check if the entity has direct vision of the enemy
					if (ray.collider != null)
					{
						CreatureScript targetCreatureScript = ray.collider.gameObject.GetComponent<CreatureScript> ();
						if (targetCreatureScript != null)
						{
							if (targetCreatureScript.isAllied)
							{								
								// rotate towards target
								float xdiff = allyParent.transform.position.x - parent.transform.position.x;
								float ydiff = allyParent.transform.position.y - parent.transform.position.y;
								
								float angle = Mathf.Atan2(ydiff, xdiff);
								
								float compensate = -90f;
								
								parent.transform.rotation = Quaternion.AngleAxis(Mathf.Rad2Deg * angle + compensate, new Vector3(0, 0, 1)); 
								
								WeaponScript weaponScript = weapon.GetComponent<WeaponScript> ();
								weaponScript.fire();

								allyInSight = true;
							}
						}
					}
					else
					{
						allyInSight = false;
					}

					// reactivate the view sphere
					viewSphere.enabled = true;

				}
				else
				{
					allyInSight = false;
				}
			}
		}
	}

}
