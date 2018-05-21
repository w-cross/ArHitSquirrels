using UnityEngine;
using System.Collections;

public class MouseMove : MonoBehaviour
{
    private Vector3 originalpos;
    public float offsetY;
    void Awake()
    {
        originalpos = transform.localPosition;
    }

  
    void Update()
    {

    }

    void OnEnable()
    {
        Move();
    }
    static int i = 0;
    void Move()
    {
        i++;
        transform.localPosition = originalpos;
        iTween.MoveFrom(gameObject, iTween.Hash("y", offsetY,"islocal",true ,"delay", Random.Range(0f,0.8f), "time", 0.8f, "easetype", iTween.EaseType.linear, "loopType", iTween.LoopType.none,"oncomplete","OnAnimationEnd"));
    }

    void OnAnimationEnd()
    {
        gameObject.SetActive(false);
    }
}
