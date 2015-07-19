using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PlayerScript : MonoBehaviour
{
	// public variables
    public int rotationSpeed = 1;
    public int movementSpeed = 15;
    public GameObject weapon;
    public GameObject parent;

    public Text textBox;
    public GameObject mainCamera;

	// private variables
	Animator animator;

    // Use this for initialization
    void Start()
    {
		//animator = this.GetComponent<Animator> ();
		animator = parent.GetComponent<Animator> ();
    }

    // Update is called once per frame
    void Update()
    {
        //if (player == null)
          //  DestroyObject(this.gameObject);
    }

    void FixedUpdate()
    {
        #region Rotation Control Xbox
		/*
        float fourthAxis = Input.GetAxis("Axis4");
        float fifthAxis = -Input.GetAxis("Axis5");

        int compensate = 0;

        if (fourthAxis <= 0 && fifthAxis <= 0)
            compensate = 360;
        if (fourthAxis >= 0 && fifthAxis <= 0)
            compensate = 360;

		compensate += -90;
        //textBox.text = "Angle: " + (Mathf.Rad2Deg*Mathf.Atan2(fifthAxis,fourthAxis) + compensate);

        if (Mathf.Pow(fourthAxis,2f) + Mathf.Pow(fifthAxis, 2f) > 0.5)
            transform.rotation = Quaternion.AngleAxis(Mathf.Rad2Deg * Mathf.Atan2(fifthAxis, fourthAxis) + compensate, new Vector3(0, 0, 1));
		*/
        #endregion

		#region Movement Control Xbox
		/*
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");
        bool runningPressed = Input.GetButton("Left Button");

		Debug.Log("moveHorizontal: " + moveHorizontal + "; moveVertical: " + moveVertical);

        int actualSpeed = movementSpeed;

        if (runningPressed)
            actualSpeed = 2 * movementSpeed;

        Vector2 force = new Vector2(moveHorizontal, moveVertical);
        this.GetComponent<Rigidbody2D>().AddForce(actualSpeed*force);

        mainCamera.transform.position = new Vector3(this.transform.position.x, this.transform.position.y, -10);

		if (moveHorizontal != 0 || moveVertical != 0)
			animator.SetBool("isWalking",true);
		else
			animator.SetBool("isWalking",false);
		*/
		#endregion

		#region Weapon Control Xbox
		/*
        float firingRightTrigger = Input.GetAxis("Right Trigger");
        float firingLeftTrigger = Input.GetAxis("Left Trigger");

        if (firingRightTrigger > 0)
        {
            ((WeaponScript)weapon.GetComponent<WeaponScript>()).fire();
			animator.SetBool("isWalking",false);
			animator.SetBool("isShooting",true);
        }

		if (firingRightTrigger == 0)
		{
			animator.SetBool("isShooting",false);
		}
		*/
		#endregion

		#region Rotation Control 
		//float xPos =  this.transform.position.x;
		//float yPos = this.transform.position.y;
		float xPos =  parent.transform.position.x;
		float yPos = parent.transform.position.y;

		float xMouse = Camera.main.ScreenToWorldPoint(Input.mousePosition).x;
		float yMouse = Camera.main.ScreenToWorldPoint(Input.mousePosition).y;

		/*
		Debug.Log("player Position: (" + xPos + "," + yPos + "); mouse position: (" + xMouse + "," + yMouse + ")");
		Debug.Log(Mathf.Rad2Deg * angle);
		*/

		float angle = Mathf.Atan2(yMouse-yPos, xMouse-xPos);

		float compensate = -90f;

		parent.transform.rotation = Quaternion.AngleAxis(Mathf.Rad2Deg * angle + compensate, new Vector3(0, 0, 1));

		#endregion

		#region Movement Control
		float moveHorizontal = Input.GetAxis("Horizontal");
		float moveVertical = Input.GetAxis("Vertical");
		bool runningPressed = Input.GetButton("Sprinting");

		int actualSpeed = movementSpeed;
		
		if (runningPressed)
			actualSpeed = 2 * movementSpeed;

		if (moveHorizontal != 0 || moveVertical != 0)
		{
			moveHorizontal = moveHorizontal / (Mathf.Abs(moveHorizontal) + Mathf.Abs(moveVertical));
			moveVertical = moveVertical / (Mathf.Abs(moveHorizontal) + Mathf.Abs(moveVertical));

			Vector2 force = new Vector2(moveHorizontal, moveVertical);
			//this.GetComponent<Rigidbody2D>().AddForce(actualSpeed*force);
			parent.GetComponent<Rigidbody2D>().AddForce(actualSpeed*force);
		}

		//mainCamera.transform.position = new Vector3(this.transform.position.x, this.transform.position.y, -10);
		mainCamera.transform.position = new Vector3(parent.transform.position.x, parent.transform.position.y, -10);

		if (moveHorizontal != 0 || moveVertical != 0)
			animator.SetBool("isWalking",true);
		else
			animator.SetBool("isWalking",false);

		#endregion

		#region Weapon Control

		if (Input.GetButton("Fire1"))
		{
			((WeaponScript)weapon.GetComponent<WeaponScript>()).fire();
			animator.SetBool("isWalking",false);
			animator.SetBool("isShooting",true);
		}
		
		else
		{
			animator.SetBool("isShooting",false);
		}

		#endregion
        


    }

}


