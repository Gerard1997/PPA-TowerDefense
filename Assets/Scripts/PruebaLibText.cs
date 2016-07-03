using UnityEngine;
using System.Collections;
using System.Runtime.InteropServices;
using UnityEngine.UI;

public class PruebaLibText : MonoBehaviour {
[DllImport ("values")]
private static extern int GetValue();
public Text tex;
private int val;
	// Use this for initialization
	void Start () {
 val = GetValue();

	}

	// Update is called once per frame
	void Update () {
				tex.text = val.ToString();
	}
}
