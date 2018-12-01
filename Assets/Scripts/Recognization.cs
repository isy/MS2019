using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Recognization : MonoBehaviour {

  bool push = false;

	// Use this for initialization
	void Start () {
		
	}

  // ボタンをクリックした時の処理
  public void OnClick() {
    if (!IsPointerOverUIObject())
    {
      Debug.Log("Button click!");
    }
  }

  public void PushDown() {
    if (Input.touchCount > 0 && !IsPointerOverUIObject())
    {
      push = true;
      Debug.Log("Button push!");
    }
  }

  public void PushUp() {
    if (Input.touchCount > 0 && !IsPointerOverUIObject())
    {
      push = false;
      Debug.Log("Button release!");
    }
  }
	
	// Update is called once per frame
	void Update () {
		if (push) {
      SpeechAPI.SpeechAPI.StartRecord();
    } else {
      SpeechAPI.SpeechAPI.StopRecord();
    }
	}

  private bool IsPointerOverUIObject()
  {
    PointerEventData eventDataCurrentPosition = new PointerEventData(EventSystem.current);
    eventDataCurrentPosition.position = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
    List<RaycastResult> results = new List<RaycastResult>();
    EventSystem.current.RaycastAll(eventDataCurrentPosition, results);
    return results.Count > 0;
  }

}
