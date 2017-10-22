using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class hero : MonoBehaviour {
  
    public GameController GameController;

    void OnCollisionEnter2D(Collision2D coll) {
        if ((coll.gameObject.name == "tile2") || (coll.gameObject.name == "mine")) {
            StaticData.game_active = false; 
            GameController.ScorePanelH(true);   
        }
    }

    void OnTriggerEnter2D(Collider2D other) {
        if (other.gameObject.name == "boxes") {
            StaticData.speed_hero -= 0.5f;
        }
    }
  
 
    public void HeroFire() {
        GameObject itemTile = Instantiate(Resources.Load<GameObject>("fire")) as GameObject;
        itemTile.transform.parent = this.gameObject.transform;
        itemTile.transform.localScale = new Vector3 (1,1,1);
        RectTransform rt = itemTile.GetComponent<RectTransform>();
        rt.sizeDelta = new Vector2(20, 20);
        rt.transform.localPosition = new Vector3 (1, 120, 0);	
    }
}

