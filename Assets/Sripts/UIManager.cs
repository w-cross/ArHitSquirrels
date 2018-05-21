using UnityEngine;
using System.Collections;
using Vuforia;
using UnityEngine.UI;

/// <summary>
/// 从游戏开始到游戏结束  所有的UI管理类
/// </summary>
public class UIManager : MonoBehaviour
{
    public static UIManager instance;

    private DefaultTrackableEventHandler defaultTrackHandler;

    public bool isOnTrackFound = false;

    [HideInInspector]
    public bool isBeginBtnClick = false;
    [HideInInspector]
    public bool isGameRuning = false;
    public bool isEnd = false;
    public Button btn_start;
    public Button btn_pause;
    public Button btn_end;

    public Text text_dishuNum;
    private int jizhongDishuNum = 0;
    public int dishuTotal;

    public Text text_Timer;
    [HideInInspector]
    public float timeJishiqi;

    public UnityEngine.UI.Image image_beginAnimation;
    public Text shuKeng_Text;

    public Sprite[] pauseSps;

    public GameObject panel_game;
    public GameObject panel_score;
    public GameObject panel_jieshu;

    void Awake()
    {
        instance = this;
    }
    void Start()
    {
        EventTriggerListener.Get(btn_start.gameObject).onClick = BtnStartOnClickMethod;
        EventTriggerListener.Get(btn_end.gameObject).onClick = BtnEndOnClickMethod;
        EventTriggerListener.Get(btn_pause.gameObject).onClick = BtnPauseOnClickMethod;
        Init();
    }
    private bool isPause = false;
    private void BtnPauseOnClickMethod(GameObject go)
    {
        Debug.Log("dianji");
        isPause = !isPause;
        btn_pause.image.sprite = isPause ? pauseSps[1] : pauseSps[0];
        Time.timeScale = isPause ? 0 : 1;
    }


    void Update()
    {
        SetBool();

        if (isBeginBtnClick && isGameRuning && !isEnd)
        {
            StartCoroutine(BeginAnimation());
        }

        SetScoreAndTimer();
        if (i == 4)
            SetTextAndAnimation();

        if (timeJishiqi <= 0)
        {
            GameObject.FindObjectOfType<Light>().GetComponent<AudioSource>().mute = true;
            StartCoroutine(DisplayScore());
        }
    }
    void Init()
    {
        i = 0;
        text_dishuNum.text = jizhongDishuNum.ToString();

        text_Timer.text = timeJishiqi.ToString() + "S";

        defaultTrackHandler = GetComponent<DefaultTrackableEventHandler>();

       
        image_beginAnimation.gameObject.SetActive(false);

    }
    private void BtnStartOnClickMethod(GameObject go)
    {
        btn_start.gameObject.SetActive(false);
        btn_end.gameObject.SetActive(true);

        timeJishiqi = 60;

        isBeginBtnClick = true;
        isGameRuning = true;


        image_beginAnimation.gameObject.SetActive(true);


    }

    private void BtnEndOnClickMethod(GameObject go)
    {

        StopAllCoroutines();
        isGameRuning = false;
        isEnd = true;
        panel_game.SetActive(false);
        panel_jieshu.SetActive(true);
        GameObject.FindObjectOfType<Light>().GetComponent<AudioSource>().mute = true;
        StartCoroutine(DisplayScore());
    }
    int num = 0;
    void SetBool()
    {
        foreach (var item in GetComponent<GameManager>().trackFT)
        {
            if (item.isOnTrackingFound)
            {
                num += 1;
                isOnTrackFound = true;
            }
            else
                num -= 1;
        }
        if(num==-5)
        {
            isOnTrackFound = false;
        }
       
    }


    //+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++游戏开始++++++++++++++++++++++++++++++++++++++++++++++++++++++++++


    void SetTextAndAnimation()
    {
        if (isGameRuning && !isEnd)
        {
            if (!isOnTrackFound)
            {
                shuKeng_Text.text = "地鼠坑不见啦~";
            }
            else if (isOnTrackFound)
            {
                shuKeng_Text.text = "";
            }
        }
        else
            shuKeng_Text.text = "";
    }
    void SetScoreAndTimer()
    {
        text_dishuNum.text = jizhongDishuNum.ToString();

        text_Timer.text =((int) timeJishiqi).ToString()+"S";
    }

    public void AddScore(int score)
    {
        jizhongDishuNum += (score);
    }

    IEnumerator Timer()
    {
        yield return new WaitForSeconds(1);

        if (timeJishiqi > 0)
            timeJishiqi -= 0.5f;
        else if (timeJishiqi <= 0)
            panel_jieshu.SetActive(true);
        StartCoroutine("Timer");


    }


    public Sprite[] sprite;
    float interval = 1;
    public int i = 0;
    IEnumerator BeginAnimation()
    {

        interval -= Time.deltaTime;

        if (interval <= 0)
        {
            Debug.Log(111111111111);
            interval = 1;

            if (i <= sprite.Length - 1)
                image_beginAnimation.sprite = sprite[i++];

            else if (i == 3)
            {
                yield return new WaitForSeconds(1);
                i = 4;
                image_beginAnimation.gameObject.SetActive(false);
               // isGameRuning = true;
                StartCoroutine("Timer");
                GameObject.FindObjectOfType<Light>().GetComponent<AudioSource>().Play();
            }
        }


    }




    //+++++++++++++++++++++++++++++++++++++++++++++++++++++++统计结果+++++++++++++++++++++++++++++++++
  

    public Text text_successNum;

    public Text text_hitRate;

    public UnityEngine.UI.Image image_evaluate;

    public Sprite[] evaluates;
   
    IEnumerator DisplayScore()
    {
        yield return new WaitForSeconds(2);
        panel_game.SetActive(false);
        panel_jieshu.SetActive(false);
        panel_score.SetActive(true);
        SetEvaluate(jizhongDishuNum, dishuTotal);
    }
    /// <summary>
    /// 设置评价
    /// </summary>
    /// <param name="num">击中个数</param>
    /// <param name="total">出现总数</param>
    public void SetEvaluate(float num, float total)
    {
      
        float temp = (float)num / total;

     
        if (temp >= 0.9 && temp < 1)
        {
            image_evaluate.sprite = evaluates[1];
        }
        else if (temp >= 0.7f && temp < 0.9f)
        {
            image_evaluate.sprite = evaluates[2];
        }
        else if (temp >= 0.5f && temp < 0.7f)
        {
            image_evaluate.sprite = evaluates[3];
        }
        else if (temp < 0.5f||num==0)
        {
            image_evaluate.sprite = evaluates[4];
        }

        else if (temp == 1)
        {
            image_evaluate.sprite = evaluates[0];
        }

        text_successNum.text =  num.ToString() + " 个";

        text_hitRate.text =  (((float)num / total)).ToString("0.0%");
    }


}
