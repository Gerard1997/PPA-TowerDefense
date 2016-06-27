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
