using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class SceneController : MonoBehaviour
{
  public AudioClip startSE;

  private AsyncOperation async;
  [SerializeField]
  private GameObject loadUI;
  [SerializeField]
  private GameObject titleUI;
  [SerializeField]
  private Image loadSlider;

  // Use this for initialization
  void Start()
  {
    loadUI.SetActive(false);
  }

  // Update is called once per frame
  void Update()
  {
    if (Input.GetMouseButtonDown(0))
    {
#if UNITY_EDITOR
      if (EventSystem.current.IsPointerOverGameObject()) return;
#else
      if (EventSystem.current.IsPointerOverGameObject(Input.GetTouch(0).fingerId)) return;
#endif
      AudioSource.PlayClipAtPoint(startSE, transform.position);
      NextScene();
    }
  }

  public void NextScene()
  {
    loadUI.SetActive(true);
    titleUI.SetActive(true);

    StartCoroutine("LoadData");
  }

  IEnumerator LoadData()
  {
    async = SceneManager.LoadSceneAsync("Main");

    while (!async.isDone)
    {
      loadSlider.fillAmount = async.progress;
      yield return null;
    }
  }
}
