using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GreateMap : MonoBehaviour {

	public int count_item;

	[ContextMenu("CreateMap")]
    private void CreateMap()
    {
		int x = 0;
		int y = 0;

		RectTransform grt = gameObject.GetComponent<RectTransform>();
		grt.sizeDelta = new Vector2(1260, 4200);

		for (int i = 0; i < count_item; i++) {
			int height = 210;
			GameObject itemTile = Instantiate(Resources.Load<GameObject>("tile")) as GameObject;
			itemTile.name = "tile";	
			itemTile.transform.parent = this.gameObject.transform;
			itemTile.transform.localScale = new Vector3 (1,1,1);
			RectTransform rt = itemTile.GetComponent<RectTransform>();
   			rt.sizeDelta = new Vector2(height, height);
			rt.anchorMin = new Vector2(0, 1);
    		rt.anchorMax = new Vector2(0, 1);
			rt.pivot = new Vector2(0.5f, 0.5f);
			
			// rt.transform.localPosition = new Vector3 (height * x, - height * y, 0);	
			rt.transform.localPosition = new Vector3 (-(1260/2-105) + height * x  , ((4200/2)-105) - height * y, 0);	
			

			Debug.Log(rt.localPosition);
			GameObject itemTile2 = Instantiate(Resources.Load<GameObject>("tile")) as GameObject;
			itemTile2.name = "tile2";	
			itemTile2.transform.parent = itemTile.transform;
			itemTile2.transform.localScale = new Vector3 (1,1,1);
			RectTransform rt2 = itemTile2.GetComponent<RectTransform>();
   			rt2.sizeDelta = new Vector2(height, height);
			itemTile2.transform.localPosition = new Vector3 (0, 0, 0);
			
			BoxCollider2D bc = (BoxCollider2D)itemTile2.AddComponent(typeof(BoxCollider2D));
     		bc.size = new Vector2 (190, 190);

			Rigidbody2D rb = (Rigidbody2D)itemTile2.AddComponent(typeof(Rigidbody2D));
    		rb.gravityScale = 0;
			rb.interpolation = RigidbodyInterpolation2D.Extrapolate;
			// var boxCollider1 = itemTile2.AddComponent<BoxCollider>();
			// boxCollider1.size.x = boxCollider1.size.y = 210;

			x++;
			if ( (i+1)%6 == 0 ) {
				x =0;
				y +=1;
			}
		}
    }
}