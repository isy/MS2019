using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
  public GameObject effect;
  public AudioClip explosionSE;
  public string anchorObjectName;
  public int hp;
  public int score;

  // Use this for initialization
  void Start()
  {
    this.gameObject.tag = "Enemy";
  }

  // Update is called once per frame
  void Update()
  {

  }

  public void Hit()
  {
    print("Hit");
    AudioSource.PlayClipAtPoint(explosionSE, transform.position);
    hp--;
    if (hp <= 0)
    {
      GameObject g = Instantiate(effect, transform.position + new Vector3(0, 0.04f, 0), effect.transform.rotation);
      Destroy(g, 1.0f);
      Destroy(this.gameObject);
      GameObject anchorObject = GameObject.Find(anchorObjectName);
      Destroy(anchorObject);
      GameManager.instance.score.numScore(score);
    }
  }
}
