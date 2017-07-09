using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public static Player Instance;

    private Animator anim;
    private Rigidbody rb;

    public float movementSpeed = 2.4f;

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
}
