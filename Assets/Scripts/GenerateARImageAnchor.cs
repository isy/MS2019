using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.iOS;

public class GenerateARImageAnchor : MonoBehaviour
{


  [SerializeField]
  private ARReferenceImage referenceImage;

  [SerializeField]
  private GameObject prefabToGenerate;

  private GameObject imageAnchorGO;
  private UnityARCameraManager ARCameraManager;

  // Use this for initialization
  void Start()
  {
    UnityARSessionNativeInterface.ARImageAnchorAddedEvent += AddImageAnchor;
    UnityARSessionNativeInterface.ARImageAnchorUpdatedEvent += UpdateImageAnchor;
    UnityARSessionNativeInterface.ARImageAnchorRemovedEvent += RemoveImageAnchor;
    ARCameraManager = GameObject.Find("ARCameraManager").GetComponent<UnityARCameraManager>();

  }

  void AddImageAnchor(ARImageAnchor arImageAnchor)
  {
    Debug.Log("image anchor added");
    if (arImageAnchor.referenceImageName == referenceImage.imageName)
    {
      Vector3 position = UnityARMatrixOps.GetPosition(arImageAnchor.transform);
      // Quaternion rotation = UnityARMatrixOps.GetRotation(arImageAnchor.transform);
      // オブジェクトがカメラを向くようにする
      Quaternion rotation = Quaternion.LookRotation(
        ARCameraManager.m_camera.transform.localPosition - position
        );
      // Quaternion rotation = Quaternion.LookRotation(
      //   new Vector3(
      //     ARCameraManager.m_camera.transform.localPosition.x - position.x,
      //     0.0f,
      //     ARCameraManager.m_camera.transform.localPosition.z - position.z
      //   ));
      Debug.Log(string.Format("x:{0:0.######} y:{1:0.######} z:{2:0.######}", position.x, position.y, rotation));
      imageAnchorGO = Instantiate<GameObject>(prefabToGenerate, position, rotation);
      GameManager.instance.api.CallImageAnalysis();
      // imageAnchorGO.tag = "Enemy";
    }
  }

  void UpdateImageAnchor(ARImageAnchor arImageAnchor)
  {
    Debug.Log("image anchor updated");
    if (arImageAnchor.referenceImageName == referenceImage.imageName)
    {
      imageAnchorGO.transform.position = UnityARMatrixOps.GetPosition(arImageAnchor.transform);
      // imageAnchorGO.transform.rotation = UnityARMatrixOps.GetRotation(arImageAnchor.transform);
      imageAnchorGO.transform.rotation = Quaternion.LookRotation(
        new Vector3(
          ARCameraManager.m_camera.transform.localPosition.x - imageAnchorGO.transform.position.x,
          0.0f,
          ARCameraManager.m_camera.transform.localPosition.z - imageAnchorGO.transform.position.z
        ));
      // imageAnchorGO.tag = "Enemy";
    }

  }

  void RemoveImageAnchor(ARImageAnchor arImageAnchor)
  {
    Debug.Log("image anchor removed");
    if (imageAnchorGO)
    {
      GameObject.Destroy(imageAnchorGO);
    }

  }

  void OnDestroy()
  {
    UnityARSessionNativeInterface.ARImageAnchorAddedEvent -= AddImageAnchor;
    UnityARSessionNativeInterface.ARImageAnchorUpdatedEvent -= UpdateImageAnchor;
    UnityARSessionNativeInterface.ARImageAnchorRemovedEvent -= RemoveImageAnchor;

  }

  // Update is called once per frame
  void Update()
  {

  }
}
