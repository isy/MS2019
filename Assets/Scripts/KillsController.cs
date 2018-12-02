using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class KillsController : MonoBehaviour
{

  [SerializeField]
  public int killsCount = 0;
  private Text killText;

  void Awake()
  {
    killText = this.gameObject.GetComponent<Text>();
    killText.text = killsCount.ToString();
  }

  public void incrementCounter()
  {
    killsCount++;
    killText.text = killsCount.ToString();
    if (killsCount >= 1)
    {
      GameManager.instance.gameTimer.isStarted = false;
      SceneManager.LoadSceneAsync("Score");
    }

  }
}
