﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RayController : MonoBehaviour
{
  int count = 0;
  void Start()
  {

  }

  void Update()
  {
    if (Input.GetMouseButtonDown(0))
    {
      GameManager.instance.api.CallImageAnalysis();
      Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
      print(Input.mousePosition);
      RaycastHit hit = new RaycastHit();
      if (Physics.Raycast(ray, out hit))
      {
        if (hit.collider.gameObject.CompareTag("Enemy"))
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
