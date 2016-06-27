using UnityEngine;
using System.Collections;
using System.Runtime.InteropServices;

public class AI_ : MonoBehaviour {
	[DllImport("Enemy_AI")]
	public static extern int GetPathMovs (int[] badx, int[] bady, int nBad,
										  int[] movsx,int[] movsy,
										  int currX, int currY,
										  int PlayerX,int PlayerY);
	//Funcion encargada de encontrar el camino más cercano al enemigo
	//Los primeros 2 parametros (Badx y Bady son las coordenadas 'x' y 'y' respectivamente
	//de los lugares en donde se encuentran paredes, torres, obejtos que no puedan ser atravesados
	//por el enemigo, el tercer parametro es simplemente la cantidad de estos objetos mencionados.

	//El 4° y 5° parametro son arreglos en donde se almacenara el patron a seguir para 'x' y 'y'
	//Los ultimos parametros son la posicion del enemigo y la del jugador respectivamente

	private int[] MovsX;
	private int[] MovsY;
	private int movs = 0;
	public float Speed;
	public GameObject map;
	private Vector2 nextTargetPos;
	private int nextTarget = 0;
	private bool endOfPath = false;

	private float height;
	private float width;

	// Use this for initialization

	void Start () {
		map = GameObject.Find("Main Camera");
		height = map.GetComponent<PrintMap>().height;
		width = map.GetComponent<PrintMap>().width;
		SeekPlayer ();

	}

	// Update is called once per frame
	void Update () {

		FollowPath ();

	}

	void SeekPlayer(){
		Vector2 playerCoords = map.GetComponent<PrintMap> ().ply_.GetComponent<p_move> ().GetPosition ();
		int p_x = (int)playerCoords.x;
		int p_y = (int)playerCoords.y;
				Debug.Log(map);

		MovsX = new int[1000];
		MovsY = new int[1000];

		movs =  GetPathMovs(map.GetComponent<PrintMap>().BadX.ToArray(), map.GetComponent<PrintMap>().BadY.ToArray(),
				map.GetComponent<PrintMap>().BadX.Count,
				MovsX, MovsY,
				Mathf.RoundToInt(transform.position.x / width),
				Mathf.RoundToInt(-transform.position.y / height),
				p_x, p_y);

		nextTarget = 0;
		//Probablemente no necesario, pero no hay que arriesgar xD
		if (movs > 1) {
			nextTargetPos = new Vector2 ((float)MovsX [1], -(float)MovsY [1]);
			endOfPath = false;
		} else {
			endOfPath = true;
		}
	}

	void OnTriggerEnter2D(Collider2D obj){
		if (obj.gameObject.tag == "PLAYER") {
			Debug.Log ("Player violado");
			Destroy (gameObject);
		}
	}

	Vector2 GetPosition(){
		Vector2 position;
		position.x = Mathf.RoundToInt(transform.position.x / width);
		position.y = Mathf.RoundToInt(transform.position.y / height);
		return position;
	}

	void FollowPath(){

		if (endOfPath) {
			SeekPlayer ();
			return;
		}

		float relativeSpeed = Speed * Time.deltaTime;

		//0.32f tamaño en pixeles de la imagen (32x32);

		if ((transform.position.x < nextTargetPos.x * width + relativeSpeed*width &&
			transform.position.x > nextTargetPos.x * width - relativeSpeed*width) &&
			(transform.position.y <  nextTargetPos.y * width + relativeSpeed*width &&
			transform.position.y > nextTargetPos.y * width - relativeSpeed*width)) {

			nextTarget++;

			if (nextTarget == movs) {
				endOfPath = true;
				return;
			}

			nextTargetPos = new Vector2 (MovsX [nextTarget], -MovsY [nextTarget]);
		}

		transform.position = Vector2.MoveTowards (transform.position, nextTargetPos * width, relativeSpeed);
	//	transform.position = new Vector3(transform.position.x,transform.position.y,-1);
	}


}
