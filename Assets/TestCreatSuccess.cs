using UnityEngine;
using System.Collections;
using Vuforia;
using UnityEngine.UI;
public class TestCreatSuccess : MonoBehaviour
{
    public GameObject one;
    public GameObject two;

    public void ChangeOne()
    {
        one.SetActive(true);
        two.SetActive(false);
    }

    public void ChangeTwo()
    {
        one.SetActive(false);
        two.SetActive(true);
    }


    void OnGUI()
    {
        if (one.activeSelf)
            GUILayout.Label("物体1显示");
        else if (two.activeSelf)
            GUILayout.Label("物体2显示");
    }
   
}
