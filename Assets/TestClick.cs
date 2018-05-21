using UnityEngine;
using System.Collections;

public class TestClick : MonoBehaviour
{
 
    void Start()
    {
       
    }

   
    void Update()
    {
        PhysicRayCast();
      
    }
    string str = "没有点击到玩家";
    string acitve;
    void PhysicRayCast()
    {

        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Input.GetMouseButtonDown(0))
        {
            if (Physics.Raycast(ray, out hit, 10000))
            {
                if (hit.collider.gameObject.tag == "Player")
                {
                    Debug.Log("这是玩家");
                    str = "这是玩家";
                }

            }
            else
                str = "zhebushiwanj";
        }

    }
    void OnGUI()
    {
        GUILayout.Label(str);
        GUILayout.Label(acitve);
    }


   
}
