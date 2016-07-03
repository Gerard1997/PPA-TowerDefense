using UnityEngine;
using System.Collections;

public class TransformTurret : MonoBehaviour {
	public GameObject turret;

	public GameObject wave;
	private Touch input;
	private float touchingTimeAccum;
	public float touchingTime;

	// Use this for initialization
	public Canvas canvas;
	void Start () {
			wave = GameObject.Find("Main Camera");
			canvas = GameObject.Find("Pantalla Torretas").GetComponent<Canvas>();
			touchingTime = 0;

	}
	void OnMouseDown(){

		if (!wave.GetComponent<WaveScreen>().onPlay && !wave.GetComponent<PlaceTorret>().onUI){
			if (Input.touchCount == 1 ){
				touchingTimeAccum += Time.deltaTime;
				Debug.Log(touchingTimeAccum);
				if (touchingTimeAccum > touchingTime){
					canvas.enabled = true;
					wave.GetComponent<PlaceTorret>().enterUI();
					wave.GetComponent<PlaceTorret>().SetObj(gameObject);
					touchingTimeAccum = 0;
				}
			}
		}
	}
	public void Transforming(){
		if (wave.GetComponent<WaveScreen>().towers > 0){
			Instantiate(turret, transform.position,transform.rotation);
			canvas.enabled = false;
			wave.GetComponent<WaveScreen>().towers --;
			Destroy(gameObject);
		} else {
			canvas.enabled = false;
		}
	}

}
