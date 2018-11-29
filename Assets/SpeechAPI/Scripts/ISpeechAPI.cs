namespace SpeechAPI
{
  // UnityにメッセージのStatus送るやーつ
    public interface ISpeechAPI
    {
        void OnRecognized(string transcription);
        void OnError(string description);
        void OnAuthorized();
        void OnUnauthorized();
        void OnAvailable();
        void OnUnavailable();
    }
}