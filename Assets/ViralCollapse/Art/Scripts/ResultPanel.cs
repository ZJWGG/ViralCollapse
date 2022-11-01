using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ResultPanel : MonoBehaviour
{
    //private static ResultPanel instance;
    public static ResultPanel Instance;
    //{
    //    get
    //    {
    //        if (instance == null)
    //        {
    //            instance =new ResultPanel();
    //        }
    //        return instance;
    //    }
    //}
    public GameObject leftTop;
    public GameObject middle;
    public Text textScore;
    public Text textTime;
    public Text textLastScore;
    int score=0;
    int time=60;
    AudioSource audioSource;
    public AudioClip addScoreClip;
    bool isGameEnd;
    private void Awake()
    {
        Instance = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        leftTop.SetActive(true);
        middle.SetActive(false);
        textScore.text = string.Format("积分：{0}", score);
        textTime.text = string.Format("倒计时：{0}s", time);
        InvokeRepeating("SetTime", 1f, 1f);
    }

   
    public void AddScore()
    {
        score++;
        textScore.text = string.Format("积分：{0}", score);
        audioSource.PlayOneShot(addScoreClip);
    }
    public void SetTime()
    {
        time -= 1;
        if (time <= 0)
        {
            time = 0;
            CancelInvoke("SetTime");
            leftTop.SetActive(false);
            middle.SetActive(true);
            textLastScore.text = string.Format("积分：{0}", score);
            isGameEnd = true;
            return;
        }
        textTime.text = string.Format("倒计时：{0}s", time);
    }
    public void OnClickBtnRestart()
    {
        SceneManager.LoadScene(0);
    }
//    public void Quit()
//    {
//#if UNITY_EDITOR
//        UnityEditor.EditorApplication.isPlaying = false;
//#else
//Application.Quit();
//#endif
//    }
}
