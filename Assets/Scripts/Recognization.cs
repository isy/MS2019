using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Recognization : MonoBehaviour
{

  bool push = false;
  [SerializeField]
  private Text RecText;
  private Image MicImage;
  [SerializeField]
  private Sprite _on;
  [SerializeField]
  private Sprite _off;

  // Use this for initialization
  void Start()
  {
    SpeechAPI.SpeechRecognizer.RequestRecognizerAuthorization();
    MicImage = this.gameObject.GetComponent<Image>();
  }

  public void PushDown()
  {
#if UNITY_EDITOR
    if (EventSystem.current.IsPointerOverGameObject()) return;
#else
      if (EventSystem.current.IsPointerOverGameObject(Input.GetTouch(0).fingerId))
      {
        MicImage.sprite = _on;
        push = true;
      }
#endif
  }

  public void PushUp()
  {
#if UNITY_EDITOR
    if (EventSystem.current.IsPointerOverGameObject()) return;
#else
      if (EventSystem.current.IsPointerOverGameObject(Input.GetTouch(0).fingerId)) 
      {
        MicImage.sprite = _off;
        push = false;
      }
#endif
  }

  // Update is called once per frame
  void Update()
  {
    if (push)
    {
      SpeechAPI.SpeechRecognizer.CallbackGameObjectName = gameObject.name;
      SpeechAPI.SpeechRecognizer.StartRecord();
    }
    else
    {
      SpeechAPI.SpeechRecognizer.StopRecord();
    }
  }

  public void OnCallback(string message)
  {
    Debug.Log("ユニティだ");
    RecText.text = message;
    Debug.Log(message);
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
