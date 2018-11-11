using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RayController : MonoBehaviour
{

  void Start()
  {

  }

  void Update()
  {
    if (Input.GetMouseButtonDown(0))
    {
      Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
      RaycastHit hit = new RaycastHit();
      if (Physics.Raycast(ray, out hit))
      {
        if (hit.transform.gameObject.CompareTag("enemy"))
        {
          EnemyHit(hit);
        }
      }
    }
  }

  private void EnemyHit(RaycastHit hit)
  {
    hit.transform.gameObject.GetComponent<EnemyController>().Hit();
  }
}
