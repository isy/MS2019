using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TotalScore : MonoBehaviour
{
  [SerializeField]
  private Text scoreText;
  // Use this for initialization
  void Start()
  {
    StartCoroutine(ScoreAnimation(2));
  }

  // Update is called once per frame
  void Update()
  {

  }

  IEnumerator ScoreAnimation(float time)
  {
    yield return new WaitForSeconds(0.01f);
    int before = 0;
    // int after = GameManager.instance.score.count;
    int after = 300;
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
