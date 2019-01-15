using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RayController : MonoBehaviour
{
  [SerializeField]
  private GameObject HandObject;
  public float speed = 20;
  void Update()
  {
    if (Input.GetMouseButtonDown(0))
    {
      // GameManager.instance.api.CallImageAnalysis();
      GameObject duplicateHand = (GameObject)Instantiate(HandObject, Camera.main.transform.position, Quaternion.Euler(-805, -118, 30));
      Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

      Vector3 direction = ray.direction;

      print(ray.direction);
      duplicateHand.GetComponent<Rigidbody>().velocity = direction * speed;

      RaycastHit hit = new RaycastHit();
      if (Physics.Raycast(ray, out hit))
      {
        // if (hit.collider.gameObject.CompareTag("Enemy"))
        // {
        //   EnemyHit(hit);
        // }
      }
    }
  }

  private void EnemyHit(RaycastHit hit)
  {
    hit.transform.gameObject.GetComponent<EnemyController>().Hit();
  }
}
