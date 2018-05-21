using UnityEngine;
using System.Collections;
using Vuforia;

public class TestActive : MonoBehaviour  {

    public GameObject go;
    Renderer re;
	void Start () {
        re = go.GetComponent<SkinnedMeshRenderer>();
	}
	
	// Update is called once per frame
    TrackableBehaviour myTrack;
	void Update () 
    {
        Debug.Log(myTrack.CurrentStatus);
	}

    void OnGUI()
    {
        if (!re.enabled)
            GUI.Label(new Rect(0, 300, 100, 100), "物体不显示");
       else if (re.enabled)
            GUI.Label(new Rect(0, 300, 100, 100), "物体显示");
    
    }

}
