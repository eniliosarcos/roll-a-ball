using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LaserGuns : MonoBehaviour
{
    float 
        timeLooking,
        timeIsLock,
        range;

    public AudioClip laser;
    AudioSource audioSource;
    LineRenderer line;
    Transform player;
    RaycastHit hit;
    Vector3 fwd;

    bool isLock;

    // Use this for initialization
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        player = GameObject.Find("Player").transform;
        line = GetComponent<LineRenderer>();
        line.enabled = false;
    }
    // Update is called once per frame
    void Update()
    {
        MethodRaycast();
        MethodRaycastFollowPlayerAndShoot();      
    }

    void MethodRaycast()
    {
        RaycastHit hit;

        range = Vector3.Distance(player.position, transform.position);
        fwd = transform.TransformDirection(Vector3.forward) * range;
        Debug.DrawRay(transform.position, fwd, Color.green);

        if (Physics.Raycast(transform.position, fwd, out hit))
        {
            if (hit.collider.gameObject.name == "Player" && !isLock)
            {
                timeIsLock = 0.0f;
                timeLooking += Time.deltaTime;

                if ((int)timeLooking == 3)
                {
                    isLock = true;
                }
            }
            else
            {
                timeLooking = 0.0f;
            }
        }
    }

    void MethodRaycastFollowPlayerAndShoot()
    {
        if (!isLock)
        {
            Vector3 Target = player.position - transform.position;
            transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.LookRotation(Target), Time.deltaTime * 100);
        }
        else
        {
            timeIsLock += Time.deltaTime;
            if (timeIsLock > 0.6f)
            {
                line.enabled = true;
                if(!audioSource.isPlaying)
                {
                    GetComponent<AudioSource>().PlayOneShot(laser);
                }               
                Ray ray = new Ray(transform.position, transform.forward);
                line.SetPosition(0, ray.origin);

                if (Physics.Raycast(ray, out hit, range))
                {
                    line.SetPosition(1, hit.point);
                    if (hit.collider.gameObject.name == "Player")
                    {
                        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
                    }
                }
                else
                {
                    line.SetPosition(1, ray.GetPoint(range));
                }
                if (timeIsLock > 0.8f)
                {
                    line.enabled = false;
                    if (timeIsLock > 1.2f)
                    {
                        timeLooking = 0.0f;
                        timeIsLock = 0.0f;
                        isLock = false;
                    }
                }
            }
        }
    }
}
