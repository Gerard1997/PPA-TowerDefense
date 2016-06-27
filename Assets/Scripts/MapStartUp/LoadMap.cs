using UnityEngine;
using System.Collections;
using System.Text.RegularExpressions;
using System.Runtime.InteropServices;

public class LoadMap : MonoBehaviour {
	//[DllImport("Suma", EntryPoint = "FillArray")]
	//public static extern int FillArray (int[] a, int[] b,int lenght);

	/*private int[] movsX;
	private int[] movsY;
	private int n_movs = 10;*/

	public TextAsset map;
	public char [][]Grid;
	public int m_rows, m_cols;
	// Use this for initialization
	void Awake () {

		load_Map ();

	}

	// Update is called once per frame

	private void load_Map (){

		string[] fLines;
		string filetxt = map.text;
		fLines = Regex.Split ( filetxt, "\n" );

		m_rows = fLines.Length-1;

		m_cols = fLines [0].Length / 2;

		Grid = new char[m_rows][];

		for (int i = 0; i < m_rows; i++) {
			Grid [i] = new char[m_cols];
		}

		for (int i = 0; i < m_rows; i++) {
			for (int j = 0; j < m_cols; j++) {
				Grid [i] [j] = fLines [i][j*2];
				//Debug.Log (Grid [i] [j]);
			}
		}

		GetComponent<PrintMap> ().print_map ();
	}

}
