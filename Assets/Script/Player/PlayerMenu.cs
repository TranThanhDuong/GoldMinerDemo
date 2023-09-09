using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class PlayerMenu : MonoBehaviour
{
    [SerializeField]
    TextMeshProUGUI levelLB;
    [SerializeField]
    TextMeshProUGUI timerLB;
    [SerializeField]
    TextMeshProUGUI currentScore;
    [SerializeField]
    TextMeshProUGUI targetScore;
    [SerializeField]
    Image progessBar;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(Setup());
    }

    IEnumerator Setup()
    {
        yield return new WaitForSeconds(1f);
        timerLB.text = MissionControl.Instance.Timer.ToMinusAndSec();
        progessBar.fillAmount = (float)MissionControl.Instance.TotalScore / (float)MissionControl.Instance.MissionScore;

        MissionControl.Instance.OnScoreChange += ScoreChange;
        MissionControl.Instance.OnTimeChange += TimeChange;
        yield return new WaitUntil(() => MissionControl.Instance.MissionScore > 0);
        levelLB.text = "Day " + MissionControl.Instance.MissionLevel.ToString();
        currentScore.text = MissionControl.Instance.TotalScore.ToNumberSeparateByComma();
        targetScore.text = MissionControl.Instance.MissionScore.ToNumberSeparateByComma();
    }

    void TimeChange(int timer)
    {
        timerLB.text = timer.ToMinusAndSec();
    }    

    void ScoreChange(int curScore, int addScore)
    {
        currentScore.text = curScore.ToNumberSeparateByComma();
        progessBar.fillAmount = (float)curScore / (float)MissionControl.Instance.MissionScore;
    }    

    public void OnPause()
    {
        DialogManager.Instance.ShowDialog(DialogIndex.DialogPause);
    }    
}
