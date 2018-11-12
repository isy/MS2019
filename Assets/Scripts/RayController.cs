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
      print("タップしたよ");
      Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
      print(Input.mousePosition);
      RaycastHit hit = new RaycastHit();
      if (Physics.Raycast(ray, out hit))
      {
        print("当たったよ");
        if (hit.collider.gameObject.CompareTag("Enemy"))
        {
          print("敵だよ");
          EnemyHit(hit);
        }
        else
        {
          print("Tag=" + hit.collider.gameObject.tag);
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
