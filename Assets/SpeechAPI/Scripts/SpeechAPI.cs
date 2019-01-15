#if UNITY_IOS && !UNITY_EDITOR
using System.Runtime.InteropServices;
#endif

// SwiftをObjective-CでexternしたのをUnityで使えるようにImportするよ
namespace SpeechAPI
{
  public static class SpeechRecognizer
  {
    #if UNITY_IOS && !UNITY_EDITOR
      [DllImport("__Internal")]
      private static extern void _sr_requestRecognizerAuthorization();

      [DllImport("__Internal")]
      private static extern bool _sr_startRecord();

      [DllImport("__Internal")]
      private static extern bool _sr_stopRecord();

      [DllImport("__Internal")]
      private static extern void _sr_setCallbackGameObjectName(string callbackGameObjectName);
    #endif
    
    // 上記のメソッドをUnity側で別名で呼び出せるようにするよ
    public static void RequestRecognizerAuthorization()
    {
    #if UNITY_IOS && !UNITY_EDITOR
      _sr_requestRecognizerAuthorization();
    #endif
    }

    public static bool StartRecord()
    {
    #if UNITY_IOS && !UNITY_EDITOR
      return _sr_startRecord();
    #else
      return false;
    #endif
    }

    public static bool StopRecord()
    {
    #if UNITY_IOS && !UNITY_EDITOR
      return _sr_stopRecord();
    #else
      return false;
    #endif
    }

    public static string CallbackGameObjectName
    {
    #if UNITY_IOS && !UNITY_EDITOR
      set { _sr_setCallbackGameObjectName(value); }
    #else
      set {}
    #endif
    }
  }
}