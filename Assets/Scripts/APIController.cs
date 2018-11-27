using System;
using System.Text;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class APIController : MonoBehaviour
{
  RenderTexture arTexture;
  Texture2D texture;
  public FirebaseManager fb;
  private string encode;

  void Start()
  {
    texture = new Texture2D(Screen.width, Screen.height, TextureFormat.RGBA32, false);
    arTexture = new RenderTexture(texture.width, texture.height, 24, RenderTextureFormat.ARGB32);
  }
  public void CallImageAnalysis()
  {
    StartCoroutine(getScreenCapture());

    StartCoroutine(requestVisionAPI(encode));
  }

  private IEnumerator getScreenCapture()
  {
    Graphics.Blit(null, arTexture);
    // Texture2D texture = new Texture2D(Screen.width, Screen.height, TextureFormat.RGBA32, false);
    texture.ReadPixels(new Rect(0, 0, arTexture.width, arTexture.height), 0, 0);
    texture.Apply();
    byte[] jpg = texture.EncodeToJPG();
    yield return encode = Convert.ToBase64String(jpg);

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
    // feature.type = FeatureType.LANDMARK_DETECTION.ToString();
    feature.maxResults = 1;
    request.features.Add(feature);

    req.requests.Add(request);


    string jsonReqBody = JsonUtility.ToJson(req);


    var webReq = new UnityWebRequest(apiURL, "POST");
    byte[] postData = Encoding.UTF8.GetBytes(jsonReqBody);
    print(req.requests[0].image.content);
    print(req.requests[0].features[0].type);
    webReq.uploadHandler = (UploadHandler)new UploadHandlerRaw(postData);
    webReq.downloadHandler = (DownloadHandler)new DownloadHandlerBuffer();
    webReq.SetRequestHeader("Content-Type", "application/json");

    // yield return webReq.SendWebRequest();
    yield return webReq.Send();

    if (webReq.isHttpError || webReq.isNetworkError)
    {
      Debug.LogWarning("Error:" + webReq.responseCode + webReq.error);
    }
    else
    {
      Debug.Log("OK:" + webReq.downloadHandler.text);
      var responses = JsonUtility.FromJson<responseBody>(webReq.downloadHandler.text);
      string tag = responses.responses[0].labelAnnotations[0].description;
      fb.writeObject(tag);
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
  [System.Serializable]
  public class responseBody
  {
    public List<AnnotateImageResponse> responses;
  }
  [System.Serializable]
  public class AnnotateImageResponse
  {
    public List<EntityAnnotation> labelAnnotations;
  }
  [System.Serializable]
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
