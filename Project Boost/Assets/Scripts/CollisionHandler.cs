using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CollisionHandler : MonoBehaviour
{
    [SerializeField] float levelDelay = 1f;
    [SerializeField] AudioClip crashSFX;
    [SerializeField] AudioClip completeLevelSFX;

    [SerializeField] ParticleSystem successParticles;
    [SerializeField] ParticleSystem crashparticles;

    AudioSource audioSource;

    bool isTransitioning = false;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        CheatCodes();
    }

    void OnCollisionEnter(Collision other)
    {
        if (isTransitioning) { return; }

        switch (other.gameObject.tag)
        {
            case "Friendly":
                Debug.Log("You hit a friendly, noice!");
                break;
            case "Finish":
                StartSuccessSequence();
                break;
            default:
                StartCrashSequence();
                break;
        }
    }

    void StartCrashSequence()
    {
        isTransitioning = true;
        audioSource.Stop();
        crashparticles.Play();
        GetComponent<Movement>().enabled = false;
        audioSource.PlayOneShot(crashSFX, 0.5f);
        Invoke("ReloadLevel", levelDelay);
    }

    void StartSuccessSequence()
    {
        isTransitioning = true;
        audioSource.Stop();
        successParticles.Play();
        GetComponent<Movement>().enabled = false;
        audioSource.PlayOneShot(completeLevelSFX, 0.5f);
        Invoke("LoadNextLevel", levelDelay);
    }
    
    void ReloadLevel()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex);
    }

    public void LoadNextLevel()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        int nextSceneIndex = currentSceneIndex + 1;
        if (nextSceneIndex == SceneManager.sceneCountInBuildSettings)
        {
            nextSceneIndex = 0;
        }
        SceneManager.LoadScene(nextSceneIndex);
    }

    void CheatCodes()
    {
        CapsuleCollider debugged = GetComponentInChildren<CapsuleCollider>();
        if (Input.GetKeyDown(KeyCode.L))
        {
            LoadNextLevel();
        }

        if (Input.GetKeyDown(KeyCode.C) && debugged.enabled)
        {
            debugged.enabled = false;
        }
        else if (Input.GetKeyDown(KeyCode.C) && !debugged.enabled)
        {
            debugged.enabled = true;
        }
    }
}
