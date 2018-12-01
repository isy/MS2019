using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using ISpeechAPI;

public class Recognization : MonoBehaviour {

  bool push = false;

	// Use this for initialization
	void Start () {
		
	}

  public void PushDown() {
    #if UNITY_EDITOR
      if (EventSystem.current.IsPointerOverGameObject()) return;
    #else
      if (EventSystem.current.IsPointerOverGameObject(Input.GetTouch(0).fingerId))
      {
        push = true;
      }
    #endif
  }

  public void PushUp() {
    #if UNITY_EDITOR
      if (EventSystem.current.IsPointerOverGameObject()) return;
    #else
      if (EventSystem.current.IsPointerOverGameObject(Input.GetTouch(0).fingerId)) 
      {
        push = false;
      }
    #endif
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
