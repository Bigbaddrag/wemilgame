using UnityEngine;
using System.Collections;

public class PlayerCtrl : MonoBehaviour {
	
	public float movespeed = 6.0f;
	public float jumpForce = 600.0f;
	
	private Vector2 moveDir;
	private Animator anim;
	private Rigidbody2D rb;

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
		
		// Input.GetAxisRaw detects A and D keys (also left/right arrow keys) and gives a float value
		// Which we put in a vector (like physics kind of vector)
		// Normalize is important, it sets the vectors magnitude to 1, otherwise the player object would fly off into the distance
		float moveX = Input.GetAxisRaw ("Horizontal");
		moveDir = new Vector2 (moveX, 0);
		moveDir.Normalize ();
		anim.SetFloat("Speed", Mathf.Abs(moveX) );
		
		// walk
		// transform holds info like position, rotation, scale
		transform.position += new Vector3 (moveDir.x, moveDir.y, 0.0f) * movespeed * Time.deltaTime;	//speed * direction * time

		// this sections just makes it so the object can't move outside the left or right edges of the camera
		minScreenBounds = Camera.main.ScreenToWorldPoint(new Vector3(0, 0, 0));
		maxScreenBounds = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, 0));
		transform.position = new Vector3(
			Mathf.Clamp(transform.position.x, minScreenBounds.x + 0.5f, maxScreenBounds.x - 0.5f),
			transform.position.y, 
			transform.position.z);

		// Flip the sprite based on the direction it is going
		if (moveX > 0) {
			transform.localScale = new Vector3(Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
		}
		else if (moveX < 0) {
			transform.localScale = new Vector3(-Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
		}
		
		// jump
		// Also make sure the object isn't falling, or already jumping
		// We check based on 0.01 instead of looking for zero to allow for some room for small miniscule changes
		// This may need to be tweaked for allowing jumps while going up or down slopes
		if (rb.velocity.y >= -0.01f && rb.velocity.y <= 0.01f && Input.GetKeyDown(KeyCode.Space)) {
			rb.AddForce(new Vector2(0, jumpForce));
		}
		anim.SetFloat("vSpeed", rb.velocity.y);
	}

	// Colliders that are attached to objects can be set as triggers, which don't act like walls. They can be passed through
	// This function is called when this object enters a trigger box attached to another object
	void OnTriggerEnter2D (Collider2D col) {
		if (col.tag == "Interactable") {
			print ("interact?");

		}
	}
}
