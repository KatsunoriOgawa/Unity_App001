using UnityEngine;
using System.Collections;

public class TitleMoveScript : MonoBehaviour {

	private float height = 300;

	// Use this for initialization
	void Start () {
		transform.localPosition = new Vector3 ( 0, 800 );
	}

	void Update ()
	{
		if (!Common.getAnimFlag ()) {
			transform.Translate (0, -2, 0);
			if (transform.localPosition.y <= height) {
				Common.setAnimFlag (true);
			}
		} else {
			transform.localPosition = new Vector3 ( 0, height );
		}
	}
}
