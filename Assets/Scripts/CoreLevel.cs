using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoreLevel : MonoBehaviour
{
    public List<GameObject> lightSources;
    public List<GameObject> audioSources;
    public GameObject Music;
    public GameObject sceneManager;
    public AudioClip epicCutsceneMusic;
    public PlayerModel player;
    private float Timer = 0f;
    private bool isCutscene;
    private void Awake()
    {
        if (lightSources != null)
        {
            foreach(var lightSource in lightSources)
            {
                var lightRule = lightSource.GetComponent<LightRuler>();
                if (lightRule != null)
                {
                    lightRule.enabled = false;
                }
            }
        }

    }
    public void onLight()
    {
        if (lightSources != null)
        {
            foreach (var lightSource in lightSources)
            {
                var lightRule = lightSource.GetComponent<LightRuler>();
                if (lightRule != null)
                {
                    lightRule.enabled = true;
                }
            }
        }
    }
    public void offLight()
    {
        if (lightSources != null)
        {
            foreach (var lightSource in lightSources)
            {
                var lightRule = lightSource.GetComponent<LightRuler>();
                if (lightRule != null)
                {
                    lightRule.enabled = true;
                }
            }
        }
    }
    public void startEpicMusic()
    {
        var aud = Music.GetComponent<AudioSource>();
        aud.clip = epicCutsceneMusic;
        aud.Play();
    }
    public void StartEndCutscene()
    {
        isCutscene = true;
        Timer += Time.fixedDeltaTime;
        Debug.Log(Timer);
        Camera.main.fieldOfView = Mathf.Lerp(
        Camera.main.fieldOfView,40,Time.deltaTime * 0.5f);
        if (Timer > 1.5f && Timer < 1.8f) 
        {
            bool SetPlayerPos = false;
            if (SetPlayerPos == false)
            {
                player.GetComponent<PlayerController>().enabled = false;
                SetPlayerPos = true;
            }
            startEpicMusic();
        }
        else if (Timer > 9.5f && Timer < 10.5f)
        {
            onLight();
        }
        else if (Timer > 14.5f && Timer < 15.5f)
        {
            offLight();
        }
        else if (Timer > 15.5f && Timer < 16.5f)
        {
            onLight();
        }
        else if (Timer > 16.5f && Timer < 17.5f) 
        {
            offLight();
        }
        else if (Timer > 17.5f && Timer < 18.5f)
        {
            onLight();
        }
        else if (Timer > 18.5f && Timer < 19.5f)
        {
            offLight();
        }
        else if (Timer > 19.5f && Timer < 20.5f) 
        {
            onLight();
            sceneManager.GetComponent<SceneLoaderManager>().NextScene();
        }
    }

    private void FixedUpdate()
    {
        if (isCutscene)
        {
            StartEndCutscene();
        }
    }
}
