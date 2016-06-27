using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class WaveScreen : MonoBehaviour {
	private GameObject place;
	private GameObject[] places;
	public bool onPlay = false;
	public GameObject floor;
	public GameObject block;
	private WaveScreenCamera waveCamera;
	private Camera playCamera;
	private PlayerControl playerControl;

	public int towers;

	public GameObject towerText;
	public Canvas canvas;
	public Canvas menu;

	// Use this for initialization
	void Start () {
		canvas = GameObject.Find("Pantalla Torretas").GetComponent<Canvas>();
		menu = GameObject.Find("Pantalla Compras").GetComponent<Canvas>();
		waveCamera = GetComponent<WaveScreenCamera>();
		playCamera = GetComponent<Camera>();
		playerControl = GameObject.FindWithTag("PLAYER").GetComponent<PlayerControl>();
	}

	// Update is called once per frame
	void Update () {
		towerText.GetComponent<Text>().text = towers.ToString();
	}

	public void startGame(){
		canvas.enabled = false;
		menu.enabled = false;
		onPlay = true;
		waveCamera.enabled = false;
		playCamera.enabled = true;
		playerControl.enabled = true;

		places = GameObject.FindGameObjectsWithTag("PLACE_N");
		foreach(GameObject place in places){
			Instantiate(floor,place.transform.position,place.transform.rotation);
			  Destroy(place,0);
		}
		places = GameObject.FindGameObjectsWithTag("PLACE_B");
		foreach(GameObject place in places){
			Instantiate(block, place.transform.position,place.transform.rotation);
				Destroy(place,0); ;
		}
	}
	public void stopGame(){
		playerControl.enabled = false;
		waveCamera.enabled = true;
		playCamera.enabled = false;
		menu.enabled = true;
		onPlay = false;
	}
}
