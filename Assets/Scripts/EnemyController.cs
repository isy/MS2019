using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
  public GameObject effect;
  public AudioClip explosionSE;

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
    GameObject g = Instantiate(effect, transform.position + new Vector3(0, 0.04f, 0), effect.transform.rotation);
    AudioSource.PlayClipAtPoint(explosionSE, transform.position);
    Destroy(g, 1.0f);
    // Destroy(this.gameObject);
    // transform.root.gameObject.GetComponent<GenerateARImageAnchor>().OnDestroy();
    // transform.root.gameObject.GetComponent<GenerateARImageAnchor>().RemoveImageAnchor();
    Destroy(transform.root.gameObject);
  }
}
