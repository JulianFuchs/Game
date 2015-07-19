using UnityEngine;
using System.Collections;

public class LevelBarrierScript : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnTriggerEnter2D(Collider2D collider)
    {
        //Debug.Log(collider.ToString() + " entered barrier");
        if (collider.gameObject.GetComponent<BulletScript>() != null) //determine if a bullet reached the barrier
        {
            Destroy(collider.gameObject);
        }
    
    }
}
