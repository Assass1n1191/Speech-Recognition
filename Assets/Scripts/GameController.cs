using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public static GameController Instance;

    private Letter _currentListenLetter;
    public bool IsListening = false;
    private int _totalLetters;
    private int _lettersPassed;

    public int Score
    {
        get { return _lettersPassed; }
        set
        {
            _lettersPassed = value;
            GameScreen.Instance.Score.text = "Рахунок: " + _lettersPassed/* + "/" + _totalLetters*/;
        }
    }

    private void Awake()
    {
        Instance = this;
    }

	private void Start ()
	{
        iTween.CameraFadeAdd();
        iTween.CameraFadeFrom(iTween.Hash("amount", 1f, "time", 0.75f, "oncompletetarget", gameObject, "oncomplete", "StartGame"));

	    Player.Instance.onSpeechEvent += OnSpeechEvent;
	}

    private void Update()
    {
        if (!IsListening) return;

#if UNITY_EDITOR
        if (Input.GetMouseButtonDown(0))
        {
            OnSpeechEventEnded(true);
        }
        else if (Input.GetMouseButton(1))
        {
            OnSpeechEventEnded(false);
        }
#endif
    }
	
	public void StartGame()
    {
        iTween.CameraFadeDestroy();
    }

    public void OnSpeechEvent(Letter letterToListen)
    {
        Time.timeScale = 0.4f;
        _currentListenLetter = letterToListen;
        IsListening = true;
        //Invoke("OnWrongSpeech", 1.7f);
        //Invoke("StopListen", 1.1f);

        if (SpeechRecognizerManager.IsAvailable())
            MySpeechRecognizer.Instance._speechManager.StartListening(5, "en-US");
    }

    public void OnSpeechEventEnded(bool isPassed)
    {
        GameScreen.Instance.OnSpeechEventEnded(isPassed);

        if (SpeechRecognizerManager.IsAvailable())
            MySpeechRecognizer.Instance._speechManager.StopListening();

        _totalLetters++;

        if (isPassed)
        {
            //CancelInvoke("OnWrongSpeech");
            Score++;
        }

        Color letterHighlight = isPassed ? Color.green : Color.red;

        Time.timeScale = 1f;
        _currentListenLetter.gameObject.GetComponent<SpriteRenderer>().color = letterHighlight;
        iTween.ColorTo(_currentListenLetter.gameObject, new Color(0f, 1f, 0f, 0f), 0.6f);
        iTween.MoveBy(_currentListenLetter.gameObject, new Vector3(0f, 7f, 0f), 0.6f);

        _currentListenLetter = null;
        IsListening = false;
    }

    public void OnWrongSpeech()
    {
        OnSpeechEventEnded(false);
    }

    public void StopListen()
    {
#if UNITY_ANDROID
        //GameScreen.Instance.Status = "Stop Listen" + MySpeechRecognizer.Instance.SpeechEnded;

        //if(MySpeechRecognizer.Instance.SpeechEnded)
            //MySpeechRecognizer.Instance.OnSpeechResults();

        if (MySpeechRecognizer.Instance._speechManager != null)
            MySpeechRecognizer.Instance._speechManager.StopListening();
#endif

    }

    public void CheckSpeechResults(string[] results)
    {
        string targetString = _currentListenLetter.letterName.ToLower();

        for(int i = 0; i < results.Length; i++)
        {
            results[i] = results[i].ToLower();

            if (results[i].Equals(targetString))
            {
                OnSpeechEventEnded(true);
                break;
            }
        }

        OnSpeechEventEnded(false);
    }
}
