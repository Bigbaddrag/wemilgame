using UnityEngine;
using System.Collections;

public class sign1Ctrl : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	// This sign will react when the player walks next to it
	// Can be used as template code for displaying text or whatever
	void OnTriggerEnter2D (Collider2D col) {
		if (col.tag == "Player") {
			// It just prints this to Unity's debug console for now
			print ("Don't touch me");
			
		}
	}
}
