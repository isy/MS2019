using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class RayController : MonoBehaviour
{
  void Update()
  {
#if UNITY_EDITOR
    if (EventSystem.current.IsPointerOverGameObject()) return;
#else
      if (EventSystem.current.IsPointerOverGameObject(Input.GetTouch(0).fingerId)) return;
#endif

    if (Input.GetMouseButtonDown(0))
    {
      // GameManager.instance.api.CallImageAnalysis();
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
    hit.transform.gameObject.GetComponent<EnemyController>().Tap();
  }
}
