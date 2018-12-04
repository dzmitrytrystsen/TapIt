using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FadePanel : MonoBehaviour
{
    public static FadePanel instance;

    [SerializeField] private GameObject fadeCanvas;

    [SerializeField] private Animator myAnimator;

    public void Awake()
    {
        SetUpSingleton();
    }

    public void SetUpSingleton()
    {
        if (instance != null)
        {
            Destroy(gameObject);
        }

        else
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    public void FadeIn(int levelId)
    {
        StartCoroutine(FadeInIEnumerator(levelId));
    }

    public void FadeOut()
    {
        StartCoroutine(FadeOutIEnumerator());
    }

    IEnumerator FadeInIEnumerator(int levelId)
    {
        fadeCanvas.SetActive(true);
        myAnimator.Play("FadeIn");
        yield return StartCoroutine(MyCoroutine.WaitForRealSeconds(0.7f));
        SceneManager.LoadScene(levelId);
        FadeOut();
    }

    IEnumerator FadeOutIEnumerator()
    {
        myAnimator.Play("FadeOut");
        yield return StartCoroutine(MyCoroutine.WaitForRealSeconds(1f));
        fadeCanvas.SetActive(false);
    }
}
