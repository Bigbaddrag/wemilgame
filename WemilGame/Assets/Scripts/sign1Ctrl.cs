using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class sign1Ctrl : MonoBehaviour {

	private Transform plyrTarget;
    private Canvas c;
    private Text t;
    private float Distance;
    private bool touched;

    public Texture img;
    public Texture imgShock;

	// Use this for initialization
	void Start () {
        c = GetComponentInChildren<Canvas>();
        //c.enabled = false;
        t = GetComponentInChildren<Text>();
        touched = false;
	}
	
	// Update is called once per frame
	void Update () {
		plyrTarget = GameObject.FindGameObjectWithTag ("Player").transform;

        if (Vector3.Distance(transform.position, plyrTarget.position) > 2.0f && touched)
        {
            //c.enabled = false;    // this makes the canvas (text bubble) disappear
            t.text = "Thank you...";
            c.GetComponentInChildren<CanvasRenderer>().SetTexture(img);
        }
	}
	
	void OnTriggerEnter2D (Collider2D col) {
		if (col.tag == "Player") {
            c.enabled = true;
            c.GetComponentInChildren<CanvasRenderer>().SetTexture(imgShock);
            t.text = "DON'T TOUCH ME!";
            touched = true;
		}
	}
}
