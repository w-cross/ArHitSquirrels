using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class BeginGame : MonoBehaviour
{

    // Use this for initialization
    void Start()
    {

        Invoke("Begin", 3);
    }

    // Update is called once per frame
    void Update()
    {

    }

    void Begin()
    {
        SceneManager.LoadScene(1);
    }
}
