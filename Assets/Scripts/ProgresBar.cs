using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProgresBar : MonoBehaviour {

	public RectTransform  progres;
	public int x = 0;
	int count_win = 1000;
	float bar;
	float width;

	void Start () {
		bar = gameObject.GetComponent<RectTransform>().rect.height;
		width = gameObject.GetComponent<RectTransform>().rect.width;
		progres.offsetMax = new Vector2(width/2, -bar);
	}

	void FixedUpdate () {
		Progres(0.1f);
	}

	void Progres(float count) {
		bar = bar - count;
		progres.offsetMax = new Vector2(width/2, -bar);
	}
}
