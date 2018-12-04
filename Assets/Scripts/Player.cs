using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{

    [SerializeField] private Rigidbody2D myRigidbody2D;
    [SerializeField] private Animator myAnimator;
    [SerializeField] private float forwardSpeed = 3f;
    [SerializeField] private float bounceSpeed = 4f;
    [SerializeField] private float rotationTimeIfFlap = 7f;
    [SerializeField] private AudioSource myAudioSource;
    [SerializeField] private AudioClip flapAudioClip, pointAudioClip, deathAudioClip;

    [SerializeField] public int score;

    private bool didFlap;
    public bool isAlive;
    private Button flapButton;

    public static Player instance;

    public void Awake()
    {
        isAlive = true;

        flapButton = GameObject.FindGameObjectWithTag("FlapButton").GetComponent<Button>();
        flapButton.onClick.AddListener(() => FlapPlayer());

        score = 0;

        if (instance == null)
        {
            instance = this;
        }

    }
	
	public void Update ()
    {
        PlayerMovement();
    }

    private void PlayerMovement()
    {
        if (isAlive)
        {
            Vector2 temp = transform.position;
            temp.x += forwardSpeed * Time.deltaTime;
            transform.position = temp;

            if (didFlap)
            {
                didFlap = false;
                myRigidbody2D.velocity = new Vector2(0, bounceSpeed);
                myAudioSource.PlayOneShot(flapAudioClip);
                myAnimator.SetTrigger("Flap");
            }

            if (myRigidbody2D.velocity.y >= 0)
            {
                transform.rotation = Quaternion.Euler(0, 0, 0);
            }

            else
            {
                float angle = 0;
                angle = Mathf.Lerp(0, -90, -myRigidbody2D.velocity.y / rotationTimeIfFlap);
                transform.rotation = Quaternion.Euler(0, 0, angle);
            }
        }
    }

    public void FlapPlayer()
    {
        didFlap = true;
    }

    void OnCollisionEnter2D(Collision2D target)
    {
        if (target.gameObject.tag == "Ground" || target.gameObject.tag == "Pipe")
        {
            if (isAlive)
            {
                isAlive = false;

                myRigidbody2D.velocity = new Vector2(0, bounceSpeed);

                myAnimator.SetTrigger("Death");
                myAudioSource.PlayOneShot(deathAudioClip);

                GameplayController.instance.PlayerDiedShowScore(score);
            }
        }
    }

    void OnTriggerEnter2D(Collider2D target)
    {
        if (isAlive)
        {
            if (target.tag == "Coin")
            {
                score++;
                myAudioSource.PlayOneShot(pointAudioClip);

                GameplayController.instance.SetScore(score);
            }
        }
    }
}
