using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
  public static GameManager instance;
  public Score score;
  // Use this for initialization
  void Start()
  {
    if (instance == null) instance = this;
    DontDestroyOnLoad(this);
  }

  // Update is called once per frame
  void Update()
  {

  }
}
