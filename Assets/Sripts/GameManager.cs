using UnityEngine;
using System.Collections;
using Vuforia;

/// <summary>
/// 游戏管理类
/// </summary>
public class GameManager : MonoBehaviour
{

    private DefaultTrackableEventHandler defaultTrackHandler;
    private GameObject currentMouse;

    private bool isTrack;
    
    public UnityEngine.UI.Image flowers;

    public DefaultTrackableEventHandler[] trackFT;

    void Start()
    {
        Init();
        currentMouse = transform.GetChild(0).GetChild(0).gameObject;
    }


    void Update()
    {

        isTrack = defaultTrackHandler.isOnTrackingFound;
       
        MouseGenerateAndMiss();
        StartCoroutine( Interavtive());

    }

    private float time = 2;//没有击打时候老鼠存在2秒
    private float time1 = 1;
    private float intervalMiss = 3;
    private float intervalGenerate = 1;

    void MouseGenerateAndMiss()
    {
      
        if (  UIManager.instance.isGameRuning&&UIManager.instance.i==4)
        {
            if (UIManager.instance.timeJishiqi > 0)
            {
                foreach (var item in trackFT)
                {
                    if(item.isOnTrackingFound)
                    {
                       // StartCoroutine(CreatFlower(flowers));
                        Debug.Log("xianshi");
                        intervalMiss -= Time.deltaTime;
                        if (intervalMiss > 0)
                            continue;
                        intervalMiss = item.timer;
                        item.transform.GetChild(0).GetChild(0).gameObject.SetActive(true);
                        UIManager.instance.dishuTotal += 1;
                    }
                }
             
             

               
            }

        }
         else if(!isTrack|| UIManager.instance.timeJishiqi<=0)
            {
               
            }
    }

    IEnumerator CreatFlower(UnityEngine.UI.Image flower)
    {

        if ( UIManager.instance.isGameRuning)
        {
            yield return new WaitForSeconds(0f);
            flower.gameObject.SetActive(true);
        }
        else
        {
            yield return 0;
            flower.gameObject.SetActive(false);
        }
    }
    IEnumerator Interavtive()
    {
        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Input.GetMouseButtonDown(0))
        {
            if (Physics.Raycast(ray, out hit, 10000))
            {
                if (hit.collider.gameObject.tag == "Player")
                {
                    //地鼠表情变化
                    UIManager.instance.AddScore(1);
                    hit.transform.GetChild(0).gameObject.SetActive(true);
                    GetComponent<AudioSource>().Play();
                    yield return new WaitForSeconds(0.3f);
                    hit.transform.GetChild(0).gameObject.SetActive(false);
                }

            }
            else
            {

            }
        }
    }
    void Init()
    {
        defaultTrackHandler = GetComponent<DefaultTrackableEventHandler>();
             
    }

}
