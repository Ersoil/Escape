using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class SceneLoaderManager : MonoBehaviour
{
    public GameObject loadingScreen;
    public Slider progressBar;
    public TextMeshProUGUI text;

    public void ReloadCurrentScene()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        if (progressBar == null && text == null) SceneManager.LoadScene(currentSceneIndex);
        else StartCoroutine(LoadAsync(currentSceneIndex));
    }

    public void NextScene()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        if (progressBar == null && text == null) SceneManager.LoadScene(currentSceneIndex+1);
        else StartCoroutine(LoadAsync(currentSceneIndex+1));
    }

    public void GameOut()
    {
        Application.Quit();
        #if UNITY_EDITOR
                UnityEditor.EditorApplication.isPlaying = false;
        #endif
    }

    private IEnumerator LoadAsync(int currentSceneIndex)
    {
        var AsyncLoad = SceneManager.LoadSceneAsync(currentSceneIndex);
        loadingScreen.SetActive(true);
        while (!AsyncLoad.isDone)
        {
            progressBar.value = AsyncLoad.progress;
            yield return null;
        }
    }
}
