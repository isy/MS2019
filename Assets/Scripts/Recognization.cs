using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Recognization : MonoBehaviour {

  bool push = false;

	// Use this for initialization
	void Start () {
    SpeechAPI.SpeechRecognizer.RequestRecognizerAuthorization();
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
      SpeechAPI.SpeechRecognizer.CallbackGameObjectName = gameObject.name;
      SpeechAPI.SpeechRecognizer.StartRecord();
    } else {
      SpeechAPI.SpeechRecognizer.StopRecord();
    }
	}

  public void OnAuthorized()
    {
        SpeechAPI.SpeechRecognizer.StartRecord();
    }

    public void OnRecognized(string transcription)
    {
        Debug.Log("OnRecognized: " + transcription);
    }

    public void OnError(string description) { }
    public void OnUnauthorized() { }
    public void OnAvailable() { }
    public void OnUnavailable() { }
}
