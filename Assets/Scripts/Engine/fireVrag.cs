using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fireVrag : MonoBehaviour {

	void Start () {
		// Invoke("Destroy", 1f);
	}
	
	// Update is called once per frame
	void Update () {
		var tt = transform.position;
        tt.y -= 11f;
        gameObject.transform.position = tt;
	}

	void Destroy() {
		Destroy(this.gameObject);
	}
}
