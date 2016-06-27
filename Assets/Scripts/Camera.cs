using UnityEngine;
using System.Collections;

public class Camera : MonoBehaviour {
	public GameObject player;

	// Use this for initialization
	void Start () {
		player = GameObject.FindWithTag("PLAYER");
	}

	// Update is called once per frame
	void Update () {
			if (player != null){

				transform.position = player.transform.position + new Vector3 (0,0,-5);
			}

	}
}
