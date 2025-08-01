using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class SceneLoaderManager : MonoBehaviour
{
    public GameObject loadingScreen;
    public Slider progressBar;
    public TextMeshProUGUI text;
    public Animator SliderAnim;
    public Animator ImageAnim;
    public int endGameIndex = 0;
    private int loadingScene =0;
    public static Action onAdShow;
    private void Awake()
    {
        GloabalData.onExitPressed += StartScreen;
        GloabalData.onEnergyEnd += EndGame;
        if (UnityEngine.Random.Range(0, 10) >= 6)
        {
            onAdShow?.Invoke();
        }
    }
    public void ReloadCurrentScene()
    {
        if (loadingScene != 0) return;
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        if (progressBar == null && text == null) SceneManager.LoadScene(currentSceneIndex);
        else StartCoroutine(LoadAsync(currentSceneIndex));
        loadingScene++;
    }

    public void NextScene()
    {
        if (loadingScene != 0) return;
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        if (progressBar == null && text == null) SceneManager.LoadScene(currentSceneIndex+1);
        else StartCoroutine(LoadAsync(currentSceneIndex+1));
        loadingScene++;
    }
    public void LoadScene(int index)
    {
        if (loadingScene != 0) return;
        if (progressBar == null && text == null) SceneManager.LoadScene(index);
        else StartCoroutine(LoadAsync(index));
        loadingScene++;
    }

    public void GameOut()
    {
        Application.Quit();
        #if UNITY_EDITOR
                UnityEditor.EditorApplication.isPlaying = false;
        #endif
    }
    public void StartScreen()
    {
        LoadScene(0);
    }
    public void EndGame()
     {
        LoadScene(endGameIndex);
     }
    private IEnumerator LoadAsync(int currentSceneIndex)
    {
        loadingScreen.SetActive(true);
        ImageAnim.SetTrigger("Start");
        SliderAnim.SetTrigger("Start");
        var AsyncLoad = SceneManager.LoadSceneAsync(currentSceneIndex);
        AsyncLoad.allowSceneActivation = false;
        while (!AsyncLoad.isDone)
        {
            progressBar.value = AsyncLoad.progress+0.1f;
            if(!AsyncLoad.allowSceneActivation && AsyncLoad.progress >= 0.9f)
            {
                yield return new WaitForSeconds(2.2f);
                AsyncLoad.allowSceneActivation = true;
            }
            
            yield return null;
        }
    }
}
