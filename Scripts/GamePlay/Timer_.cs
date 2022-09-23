using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using SkillzSDK;

public class Timer_ : MonoBehaviour
{
    private TMP_Text textTimer;
    public float delta_time;

    private bool stop_clock_;
    [HideInInspector]
    public string _Timing { set; get; }

    public GameObject TimeOverBanner;
    public Slider progressSliders;

    public GameObject TimeUp_;
    public TMP_Text TimeUp_Text_;

    public static Timer_ instance;

    public GameObject twoMinutes;
    public GameObject oneMinutes;

    private TMP_Text TimeText;
    public GameObject[] TimeBoarder;
    public Color[] _TimeColor;
    private Color lerpedColor;

    private bool Setonce;
    private bool Setonce2;

    private IEnumerator Wait;
    [SerializeField] private float lerpTime;

    public bool StopTimer(bool time) { stop_clock_ = time; return stop_clock_; }

    private void Awake()
    {
        if (instance)
            Destroy(instance);

        instance = this;
        Debug.Log("Get Score Timer: " + GetScoreTimer());
    }

    // Start is called before the first frame update
    void Start()
    {
        stop_clock_ = true;
        TimeUp_.SetActive(false);
        TimeOverBanner.SetActive(false);
        TimeText = this.gameObject.GetComponent<TMP_Text>();

        for (int i = 0; i < TimeBoarder.Length; i++)
            TimeBoarder[i].GetComponent<Image>().color = _TimeColor[0];

        TimeText.color = _TimeColor[0];
        Setonce = true;
        Setonce2 = true;


        textTimer = GetComponent<TMP_Text>();

        delta_time = PlayerSettings.TimerSet;
         
        TimeSpan span = TimeSpan.FromSeconds(delta_time);
        //string hour = LeadingZero(span.Hours);
        string minute = LeadingZero(span.Minutes);
        string seconds = LeadingZero(span.Seconds);
        _Timing = /*hour + ":" +*/ minute + ":" + seconds;
        textTimer.text = _Timing;

    }


    // Update is called once per frame
    void Update()
    {
        if (stop_clock_ == false)
        {
            if (delta_time >= 0)
            {
                delta_time -= Time.deltaTime;
                StopCoroutine(Wait);

                TimeSpan span = TimeSpan.FromSeconds(delta_time);

                //string hour = LeadingZero(span.Hours);
                string minute = LeadingZero(span.Minutes);
                string seconds = LeadingZero(span.Seconds);
                _Timing = /*hour + ":" +*/ minute + ":" + seconds;
                textTimer.text = _Timing;

                progressSliders.value = delta_time;
            }

            if (delta_time <= 120)
            {
                if (Setonce2)
                {
                    twoMinutes.SetActive(true);
                    Setonce2 = false;
                }

                lerpedColor = Color.Lerp(_TimeColor[0], _TimeColor[1], Mathf.PingPong(Time.time, 1));
                
                for (int i = 0; i < TimeBoarder.Length; i++)
                    TimeBoarder[i].GetComponent<Image>().color = lerpedColor;
            }

            if (delta_time <= 60)
            {

                lerpedColor = Color.Lerp(_TimeColor[0], _TimeColor[2], Mathf.PingPong(Time.time, 1));
              
                for (int i = 0; i < TimeBoarder.Length; i++)
                    TimeBoarder[i].GetComponent<Image>().color = lerpedColor;

                if (Setonce)
                {
                    oneMinutes.SetActive(true);
                    Setonce = false;
                }
            }

            if (delta_time <= 0)
            {
                TimeOverBanner.SetActive(true);
                StartCoroutine(WaitforAnimation());
                GameEvents.OnSetGameInactiveMethod();
            }
        }
        else
        {
            Wait = waitafewSecond(25f);
            StartCoroutine(Wait);
        }
    }


    IEnumerator waitafewSecond(float time)
    {
        yield return new WaitForSeconds(time);
        stop_clock_ = false;
    }


    string LeadingZero(int n)
    {
        return n.ToString().PadLeft(2, '0');
    }


    private IEnumerator WaitforAnimation()
    {
        yield return new WaitForSeconds(3);
        TimeUp_.SetActive(true);
        TimeUp_Text_.text = "Times Up";
        TimeOverBanner.SetActive(false);
    }

    public TMP_Text GetCurrentTimeText()
    {
        return textTimer;
    }

    public float GetScoreTimer()
    {
        return delta_time;
    }


}
