using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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
    count++;
    GameObject.Find("Score").GetComponent<Text>().text = "Score: " + count.ToString();
  }
}
