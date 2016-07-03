using UnityEngine;
using System.Collections;

public class PlaceTorret : MonoBehaviour {
	// Use this for initialization
	private Canvas canvas;
	public	GameObject obj;

	public bool onUI;
	void Start () {
		canvas = GameObject.Find("Pantalla Torretas").GetComponent<Canvas>();
		onUI= false;
	}
	void Update(){
		if (Input.touchCount == 1 && Input.GetTouch(0).phase == TouchPhase.Began ){
			Vector3 click = Input.GetTouch(0).position;
			click.z = 10;
			click = GetComponent<Camera>().ScreenToWorldPoint(click);
			Debug.DrawLine ( click + new Vector3 (0, 0, 3), click, Color.red);
			RaycastHit2D hit = Physics2D.Linecast (click + new Vector3 (0, 0, 3), click );
			Debug.Log ("Hit: " + hit.collider.gameObject.name);
			if (hit.collider.gameObject.tag == "PLACE_B" || hit.collider.gameObject.tag == "PLACE_N"){
				Debug.Log("lol");
			}
		}
	}

  public	void Turret(){
		obj.GetComponent<TransformTurret>().Transforming();
		onUI = false;
	}
	public void SetObj(GameObject other){
		obj = other;
	}
	public void enterUI(){
		onUI=true;
	}
}
