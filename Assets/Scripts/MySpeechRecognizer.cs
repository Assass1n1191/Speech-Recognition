using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MySpeechRecognizer : MonoBehaviour
{
    public static MySpeechRecognizer Instance;
    public SpeechRecognizerManager _speechManager;
    public bool SpeechEnded = false;

    private void Awake()
    {
        Instance = this;
    }

	private void Start () 
	{
	    if (Application.platform != RuntimePlatform.Android)
	    {
	        Debug.Log("What a heck man? Available only on Andoid.");
	        return;
	    }

	    if (!SpeechRecognizerManager.IsAvailable())
	    {
	        GameScreen.Instance.Status = "Speech recognition is not available on this device.";
            //Debug.Log("Speech recognition is not available on this device.");
	        return;
	    }

	    GameScreen.Instance.Status = "Speech recognizer initialized";
        _speechManager = new SpeechRecognizerManager(gameObject.name);
    }

    private void OnDestroy()
    {
        if(_speechManager != null)
            _speechManager.Release();
    }

    void OnSpeechEvent(string e)
    {
        switch (int.Parse(e))
        {
            case SpeechRecognizerManager.EVENT_SPEECH_READY:
                //GameScreen.Instance.Status = "Speech ready";
                break;
            case SpeechRecognizerManager.EVENT_SPEECH_BEGINNING:
                //GameScreen.Instance.Status = "Speech began";
                SpeechEnded = false;
                break;
            case SpeechRecognizerManager.EVENT_SPEECH_END:
                SpeechEnded = true;
                //_speechManager.StartListening(5, "en-US");
                _speechManager.StopListening();
                //GameScreen.Instance.Status = "End of speaking";
                break;
        }
    }

    public void OnSpeechResults(string results)
    {
        string[] texts = results.Split(new string[] { SpeechRecognizerManager.RESULT_SEPARATOR }, System.StringSplitOptions.None);
        //GameScreen.Instance.Status = "Speech results:\n   " + string.Join("\n   ", texts);

        GameController.Instance.CheckSpeechResults(texts);
    }

    void OnSpeechError(string error)
    {
        switch (int.Parse(error))
        {
            case SpeechRecognizerManager.ERROR_AUDIO:
                GameScreen.Instance.Status = ("Error during recording the audio.");
                break;
            case SpeechRecognizerManager.ERROR_CLIENT:
                //GameScreen.Instance.Status = ("Error on the client side.");
                break;
            case SpeechRecognizerManager.ERROR_INSUFFICIENT_PERMISSIONS:
                GameScreen.Instance.Status = ("Insufficient permissions. Do the RECORD_AUDIO and INTERNET permissions have been added to the manifest?");
                break;
            case SpeechRecognizerManager.ERROR_NETWORK:
                GameScreen.Instance.Status = ("A network error occured. Make sure the device has internet access.");
                break;
            case SpeechRecognizerManager.ERROR_NETWORK_TIMEOUT:
                GameScreen.Instance.Status = ("A network timeout occured. Make sure the device has internet access.");
                break;
            case SpeechRecognizerManager.ERROR_NO_MATCH:
                //GameScreen.Instance.Status = ("No recognition result matched.");
                GameController.Instance.OnSpeechEventEnded(false);
                break;
            case SpeechRecognizerManager.ERROR_NOT_INITIALIZED:
                GameScreen.Instance.Status = ("Speech recognizer is not initialized.");
                break;
            case SpeechRecognizerManager.ERROR_RECOGNIZER_BUSY:
                GameScreen.Instance.Status = ("Speech recognizer service is busy.");
                break;
            case SpeechRecognizerManager.ERROR_SERVER:
                GameScreen.Instance.Status = ("Server sends error status.");
                break;
            case SpeechRecognizerManager.ERROR_SPEECH_TIMEOUT:
                GameScreen.Instance.Status = ("No speech input.");
                break;
            default:
                break;
        }
    }

}
