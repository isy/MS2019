using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
  public static GameManager instance;
  public Score score;
  public GameTimer gameTimer;
  public APIController api;

  public int restOfTime = 0;
  public string uid;
  // Use this for initialization
  void Start()
  {
    if (instance == null)
    {
      instance = this;
      instance.uid = Guid.NewGuid().ToString("N").Substring(0, 18);
    }
    DontDestroyOnLoad(this);
  }

  // Update is called once per frame
  void Update()
  {
  }

  public void initInstance()
  {
    Destroy(this.score.gameObject);
    Destroy(this.gameObject);
  }
}
