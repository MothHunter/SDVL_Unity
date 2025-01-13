using System.Collections;
using System.Collections.Generic;
using System.Linq;
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

        // Move player and camera game objects to the new scene
        Scene newScene = SceneManager.GetSceneByName(sceneName);

        // prevent duplication of player and camera objects
        RemoveObjectsFromScene(newScene, new List<string> { "Player", "MainCamera", "CmCamera" });

        SceneManager.MoveGameObjectToScene(player, newScene);
        SceneManager.MoveGameObjectToScene(mainCamera, newScene);
        SceneManager.MoveGameObjectToScene(cmCamera, newScene);

        // Set MapBounds on Cinemachine Virtual Camera
        PolygonCollider2D mapBounds = newScene.GetRootGameObjects().FirstOrDefault(go => go.name == "MapBounds").GetComponent<PolygonCollider2D>();
        cmCamera.GetComponent<Cinemachine.CinemachineVirtualCamera>().GetComponent<Cinemachine.CinemachineConfiner>().m_BoundingShape2D = mapBounds;

        // Set the new scene as the active scene
        SceneManager.SetActiveScene(newScene);

        // Get the entry point from the new scene
        GameObject sceneLogic = newScene.GetRootGameObjects().FirstOrDefault(go => go.name == "SceneLogic");
        if (sceneLogic != null)
        {
            EntryPoints entryPoints = sceneLogic.GetComponent<EntryPoints>();
            Vector2 spawnPoint = entryPoints.GetEntryPointFrom(currentScene.name);

            Debug.Log("Moving player to: " + spawnPoint);

            // Move the player to the entry point
            player.transform.position = spawnPoint;
        }
        else
        {
            player.transform.position = Vector2.zero;
            Debug.LogError("SceneLogic object not found in the new scene");
        }

        SceneManager.UnloadSceneAsync(currentScene);

        Debug.Log("Scene transition complete");
    }

    private void RemoveObjectsFromScene(Scene scene, List<string> objectNames)
    {
        foreach (GameObject go in scene.GetRootGameObjects())
        {
            if (objectNames.Contains(go.name))
            {
                Destroy(go);
            }
        }
    }
}
