using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightFlicker : MonoBehaviour {
	public Light flashingLight;
	public bool burst = false;
	public float delay;

	void Update() {

		delay -= Time.deltaTime;

		if (delay < 0){
			if (flashingLight.enabled == true)
				flashingLight.enabled = false;
			else if (flashingLight.enabled == false) {
				flashingLight.enabled = true;
				burst = true;
			}

			if (burst){
				burst = false;
				delay = .4f;
			} else if (!burst){
				delay = 1;
			}
		}

}


}
