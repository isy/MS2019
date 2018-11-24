using System;
using System.Text;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class APIController : MonoBehaviour
{
  private string encode;
  public void CallImageAnalysis()
  {
    StartCoroutine(getScreenCapture());

    StartCoroutine(requestVisionAPI(encode));
  }

  private IEnumerator getScreenCapture()
  {
    Texture2D texture = new Texture2D(Screen.width, Screen.height);
    yield return new WaitForEndOfFrame();
    texture.ReadPixels(new Rect(0, 0, Screen.width, Screen.height), 0, 0);

    byte[] jpg = texture.EncodeToJPG();
    encode = Convert.ToBase64String(jpg);

  }

  private IEnumerator requestVisionAPI(string base64Image)
  {
    string json = Resources.Load<TextAsset>("secret").ToString();
    API api = JsonUtility.FromJson<API>(json);

    string apiURL = "https://vision.googleapis.com/v1/images:annotate?key=" + api.visionApiKey;

    var req = new requestBody();
    req.requests = new List<AnnotateImageRequest>();

    var request = new AnnotateImageRequest();

    request.image = new Image();
    request.image.content = base64Image;

    request.features = new List<Feature>();
    var feature = new Feature();
    feature.type = FeatureType.LABEL_DETECTION.ToString();
    feature.maxResults = 10;
    request.features.Add(feature);

    req.requests.Add(request);


    string jsonReqBody = JsonUtility.ToJson(req);


    var webReq = new UnityWebRequest(apiURL, "POST");
    byte[] postData = Encoding.UTF8.GetBytes(jsonReqBody);
    webReq.uploadHandler = (UploadHandler)new UploadHandlerRaw(postData);
    webReq.downloadHandler = (DownloadHandler)new DownloadHandlerBuffer();
    webReq.SetRequestHeader("Content-Type", "application/json");

    yield return webReq.SendWebRequest();

    if (webReq.isHttpError || webReq.isNetworkError)
    {
      Debug.LogWarning(webReq.error);
    }
    else
    {
      var responses = JsonUtility.FromJson<responseBody>(webReq.downloadHandler.text);
      print(responses);
    }


  }
  [System.Serializable]
  public class requestBody
  {
    public List<AnnotateImageRequest> requests;
  }
  [System.Serializable]
  public class AnnotateImageRequest
  {
    public Image image;
    public List<Feature> features;
    //public string imageContext;
  }
  [System.Serializable]
  public class Image
  {
    public string content;
    // public ImageSource source;
  }

  public class ImageSource
  {
    public string imageUri;
  }
  [System.Serializable]
  public class Feature
  {
    public string type;
    public int maxResults;
  }
  [System.Serializable]
  public enum FeatureType
  {
    TYPE_UNSPECIFIED,
    FACE_DETECTION,
    LANDMARK_DETECTION,
    LOGO_DETECTION,
    LABEL_DETECTION,
    TEXT_DETECTION,
    SAFE_SEARCH_DETECTION,
    IMAGE_PROPERTIES
  }

  public class responseBody
  {
    public List<AnnotateImageResponse> responses;
  }
  public class AnnotateImageResponse
  {
    public List<EntityAnnotation> labelAnnotations;
  }

  public class EntityAnnotation
  {
    public string mid;
    public string locale;
    public string description;
    public float score;
    public float confidence;
    public float topicality;
    public BoundingPoly boundingPoly;
    public List<LocationInfo> locations;
    public List<Property> properties;
  }

  public class BoundingPoly
  {
    public List<Vertex> vertices;
  }

  public class Vertex
  {
    public float x;
    public float y;
  }

  public class Property
  {
    string name;
    string value;
  }
}

internal class API
{
  public string visionApiKey;
}
