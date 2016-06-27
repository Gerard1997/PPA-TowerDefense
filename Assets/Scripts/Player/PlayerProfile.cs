using UnityEngine;
using System.Collections;

public class PlayerProfile : MonoBehaviour {

	// Use this for initialization
	public int life;
	private GameObject mainControler;
	void Start () {
		mainControler = GameObject.Find("Main Camera");
	}

	// Update is called once per frame
	void Update () {

	}
	int GetLife(){
		return life;
	}
	void Death(){
		Destroy(gameObject);
		mainControler.GetComponent<WaveScreen>().stopGame();
	}
	void takeDamage(){
		life -- ;
		if (life <= 0 ){
			Death();
		}
	}
	void OnTriggerEnter2D(Collider2D coll){
		if (coll.tag == "ENEMY"){
			takeDamage();
		}

	}
}
