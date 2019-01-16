using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
  public GameObject effect;
  public GameObject fireEffect;
  public GameObject coldEffect;
  private GameObject KillsObject;
  public AudioClip explosionSE;
  public AudioClip fireSE;
  public AudioClip iceSE;
  public string anchorObjectName;
  public int hp;
  private int randomDamage;
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

  public void Hit(int damage)
  {
    print("Hit");
    anim.SetTrigger("hit");
    randomDamage = UnityEngine.Random.Range((int)(damage - damage * 0.2), (int)(damage + damage * 0.2));
    hp = hp - randomDamage;
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

  public void Tap()
  {
    AudioSource.PlayClipAtPoint(explosionSE, transform.position);
    Hit(10);
  }

  public void Fire()
  {
    AudioSource.PlayClipAtPoint(fireSE, new Vector3(0, 0, 0));
    GameObject fire = Instantiate(fireEffect, transform.position, effect.transform.rotation);
    Hit(50);
    Destroy(fire, 1.0f);

  }
  public void Cold()
  {
    AudioSource.PlayClipAtPoint(iceSE, new Vector3(0, 0, 0));
    GameObject fire = Instantiate(coldEffect, transform.position, effect.transform.rotation);
    Hit(60);
    Destroy(fire, 1.0f);

  }
}
