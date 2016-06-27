	using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PrintMap : MonoBehaviour {

	private enum sprt_name{
		ENEMY = 0,
		WALL_1,
		WALL_2,
		WALL_3,
		FLOOR_1,
		FLOOR_2,
		FLOOR_3,
		PLAYER,
		SPAWNER,
		TORRET_1,
		TORRET_2,
		TORRET_3,
		VOID,
		SPRITES_COUNT
	};

	public GameObject[] prefabs;
	public GameObject ply_;
	public int last_playerx, last_playery;
	private Vector2[] badpos;
	public List<int> BadX;
	public List<int> BadY;
	public float height;
	public float width;
	// Use this for initialization
	public void print_map () {

		//Se cambió el tamaño del boxcolider por el tamaño del bound
		//float height = prefabs [1].GetComponent<BoxCollider2D> ().size.y;
		//float width = prefabs [1].GetComponent<BoxCollider2D> ().size.x;
		height = prefabs [1].GetComponent<Renderer> ().bounds.size.y;
		width = prefabs [1].GetComponent<Renderer> ().bounds.size.x;

		Vector3 playerpos = new Vector3(0,0,0);

		ply_ =  			(GameObject)Instantiate(prefabs[(int)sprt_name.PLAYER], playerpos + new Vector3(0,0,-1), Quaternion.identity);
		//GameObject Enemy  = (GameObject)Instantiate (prefabs [(int)sprt_name.ENEMY], playerpos , Quaternion.identity);
		//Buscar todos los caracteres dentro del arreglo
		for (int i = 0; i < GetComponent<LoadMap> ().m_rows; i++) {
			for (int j = 0; j < GetComponent<LoadMap> ().m_cols; j++) {
				for (int k = 0; k < (int)sprt_name.SPRITES_COUNT; k++) {
					if (GetComponent<LoadMap> ().Grid [i] [j] == (char)((int)'a'+ k)) {//Codigo ascci de la letra a
						Vector3 position_ = new Vector3 (j * width, -i * height, 0);
						if (GetComponent<LoadMap> ().Grid [i] [j] == 'b' || GetComponent<LoadMap> ().Grid [i] [j] == 'c') {
							BadX.Add (j);
							BadY.Add (i);
						}
						if (GetComponent<LoadMap> ().Grid [i] [j] == 'h') {//h es el caracter representativo del player

							last_playerx = j;
							last_playery = i;
							ply_.name = "Player_1";
							ply_.transform.Translate (position_ );
							Instantiate (prefabs [(int)sprt_name.FLOOR_1], position_, Quaternion.identity);
						} else {

							Instantiate (prefabs [k], position_, Quaternion.identity);

						}
						k = (int)sprt_name.SPRITES_COUNT + 1;
					}
				}
			}
		}
	}
}
