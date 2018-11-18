using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameTimer : MonoBehaviour
{
  private float totalTime;
  [SerializeField]
  private int minute = 0;
  [SerializeField]
  private float seconds = 60;

  public bool isStarted = false;
  public Text timeText;
  // Use this for initialization
  void Start()
  {
    timeText.text = calcTime(minute, seconds);
  }

  // Update is called once per frame
  void Update()
  {
    if (isStarted) CountTime();
  }

  public void CountTime()
  {
    totalTime = minute * 60 + seconds;
    totalTime -= Time.deltaTime;
    if (totalTime < 0) return;
    minute = (int)totalTime / 60;
    seconds = totalTime - minute * 60;
    if (totalTime < 10) timeText.color = new Color(240f / 255f, 0f / 255f, 10f / 255f);

    timeText.text = calcTime(minute, seconds);
  }

  private string calcTime(int minute, float seconds)
  {
    return minute.ToString("00") + ":" + ((int)seconds).ToString("00");
  }
}
