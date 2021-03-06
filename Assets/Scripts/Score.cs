﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Score : MonoBehaviour
{
  private GameObject scoreNumber;
  public AudioClip scoreSE;
  public int count = 0;

  void Start()
  {
    DontDestroyOnLoad(this);
    scoreNumber = GameObject.Find("Number");
  }

  public void numScore(int score)
  {
    count += score;
    scoreNumber.GetComponent<Text>().text = count.ToString();
    scoreNumber.GetComponent<TypefaceAnimator>().Play();
    AudioSource.PlayClipAtPoint(scoreSE, transform.position);
  }
}
