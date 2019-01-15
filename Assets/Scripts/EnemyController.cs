using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class EnemyController : MonoBehaviour
{
  public GameObject effect;
  private GameObject KillsObject;
  public AudioClip explosionSE;
  public string anchorObjectName;
  public int hp;
  public int score;

  private Animator anim;

  // Use this for initialization
  void Awake()
  {
    this.gameObject.tag = "Enemy";
    anim = GetComponent<Animator>();
    KillsObject = GameObject.Find("TextKills");
  }

  // Update is called once per frame
  void Update()
  {
  }

  public void Hit()
  {
    print("Hit");
    int min = Convert.ToInt32(hp * 0.1);
    int max = Convert.ToInt32(hp * 0.2);
    int damage = Random.Range(min, max);
    AudioSource.PlayClipAtPoint(explosionSE, transform.position);
    //ADD ikeda 2018/11/27
    anim.SetTrigger("hit");
    hp -= damage;
    if (hp <= 0)
    {
      GameObject g = Instantiate(effect, transform.position + new Vector3(0, 0.04f, 0), effect.transform.rotation);
      Destroy(g, 1.0f);
      Destroy(this.gameObject);
      GameObject anchorObject = GameObject.Find(anchorObjectName);
      Destroy(anchorObject);
      GameManager.instance.score.numScore(score);
      KillsObject.GetComponent<KillsController>().incrementCounter();
    }
  }
}
