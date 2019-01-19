using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class ScoreButtonHander : MonoBehaviour
{
  [SerializeField]
  private InputField nameField;
  public FirebaseManager fb;
  // Use this for initialization
  void Start()
  {

  }

  // Update is called once per frame
  void Update()
  {

  }

  public void OnClick()
  {
    string name = nameField.text;
    int score = GameManager.instance.score.count + (int)GameManager.instance.restOfTime * 10;
    string uid = GameManager.instance.uid;
    fb.writeScore(score, name, uid);
    GameManager.instance.initInstance();
    SceneManager.LoadSceneAsync("Menu");
  }
}
