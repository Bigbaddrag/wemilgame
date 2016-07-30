using UnityEngine;
using System.Collections;

public class cameraCtrl : MonoBehaviour {


	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
		Vector3 PlayerPOS = GameObject.FindGameObjectWithTag("Player").transform.transform.position;

		transform.position = new Vector3 (PlayerPOS.x, 0, -10);
	}
}
