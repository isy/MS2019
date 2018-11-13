using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Score : MonoBehaviour
{
  int count = 0;

  public void numScore(int score)
  {
    count += score;
    GameObject.Find("Score").GetComponent<Text>().text = "Score: " + count.ToString();
  }
}
