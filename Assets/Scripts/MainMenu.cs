using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public AudioSource sfxButton;
    public float delay = 0.3f;

    public void PlayGame()
    {
        StartCoroutine(PlayRoutine());
    }

    public void QuitGame()
    {
        StartCoroutine(QuitRoutine());
    }

    IEnumerator PlayRoutine()
    {
        if (sfxButton != null) sfxButton.Play();
        yield return new WaitForSeconds(delay);
        SceneManager.LoadSceneAsync(1);
    }

    IEnumerator QuitRoutine()
    {
        if (sfxButton != null) sfxButton.Play();
        yield return new WaitForSeconds(delay);
        Application.Quit();
    }
}