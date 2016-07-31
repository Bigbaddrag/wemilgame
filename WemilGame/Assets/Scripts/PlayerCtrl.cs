using UnityEngine;
using System.Collections;

public class PlayerCtrl : MonoBehaviour {
	
	public float movespeed = 6.0f;
	public float jumpForce = 600.0f;
	public Transform groundCheck;

	private float snapDistance = 1.5f;
	private Vector2 moveDir;
	private Animator anim;
	private Rigidbody2D rb;
	public bool grounded = true;
	private bool jump = false;

	private Vector3 minScreenBounds;
	private Vector3 maxScreenBounds;

	// Use this for initialization
	void Start () {
		moveDir = Vector2.zero;
		anim = GetComponent<Animator> ();
		rb = GetComponent<Rigidbody2D> ();
	}
	
	// Update is called once per frame
	void Update () {
		


//		RaycastHit2D rh = Physics2D.Raycast (transform.position, Vector2.left, 1 << LayerMask.NameToLayer("Ground"));
//		print (rh.distance);
//
//
//		if (grounded || rh.collider != null) {
//			rb.velocity = new Vector2 (moveX * movespeed, rb.velocity.y);
//		}


		// walk
		// transform holds info like position, rotation, scale
		//transform.position += new Vector3 (moveDir.x, moveDir.y, 0.0f) * movespeed * Time.deltaTime;	//speed * direction * time

		// this sections just makes it so the object can't move outside the left or right edges of the camera
		minScreenBounds = Camera.main.ScreenToWorldPoint(new Vector3(0, 0, 0));
		maxScreenBounds = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, 0));
		transform.position = new Vector3(
			Mathf.Clamp(transform.position.x, minScreenBounds.x + 0.5f, maxScreenBounds.x - 0.5f),
			transform.position.y, 
			transform.position.z);


		grounded = Physics2D.Linecast(transform.position, groundCheck.position, 1 << LayerMask.NameToLayer("Ground"));
		
		if (Input.GetKeyDown (KeyCode.Space) && grounded) {
			jump = true;
			anim.SetTrigger ("Jump");
		} else if (grounded) {
			anim.ResetTrigger("Jump");
		}

	}

	// FixedUpdate is called every Physics frame, so do Physics/Rigidbody related stuff here
	void FixedUpdate()
	{
		// Input.GetAxisRaw detects A and D keys (also left/right arrow keys) and gives a float value
		// Which we put in a vector (like physics kind of vector)
		// Normalize is important, it sets the vectors magnitude to 1, otherwise the player object would fly off into the distance
		float moveX = Input.GetAxisRaw ("Horizontal");
		moveDir = new Vector2 (moveX, 0);
		moveDir.Normalize ();
		anim.SetFloat("Speed", Mathf.Abs(moveX) );
		
		// Flip the sprite based on the direction it is going
		if (moveX > 0) {
			transform.localScale = new Vector3(Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
		}
		else if (moveX < 0) {
			transform.localScale = new Vector3(-Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
		}

		//if (grounded) {
			rb.velocity = new Vector2 (moveX * movespeed, rb.velocity.y);
		//}

		
		RaycastHit2D hitInfo = new RaycastHit2D ();
		hitInfo = Physics2D.Raycast (transform.position, Vector2.down, snapDistance, 1 << LayerMask.NameToLayer("Ground"));

		if (grounded) {
			//grounded = true;
			transform.position = hitInfo.point;
			transform.position = new Vector3(transform.position.x, hitInfo.point.y + 0.5f, transform.position.z);
		} else {
			//grounded = false;
		}
		
		if(jump) {
			//anim.SetTrigger("Jump");
			rb.AddForce(new Vector2(0f, jumpForce));
			jump = false;
		}
		anim.SetFloat("vSpeed", rb.velocity.y);
		
		
		if (grounded && moveX == 0) {
			rb.constraints = (RigidbodyConstraints2D.FreezePositionX | RigidbodyConstraints2D.FreezeRotation);
		} else {
			rb.constraints = RigidbodyConstraints2D.FreezeRotation;
		}
	}

	// Colliders that are attached to objects can be set as triggers, which don't act like walls. They can be passed through
	// This function is called when this object enters a trigger box attached to another object
	void OnTriggerEnter2D (Collider2D col) {
		if (col.tag == "Interactable") {
			print ("interact?");

		}
	}
}
