using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{

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
    // Destroy(this.gameObject);
    Destroy(transform.root.gameObject);
  }
}
