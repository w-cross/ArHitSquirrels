//using UnityEngine;
//using System.Collections;

//public class RayTest : MonoBehaviour {
//    public LayerMask myLayer;
//    public GUITexture myUI;
//    public GameObject bloodObj;
//    //UISprite bloodSprite;
//    LineRenderer line;
//    float enemyNum = 1;

//    void Start ()
//    {
//        bloodSprite = bloodObj.GetComponent<UISprite> ();
//        line = GetComponent<LineRenderer> ();
//        Cursor.visible = false;
//    }

//    void Update () {
//        //从枪口位置向前投放射线,在碰到的物体相对于屏幕位置显示瞄准镜（GUITexture）
//        RaycastHit hit;
		
//        if (Physics.Raycast (transform.position, transform.forward, out hit, 15, myLayer)) {
//            Vector3 hitPos = Camera.main.WorldToScreenPoint (hit.point);//世界坐标转化为屏幕坐标位置
//            myUI.enabled = true;
//            line.enabled = true;
//            myUI.pixelInset = new Rect (hitPos.x - myUI.pixelInset.width / 2,
//                           hitPos.y - myUI.pixelInset.height / 2,
//                           myUI.pixelInset.width, myUI.pixelInset.height);
//            Debug.DrawLine (transform.position, hit.point, Color.red);
//            if (hit.collider.tag == "enemy" && Input.GetMouseButtonDown (0)) {
//    //射线碰到的物体标签为enemy 并且点击鼠标左键（此处可代表击中enemy）
					
//                enemyNum -= 0.1f;
//                bloodSprite.fillAmount = enemyNum;
//                if(enemyNum<=0) Destroy (hit.collider.gameObject, 0.5f);//消失enemy物体
//            }
//            line.SetPosition (0, transform.position);
//            line.SetPosition (1, hit.point);
//        } 
//        else 
//        {
//            myUI.enabled = false;
//            line.enabled = false;
//        }
//    }
//}
