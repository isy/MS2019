using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Recognization : MonoBehaviour {

  bool push = false;

	// Use this for initialization
	void Start () {
		
	}

  // ボタンをクリックした時の処理
  public void OnClick() {
    Debug.Log("Button click!");
  }

  public void PushDown() {
    push = true;
    Debug.Log("Button push!");
  }

  public void PushUp() {
    push = false;
    Debug.Log("Button release!");
  }
	
	// Update is called once per frame
	void Update () {
		if (push) {
      SpeechAPI.SpeechAPI.StartRecord();
    } else {
      SpeechAPI.SpeechAPI.StopRecord();
    }
	}
}
