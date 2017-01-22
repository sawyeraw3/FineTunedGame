using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorDif : MonoBehaviour {
	public static int[] ColDist;
	GameManager gm;
	// Use this for initialization
	void Start () {
		gm = transform.Find ("LevelManager").GetComponent<GameManager> ();
		for (int i = 0; i < gm.cols.Length; i++) {
			ColDist [i] = (gm.cols [i].r * 255) + (gm.cols [i].g * 255) + (gm.cols [i].b * 255);
		}
	}
	

}
