using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CountDownController : MonoBehaviour
{
  [SerializeField]
  private Text textCountDown;
  [SerializeField]
  private Image imageMask;
  [SerializeField]
  private AudioClip countDownSE;

  // Use this for initialization
  void Start()
  {
    StartCoroutine(CountdownCoroutine());
  }

  // Update is called once per frame
  void Update()
  {

  }
  IEnumerator CountdownCoroutine()
  {
    imageMask.gameObject.SetActive(true);
    textCountDown.gameObject.SetActive(true);

    AudioSource.PlayClipAtPoint(countDownSE, new Vector3(0.0f, 0.0f, 0.0f));
    textCountDown.text = "3";
    yield return new WaitForSeconds(1.0f);

    textCountDown.text = "2";
    yield return new WaitForSeconds(1.0f);

    textCountDown.text = "1";
    yield return new WaitForSeconds(1.0f);

    textCountDown.text = "Start!";
    yield return new WaitForSeconds(1.0f);

    imageMask.gameObject.SetActive(false);
    textCountDown.gameObject.SetActive(false);
    GameManager.instance.gameTimer.isStarted = true;
  }
}
