using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public static Player Instance;

    private Animator anim;
    private Rigidbody rb;

    public float movementSpeed = 2.4f;

    public delegate void OnSpeechEvent(Letter currentLetter);
    public event OnSpeechEvent onSpeechEvent;

    private void Awake()
    {
        Instance = this;
    }

    private void Start ()
    {
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody>();

        anim.SetFloat("MoveSpeed", 1.0f);
	}
	
	private void Update ()
    {
        transform.Translate(Vector3.forward * movementSpeed * Time.deltaTime);
	}

    private void OnTriggerEnter(Collider col)
    {

        if (col.tag.Equals("Letter"))
        {
            if (col is BoxCollider) //Starting listen
            {
                if (onSpeechEvent != null)
                    onSpeechEvent(col.gameObject.GetComponent<Letter>());
            }

            if (col is CapsuleCollider) //Last point
            {
                GameController.Instance.StopListen();
            }


            //GameController.Instance.OnSpeechEvent(col.gameObject.GetComponent<Letter>());
        }
    }
}
