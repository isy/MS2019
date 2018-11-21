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

  // Update is called once per frame
  void Update()
  {

  }

  public void writeScore(int scorePoint, string name, string uid)
  {
    Score score = new Score(scorePoint, name);
    string json = JsonUtility.ToJson(score);

    dbReference.Child("scores").Child(uid).SetRawJsonValueAsync(json);
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
}
