using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameScreen : MonoBehaviour
{
    public static GameScreen Instance;

    public Text SpeakTheLetter;
    public Text SpeechResult;
    public Text Score;
    [SerializeField] private Text _status;

    public string Status
    {
        get { return _status.text; }
        set
        {
            _status.gameObject.SetActive(true);
            _status.text = value;
            Invoke("DisableStatusMessage", 1f);
        }
    }

    private void Awake()
    {
        Instance = this;
        Player.Instance.onSpeechEvent += OnSpeechEvent;
    }

	private void Start () 
	{
		
	}
	
	private void Update () 
	{
		if(Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }
	}

    public void OnSpeechEvent(Letter currentLetter)
    {
        SpeakTheLetter.gameObject.SetActive(true);
        CancelInvoke("DisableSpeechResultMessage");
    }

    public void OnSpeechEventEnded(bool result)
    {
        SpeakTheLetter.gameObject.SetActive(false);

        //SpeechResult.gameObject.SetActive(true);
        //SpeechResult.text = result ? "Правильно!" : "Не правильно!";
        Invoke("DisableSpeechResultMessage", 1f);
    }

    private void DisableSpeechResultMessage()
    {
        SpeechResult.gameObject.SetActive(false);
    }

    private void DisableStatusMessage()
    {
        _status.gameObject.SetActive(false);
    }
}
