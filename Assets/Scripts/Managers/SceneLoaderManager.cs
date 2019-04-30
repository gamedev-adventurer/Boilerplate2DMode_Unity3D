using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoaderManager : MonoBehaviour
{
    public void GoToLevelByNumber(int indexLevel)
    {
        StartCoroutine(LoadNewSceneByIndex(indexLevel));

    }
    public void GoToLevelByName(string levelName)
    {
        StartCoroutine(LoadNewSceneByName(levelName));

    }

    public void LoadNextLevel()
    {

        Scene sceneNow = SceneManager.GetActiveScene();
        int index = sceneNow.buildIndex + 1;

        if (index > SceneManager.sceneCountInBuildSettings - 1)
        {

            index = 0;
        }

        StartCoroutine(LoadNewSceneByIndex(index));

    }

    public void ResetLevel()
    {
        Scene sceneNow = SceneManager.GetActiveScene();
        StartCoroutine(LoadNewSceneByIndex(sceneNow.buildIndex));
    }
   
    IEnumerator LoadNewSceneByIndex(int levelIndex)
    {

        AsyncOperation async = SceneManager.LoadSceneAsync(levelIndex);

        while (!async.isDone)
        {
            yield return null;
        }
    }
    IEnumerator LoadNewSceneByName(string levelName)
    {

        AsyncOperation async = SceneManager.LoadSceneAsync(levelName);

        while (!async.isDone)
        {
            yield return null;
        }
    }
}
