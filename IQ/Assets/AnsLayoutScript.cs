using UnityEngine;
using System.Collections;

public class AnsLayoutScript : MonoBehaviour {

	[SerializeField]
	private int id;

	// Update is called once per frame
	void Awake () {
		switch( Common.getLevel() ){
		default:
		case 1:
			switch( id ){
			case 0: transform.localPosition = new Vector3 (-120,90); break;
			case 1: transform.localPosition = new Vector3 (120,90); break;
			case 2: transform.localPosition = new Vector3 (-120,-150); break;
			case 3: transform.localPosition = new Vector3 (120,-150); break;
			case 4: transform.localPosition = new Vector3 (-2000,-2000); break;
			case 5: transform.localPosition = new Vector3 (-2000,-2000); break;
			}
			break;
		case 2:
			switch( id ){
			case 0: transform.localPosition = new Vector3 (-150,166); break;
			case 1: transform.localPosition = new Vector3 (150,166); break;
			case 2: transform.localPosition = new Vector3 (-150,-47); break;
			case 3: transform.localPosition = new Vector3 (150,-47); break;
			case 4: transform.localPosition = new Vector3 (0,-258); break;
			case 5: transform.localPosition = new Vector3 (-2000,-2000); break;
			}
			break;
		case 3:
			switch( id ){
			case 0: transform.localPosition = new Vector3 (-150,166); break;
			case 1: transform.localPosition = new Vector3 (150,166); break;
			case 2: transform.localPosition = new Vector3 (-150,-47); break;
			case 3: transform.localPosition = new Vector3 (150,-47); break;
			case 4: transform.localPosition = new Vector3 (-150,-258); break;
			case 5: transform.localPosition = new Vector3 (150,-258); break;
			}
			break;
		}
	}
}
