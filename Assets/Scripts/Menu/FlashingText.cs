using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UniRx;

public class FlashingText : MonoBehaviour
{
  [SerializeField]
  private float angularFrequency = 5f;
  static readonly float DeltaTime = 0.0333f;
  private Text text;
  // Use this for initialization
  void Start()
  {
    float time = 0.0f;
    text = GetComponent<Text>();
    Observable.Interval(TimeSpan.FromSeconds(DeltaTime)).Subscribe(_ =>
      {
        time += angularFrequency * DeltaTime;
        var color = text.color;
        color.a = Mathf.Sin(time) * 0.5f + 0.5f;
        text.color = color;
      }).AddTo(this);
  }

  // Update is called once per frame
  void Update()
  {

  }
}
