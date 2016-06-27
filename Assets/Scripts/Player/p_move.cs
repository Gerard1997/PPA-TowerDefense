using UnityEngine;
using System.Collections;

public class p_move : MonoBehaviour {

	public float m_speed = 0.2f;
	private Vector3 currentePos;
	private Vector2 move;

	// Use this for initialization
	void Start () {
		currentePos = transform.position;
	}

	// Update is called once per frame
	void Update(){
		/*Debug.Log ("(" + Mathf.RoundToInt(transform.position.x/0.32f) + "," +
			             Mathf.RoundToInt(-transform.position.y / 0.32f) + ")" );*/
									 if (Input.GetKeyDown("w")){
									 		move.y = 1;
											move_player ();
									 }
									 if (Input.GetKeyUp("w")){
									 		move.y = 0;
									 }

									 if (Input.GetKeyDown("s")){
									 		move.y = -1;
									 }
									 if (Input.GetKeyUp("s")){
									 		move.y = 0;
									 }

									 if (Input.GetKeyDown("d")){
									 		move.x = 1;
									 }
									 if (Input.GetKeyUp("d")){
									 		move.x = 0;
									 }

									 if (Input.GetKeyDown("a")){
									 		move.x = -1;
									 }
									 if (Input.GetKeyUp("a")){
									 		move.x = 0;
									 }
	}

	void FixedUpdate () {



	}

	void move_player(){
		 	Vector2 pos = new Vector2 (transform.position.x,transform.position.y);
	  	float relativeSpeed = m_speed * Time.deltaTime;
			transform.position = Vector2.MoveTowards (transform.position, pos+ move * 0.5f, relativeSpeed);
		/*//Mover hacia la direccion actual

		//Preguntar si se ha hecho alguna entrada al teclado solo si no se esta moviendo
		if (Input.GetKey(KeyCode.UpArrow))
			currentePos += Vector3.up * 0.32f * 0.1f;
		if (Input.GetKey(KeyCode.RightArrow))
			currentePos += Vector3.right * 0.32f * 0.1f;
		if (Input.GetKey(KeyCode.DownArrow))
			currentePos -= Vector3.up * 0.32f * 0.1f;
		if (Input.GetKey(KeyCode.LeftArrow))
			currentePos -= Vector3.right * 0.32f * 0.1f;

		transform.position = currentePos;*/
	}

	public Vector2 GetPosition(){
		Vector2 position;
		position.x = Mathf.RoundToInt(transform.position.x / 0.5f);
		position.y = Mathf.RoundToInt(-transform.position.y / 0.5f);
		return position;
	}
}
