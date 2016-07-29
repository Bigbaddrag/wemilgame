using UnityEngine;
using System.Collections;

public class sign1Ctrl : MonoBehaviour {

	private Transform plyrTarget;
	private bool talk;
	private GUIText gt;

	// Use this for initialization
	void Start () {
		gt = GetComponent<GUIText> ();
		talk = false;
	}
	
	// Update is called once per frame
	void Update () {
		plyrTarget = GameObject.FindGameObjectWithTag ("Player").transform;
	}

	void OnGUI ()
	{
		if (talk) {
			Vector3 getPixelPos = Camera.main.WorldToScreenPoint (transform.position);
			getPixelPos.y = Screen.height - getPixelPos.y;

//			gameObject.renderer.bounds.center;
//
//			GetComponent<SpriteRenderer>().bounds.center.x;
			GUIContent gc = new GUIContent(gt.text);
			GUIStyle gs = new GUIStyle();

			gs.alignment = gt.anchor;



			//this ain't flying too well atm
			GUI.Label (new Rect (getPixelPos.x - 50.0f, getPixelPos.y - 50.0f, 200f, 100f), gc, gs);
		}
	}
	
	void OnTriggerEnter2D (Collider2D col) {
		if (col.tag == "Player") {
			talk = true;
		}
	}

	void OnTriggerExit2D (Collider2D col){
		if (col.tag == "Player") {
			talk = false;
		}
	}
}
