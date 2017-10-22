using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret : MonoBehaviour {
	public int PositionTurret;
	bool fire = false;
	GameObject Hero;
	void Awake () {
		GameController.PositionHero += PositionHeroHend;
		Hero = GameObject.Find("Games");
	}
	void onDestroy (){
		GameController.PositionHero -= PositionHeroHend;
	}

	void PositionHeroHend(int pos) {
		Debug.Log(PositionTurret);
			if (PositionTurret == pos) {
				fire = true;
			}
	}

	void Update () {
		if (fire) {
		fire = false;
			StartCoroutine("Fire");
		}
	}

	private IEnumerator Fire()
		{
			GameObject item = Instantiate(Resources.Load<GameObject>("fireVrag")) as GameObject;
			
			item.transform.SetParent (Hero.transform);
			RectTransform rt = item.GetComponent<RectTransform>();
			rt.sizeDelta = new Vector2(50, 50);
			rt.transform.position = new Vector3 (transform.position.x , transform.position.y, transform.position.z);	
			
			item.transform.localScale = new Vector3 (1,1,1);
			var tt = item.transform.position;
			float timer = 0f;
			while (timer < 90)
			{
				tt.y -= 6f;
				timer +=1;
       			item.transform.position = tt;
				yield return new WaitForFixedUpdate();
			}
			Destroy(item);
		}
}