﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TotalScore : MonoBehaviour
{
  [SerializeField]
  private Text scoreText;
  [SerializeField]
  private Text killText;
  [SerializeField]
  private Text timeText;
  private int totalScore = 0;
  private int timeScore = 0;
  private int killScore = 0;

  // Use this for initialization
  void Awake()
  {
    timeScore = (int)GameManager.instance.restOfTime * 10;
    killScore = GameManager.instance.score.count;
    totalScore = timeScore + killScore;
    timeText.text = timeScore.ToString();
    killText.text = killScore.ToString();
  }
  void Start()
  {
    StartCoroutine(ScoreAnimation(2));
  }

  IEnumerator ScoreAnimation(float time)
  {
    yield return new WaitForSeconds(0.01f);
    int before = 0;
    int after = totalScore;
    // int after = 300;
    float elapsedTime = 0.0f;

    while (elapsedTime < time)
    {
      float rate = elapsedTime / time;
      scoreText.text = (before + (after - before) * rate).ToString("f0");

      elapsedTime += Time.deltaTime;

      yield return new WaitForSeconds(0.01f);
    }

    scoreText.text = after.ToString();
  }
}
