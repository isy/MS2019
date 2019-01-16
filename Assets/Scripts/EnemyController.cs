using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
    AudioSource.PlayClipAtPoint(explosionSE, transform.position);
        //ADD ikeda 2018/11/27
        anim.SetTrigger("hit");
        hp--;
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
