using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
// using UnityEngine.Networking;
using Firebase;
using Firebase.Unity.Editor;
using Firebase.Database;

public class NoticeController : MonoBehaviour
{
  private Text textNotice;
  private string oldText;
  private ScrollRect scrollRect;



  void Start()
  {
    DatabaseReference objectRef = FirebaseDatabase.DefaultInstance.GetReference("objects");
    objectRef.ChildAdded += HandleObjectChildAdd;
    scrollRect = this.gameObject.GetComponent<ScrollRect>();
    textNotice = scrollRect.content.GetComponentInChildren<Text>();
  }

  void HandleObjectChildAdd(object sender, ChildChangedEventArgs args)
  {
    if (args.DatabaseError != null)
    {
      Debug.LogError(args.DatabaseError.Message);
      return;
    }
    string json = args.Snapshot.GetRawJsonValue();
    FbObject ob = JsonUtility.FromJson<FbObject>(json);
    oldText = textNotice.text;
    textNotice.text = oldText + ob.tag + "の近くで敵が発見されたようです\n";
    scrollRect.verticalNormalizedPosition = 0;
  }
  // private IEnumerator requestTranslate(string keyword)
  // {
  //   string apiURL = "https://script.google.com/macros/s/AKfycbxpz3lo-kAwl5xLLWQ411-lrLhrwqVO2qK8A-_CKxPp6LqtQaX1/exec?text=" + keyword + "&source=en&target=ja";
  //   var webReq = new UnityWebRequest(apiURL, "GET");

  //   yield return webReq.Send();
  //   if (webReq.isHttpError || webReq.isNetworkError)
  //   {
  //     Debug.LogWarning("Error:" + webReq.responseCode + webReq.error);
  //   }
  //   else
  //   {
  //     Debug.Log("OK:" + webReq.downloadHandler.text);
  //     Translate res = JsonUtility.FromJson<Translate>(webReq.downloadHandler.text);
  //     Debug.Log("翻訳:" + webReq.downloadHandler.text);
  //     string tag = res.tag;
  //     oldText = textNotice.text;
  //     textNotice.text = oldText + tag + "の近くで敵が発見されたようです\n";
  //     scrollRect.verticalNormalizedPosition = 0;
  //   }
  // }

  // [System.Serializable]
  // public class Translate
  // {
  //   public string tag;
  // }
}

internal class FbObject
{
  public string tag;
  public string uid;
}
