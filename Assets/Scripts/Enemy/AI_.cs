using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;

public class AI_ : MonoBehaviour {

  private List<Vector2> movsxy;
	private float blockSize = 0.5f;
	private int movs = 0;
    public bool randomState;
	public float Speed;
	private Vector2 nextTargetPos;
	private int nextTarget = 0;
	private bool endOfPath = false;
  private Rigidbody2D rgbod;

	// Use this for initialization
	void Start () {
        rgbod = GetComponent<Rigidbody2D>();
        SeekPlayer();
        randomState = true;
	}

	// Update is called once per frame
	void Update () {

		FollowPath ();

	}

	void SeekPlayer(){
//		Vector2 playerCoords = map.GetComponent<PrintMap> ().ply_.GetComponent<p_move> ().GetPosition ();
//		int p_x = (int)playerCoords.x;
//		int p_y = (int)playerCoords.y;

		Vector2 currentPos = new Vector2 (Mathf.RoundToInt (transform.position.x / blockSize),
										  Mathf.RoundToInt (-transform.position.y / blockSize));

        movsxy = new List<Vector2>();

        float desition = Random.Range(0, 10);

        if(desition <= 5 || !randomState) {
            movsxy = GameObject.Find("Main Camera").GetComponent<PathToPlayer>().GetPathToPlayer(currentPos);
        }
        else {
            int tx = (int)(Random.Range(0, GameObject.Find("Main Camera").GetComponent<LoadMap>().m_cols));
            int ty = (int)(Random.Range(0, GameObject.Find("Main Camera").GetComponent<LoadMap>().m_rows));
            if (GameObject.Find("Main Camera").GetComponent<LoadMap>().Grid[ty][tx] == 'b')
            {
                endOfPath = true;
                return;
            }
            else
            {
                Vector2 targetPos = new Vector2(tx, ty);
                movsxy = GameObject.Find("Main Camera").GetComponent<PathToPlayer>().GetPathToPlayer(currentPos, targetPos);
            }
        }    

		nextTarget = 0;
        //Probablemente no necesario, pero no hay que arriesgar xD
        if (movsxy.Count > 1) {
            nextTargetPos = new Vector2 ((float)movsxy[1].x, -(float)movsxy[1].y);
            endOfPath = false;
        } else {
            endOfPath = true;
        }

        movs = movsxy.Count - 1;

    }

	void OnTriggerEnter2D(Collider2D obj){
		if (obj.gameObject.tag == "PLAYER") {
			Debug.Log ("Player violado");
			Destroy (gameObject);
		}
	}

	Vector2 GetPosition(){
		Vector2 position;
		position.x = Mathf.RoundToInt(transform.position.x / 0.5f);
		position.y = Mathf.RoundToInt(transform.position.y / 0.5f);
		return position;
	}

	void FollowPath(){

		if (endOfPath) {
			SeekPlayer ();
			return;
		}

		float relativeSpeed = Speed * Time.deltaTime;

		if ((transform.position.x < nextTargetPos.x * .5f + relativeSpeed*0.5f &&
			transform.position.x > nextTargetPos.x * .5f - relativeSpeed*0.5f) &&
			(transform.position.y <  nextTargetPos.y * .5f + relativeSpeed*0.5f &&
			transform.position.y > nextTargetPos.y * .5f - relativeSpeed*0.5f)) {

			nextTarget++;

			if (nextTarget >= movs) {
				endOfPath = true;
				return;
			}

			nextTargetPos = new Vector2 (movsxy [nextTarget].x, -movsxy[nextTarget].y);
		}

		rgbod.position = Vector2.MoveTowards (transform.position, nextTargetPos * 0.5f, relativeSpeed);

	}


}
