using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{

    public GameObject credit;

    public GameObject scoreshow;

    [SerializeField] private GameObject loadingCanvas;
    [SerializeField] private GameObject menuCanvas;

    void Start()
    {
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.Confined;
    }

    public void ContinueButton(string sceneToLoad)
    {
            loadScene(sceneToLoad);
    }

    public void Credit()
    {
        credit.SetActive(true);
    }

    public void Scoreshow()
    {
       scoreshow.SetActive(true);
    }

    public void CloseScoreshow()
    {
       scoreshow.SetActive(false);
    }

    public void Exit()
    {
        Application.Quit();
    }

    public void loadScene(string sceneToLoad)
    {
        menuCanvas.SetActive(false);
        loadingCanvas.SetActive(true);
        StartCoroutine(LoadAsyncScene(sceneToLoad));
    }

    IEnumerator LoadAsyncScene(string sceneToLoad)
    {
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(sceneToLoad);

        while (!asyncLoad.isDone)
        {
            float progress = Mathf.Clamp01(asyncLoad.progress / 0.9f);

            yield return null;
        }
    }
}