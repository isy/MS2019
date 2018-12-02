using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Firebase;
using Firebase.Unity.Editor;
using Firebase.Database;

public class FirebaseManager : MonoBehaviour
{
  private DatabaseReference dbReference;

  void Start()
  {
    FirebaseApp.DefaultInstance.SetEditorDatabaseUrl("https://ms2019-835dd.firebaseio.com/");

    dbReference = FirebaseDatabase.DefaultInstance.RootReference;
  }

  public void writeScore(int scorePoint, string name, string uid)
  {
    Score score = new Score(scorePoint, name);
    string json = JsonUtility.ToJson(score);

    dbReference.Child("scores").Child(uid).SetRawJsonValueAsync(json);
  }

  public void writeObject(string tag)
  {
    string uid = GameManager.instance.uid;
    Object ob = new Object(uid, tag);
    string json = JsonUtility.ToJson(ob);

    dbReference.Child("objects").Push().SetRawJsonValueAsync(json);
  }

  public class Score
  {
    public int score;
    public string name;

    public Score()
    {

    }

    public Score(int score, string name)
    {
      this.score = score;
      this.name = name;
    }
  }
  public class Object
  {
    public string uid;
    public string tag;

    public Object()
    {

    }

    public Object(string uid, string tag)
    {
      this.uid = uid;
      this.tag = tag;
    }
  }
}
