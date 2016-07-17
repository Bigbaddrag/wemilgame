using UnityEngine;
using System.Collections;

public class PlayerCtrl : MonoBehaviour {
	
	public float movespeed = 6.0f;
	public float jumpForce = 600.0f;
	
	private Vector2 moveDir;
	//private Animator anim;
	private Rigidbody2D rb;
	private Vector3 minScreenBounds;
	private Vector3 maxScreenBounds;

	// Use this for initialization
	void Start () {
		moveDir = Vector2.zero;
		//anim = GetComponent<Animator> ();
		rb = GetComponent<Rigidbody2D> ();

	}
	
	// Update is called once per frame
	void Update () {
		
		
		float moveX = Input.GetAxisRaw ("Horizontal");
		moveDir = new Vector2 (moveX, 0);
		moveDir.Normalize ();
		//anim.SetFloat("Speed", Mathf.Abs(moveX) );
		
		// walk
		transform.position += new Vector3 (moveDir.x, moveDir.y, 0.0f) * movespeed * Time.deltaTime;	//speed * direction * time
		
		minScreenBounds = Camera.main.ScreenToWorldPoint(new Vector3(0, 0, 0));
		maxScreenBounds = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, 0));
		
		transform.position = new Vector3(
			Mathf.Clamp(transform.position.x, minScreenBounds.x + 0.5f, maxScreenBounds.x - 0.5f),
			transform.position.y, 
			transform.position.z);
		
		if (moveX > 0) {
			transform.localScale = new Vector3(Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
		}
		else if (moveX < 0) {
			transform.localScale = new Vector3(-Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
		}
		
		// jump
		if (rb.velocity.y >= -0.01f && rb.velocity.y <= 0.01f && Input.GetKeyDown(KeyCode.Space)) {
			rb.AddForce(new Vector2(0, jumpForce));
		}
		//anim.SetFloat("vSpeed", rb.velocity.y);
	}
}
