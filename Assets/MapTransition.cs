using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MapTransition : MonoBehaviour
{
    [SerializeField] private string sceneName;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Debug.Log("Player entered the transition zone");
            StartCoroutine(LoadScene());
        }
    }

    private IEnumerator LoadScene()
    {
        Scene currentScene = SceneManager.GetActiveScene();
        GameObject player = GameObject.Find("Player");
        GameObject mainCamera = GameObject.Find("MainCamera");
        GameObject cmCamera = GameObject.Find("CmCamera");
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Additive);

        Debug.Log("Loading scene: " + sceneName);

        // Wait until the last operation fully loads to return anything
        while (!asyncLoad.isDone)
        {
            yield return null;
        }

        Debug.Log("Scene loaded: " + sceneName);

        player.transform.position = new Vector3(0, 0, 0);
        SceneManager.MoveGameObjectToScene(player, SceneManager.GetSceneByName(sceneName));
        SceneManager.MoveGameObjectToScene(mainCamera, SceneManager.GetSceneByName(sceneName));
        SceneManager.MoveGameObjectToScene(cmCamera, SceneManager.GetSceneByName(sceneName));
        SceneManager.SetActiveScene(SceneManager.GetSceneByName(sceneName));
        SceneManager.UnloadSceneAsync(currentScene);

        Debug.Log("Scene transition complete");
    }
}
