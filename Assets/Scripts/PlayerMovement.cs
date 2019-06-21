using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class PlayerMovement : MonoBehaviour
{
    public float speed;

    Rigidbody rb;
    Canvas TextPlayer;
    SpawnPlayer ScripSpawnPlayer;
    // Use this for initialization
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        ScripSpawnPlayer = GameObject.Find("Spawn Player And Safe Zone").GetComponent<SpawnPlayer>();
        TextPlayer = GetComponentInChildren<Canvas>();
    }

    private void Update()
    {
        PlayerIndicator();
        Inputs();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        MethodRollingTheBallPlayer();
    }

    void Inputs()
    {
        if (Input.GetKey(KeyCode.Escape))
        {
            Application.Quit();
        }
    }

    void PlayerIndicator()
    {
        TextPlayer.transform.LookAt(TextPlayer.transform.position + Camera.main.transform.rotation * Vector3.forward, Camera.main.transform.rotation * Vector3.up);
    }

    void MethodRollingTheBallPlayer()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        rb.AddForce(new Vector3(moveHorizontal, 0.0f, moveVertical) * speed);
    }

    private void OnTriggerStay(Collider collider)
    {
        if(collider.gameObject.name == "Spawn Player And Safe Zone" && !ScripSpawnPlayer.PlayerIsSafe)
        {
            ScripSpawnPlayer.PlayerIsSafe = true;
        }
    }

    private void OnTriggerExit(Collider collider)
    {
        if (collider.gameObject.name == "Spawn Player And Safe Zone" && ScripSpawnPlayer.PlayerIsSafe)
        {
            ScripSpawnPlayer.PlayerIsSafe = false;
        }
    }

    private void OnTriggerEnter(Collider collider)
    {
        if(collider.gameObject.name == "ColliderRespawnPlayer")
        {
            transform.position = GameObject.Find("Spawn Player And Safe Zone").transform.position;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.name == "Enemy")
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }

    
}
