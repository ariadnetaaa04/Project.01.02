using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControllerX : MonoBehaviour
{
    public bool gameOver;

    public float floatForce = 20f;
    private float gravityModifier = 1.5f;

    private Rigidbody playerRb;
    private bool isOnTheGround;

    public ParticleSystem explosionParticle;
    public ParticleSystem fireworksParticle;

    private AudioSource playerAudio;
    public AudioClip moneySound;
    public AudioClip explodeSound;

    
    private int counter;


    // Start is called before the first frame update
    void Start()
    {
        playerRb = GetComponent<Rigidbody>();
        Physics.gravity *= gravityModifier;
        playerAudio = GetComponent<AudioSource>();

        // Apply a small upward force at the start of the game
        playerRb.AddForce(Vector3.up * 5, ForceMode.Impulse);

    }

    // Update is called once per frame
    void Update()
    {
        
        if (Input.GetKeyDown(KeyCode.Space) && !gameOver && !isOnTheGround)
        {
            isOnTheGround = false;
            playerRb.AddForce(Vector3.up * floatForce, ForceMode.Impulse);
        }
        
    }

    private void OnCollisionEnter(Collision other)
    {
        // if player collides with bomb, explode and set gameOver to true
        if (other.gameObject.CompareTag("Bomb"))
        {
            explosionParticle.Play();
            playerAudio.PlayOneShot(explodeSound, 1.0f);
            gameOver = true;
            Debug.Log("Game Over!");
            Destroy(other.gameObject);

        }

        // if player collides with money, fireworks
        else if (other.gameObject.CompareTag("Money"))
        {
            fireworksParticle.Play();
            playerAudio.PlayOneShot(moneySound, 1.0f);
            Destroy(other.gameObject);
            AddOneToCounter();

        }
        else if (other.gameObject.CompareTag("Ground"))
        {
            isOnTheGround = true;
            gameOver = true;
            Debug.Log("Game Over!");
            GameObject.Find("Main Camera").GetComponent<AudioSource>().Stop();
        }
        

    }

    private void AddOneToCounter()
    {
        counter++;
        {
            Debug.Log(counter);
        }
    }

}
