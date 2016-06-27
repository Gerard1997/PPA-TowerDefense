using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayerControl : MonoBehaviour {


	private Rigidbody2D rgbod;
	private Vector2 move;
	private Vector2 pos;
	private bool dirX;
	private Vector3 currentPos;
	private List<int> badX;
	private List<int> badY;
	private PrintMap map;
	private Vector2 tilePos;
	private char[,] walls;
	private enum direction {NO,UP,DOWN,RIGHT,LEFT};
	private direction redirect;
	private Vector2 deltaTouchPosition;
	float height;
	float width;

	public float sensor;
	public float speed;

	void Start () {
		rgbod = GetComponent<Rigidbody2D>();
		map = GameObject.Find("Main Camera").GetComponent<PrintMap>();
		width = map.width;
		height = map.height;
		badX = map.BadX;
		badY = map.BadY;
		tilePos = GetPosition();
		walls = new char [100, 100] ;
		int n = 0;
		foreach(int badx in badX){
			walls [badx,badY[n]] = 'p';
			n++;
		}
	}
	void Update (){
			//REDIRECCION
			if(redirect != direction.NO){
				Debug.Log("Redirect");
				if (redirect == direction.UP  && ValidMovement(Vector2.up)){
					MoveUp();
					Debug.Log("Redirect: UP");
					redirect = direction.NO;
				}
				if (redirect == direction.DOWN  && ValidMovement(Vector2.down)) {
					MoveDown();
					Debug.Log("Redirect: DOWN");
					redirect = direction.NO;
				}
				if (redirect == direction.RIGHT  && ValidMovement(Vector2.right)){
					MoveRight();
					Debug.Log("Redirect: RIGHT");
					redirect = direction.NO;
				}
				if (redirect == direction.LEFT  && ValidMovement(Vector2.left)){
					MoveLeft();
					Debug.Log("Redirect: LEFT");
					redirect = direction.NO;
				}
			}

			//MOVIMIENTO
			if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Moved ){
				deltaTouchPosition = Input.GetTouch(0).deltaPosition;
				CalculateDirection();
				//Debug.Log(deltaTouchPosition);
				if (deltaTouchPosition.y > sensor &&ValidMovement(Vector2.up) && !dirX){
					MoveUp();
				}else if (deltaTouchPosition.y > sensor && !dirX){
					redirect = direction.UP;
				}
				if (deltaTouchPosition.y < sensor &&ValidMovement(Vector2.down) && !dirX){
					MoveDown();
				}else if (deltaTouchPosition.y < sensor && !dirX){
					redirect = direction.DOWN;
				}
				if (deltaTouchPosition.x > sensor &&ValidMovement(Vector2.right) && dirX){
					MoveRight();
				}else if (deltaTouchPosition.x > sensor &&dirX ){
					redirect = direction.RIGHT;
				}
				if (deltaTouchPosition.x < sensor  &&ValidMovement(Vector2.left) && dirX){
					MoveLeft();
				}else if (deltaTouchPosition.x < sensor && dirX){
					redirect = direction.LEFT;
				}
			}
	}
	void FixedUpdate () {
		pos = transform.position;
		rgbod.position = Vector2.Lerp(pos, pos + move,speed);
	}

	void MoveUp(){
		RepositionPlayer();
		move.y = 1;
		move.x = 0;
		redirect = direction.NO;
		//Debug.Log("UP");
	}
	void MoveDown(){
		RepositionPlayer();
		move.y = -1;
		move.x = 0;
		redirect = direction.NO;
		//Debug.Log("DOWN");
	}
	void MoveRight(){
		RepositionPlayer();
		move.x = 1;
		move.y = 0;
		redirect = direction.NO;
		//Debug.Log("RIGHT");
	}
	void MoveLeft(){
		RepositionPlayer();
		move.x = -1;
		move.y = 0;
		redirect = direction.NO;
		//Debug.Log("LEFT");
	}
	void CalculateDirection(){
		dirX = false;
		if (Mathf.Abs(deltaTouchPosition.x) > Mathf.Abs(deltaTouchPosition.y)){
			dirX = true;
		}
	}
	public Vector2 GetPosition(){
		Vector2 position;
		position.x = Mathf.RoundToInt(transform.position.x / width);
		position.y = Mathf.RoundToInt(-transform.position.y / height);
		return position;
	}
	public Vector2 GetWorldPosition(){
		Vector2 position = GetPosition();
		position = new Vector2(position.x * width,-position.y * height);
		return position;
	}
	public void RepositionPlayer(){
		transform.position = GetWorldPosition();
	}
	public bool ValidMovement(Vector2 direction){
		direction = new Vector2 (direction.x,-direction.y);
		Vector2 tilePos = GetPosition() + direction;
		return !(walls[(int)tilePos.x,(int)tilePos.y] == 'p');
	}
}
