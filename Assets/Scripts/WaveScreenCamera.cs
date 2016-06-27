using UnityEngine;
using System.Collections;

public class WaveScreenCamera : MonoBehaviour {

	// Use this for initialization
	private Vector3 input;
	private float moveSensibilityX;
	private float moveSensibilityY;
	public float speed;
	private float maxX, maxY, minY, minX;
	private float limitX, limitY;
	void Start () {
		limitX = GameObject.Find("Background").GetComponent<Renderer> ().bounds.size.x;
		limitY = GameObject.Find("Background").GetComponent<Renderer> ().bounds.size.y;
		maxX = (Screen.width / Screen.height * 2 - limitX *.5f) + GameObject.Find("Background").transform.position.x;
		minX = (limitX *.5f - Screen.width / Screen.height * 2) + GameObject.Find("Background").transform.position.x;
		maxY = (2 - limitY * 0.5f) + GameObject.Find("Background").transform.position.y;
		minY = (limitY * 0.5f - 2) + GameObject.Find("Background").transform.position.y;
		Debug.Log(maxY);
		Debug.Log(minY);
	}

	// Update is called once per frame
	private bool moving;
	void Update () {
		Vector3 pos = transform.position;
		if (Input.touchCount > 1 && Input.GetTouch(0).phase == TouchPhase.Moved ){
			input = Input.GetTouch(0).deltaPosition;
			transform.Translate(-input.x * 2 * Time.deltaTime, -input.y * 2 * Time.deltaTime, 0);
			if(transform.position.x < maxX || transform.position.x > minX || transform.position.y < maxY || transform.position.y > minY){
				transform.position = pos;
			}
		}
	}
}
