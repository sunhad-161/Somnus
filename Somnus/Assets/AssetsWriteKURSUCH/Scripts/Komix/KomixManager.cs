using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class KomixManager : MonoBehaviour
{
    public AudioSource pop;
    public AudioSource err;

    public void PlayPop()
    {
        pop.Play();
    }

    public void PlayErr()
    {
        err.Play();
    }

    public void NextScene()
    {
        SceneManager.LoadScene("Office1");
    }
}
