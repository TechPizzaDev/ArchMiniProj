using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneRoot : MonoBehaviour
{
    public List<SceneRoot> roots;

    public int gameSceneIndex = 1;
    public int stationSceneIndex = 2;
    
    public SceneRoot gameRoot { get; private set; }
    public SceneRoot stationRoot { get; private set; }

    public Scene scene;
    public int sceneIndex;

    public List<CameraDescription> cameraDescs;

    public List<GameObject> disable = new();

    public List<GameObject> objects = new();

    public List<GameObject> inverseObjects = new();

    public Canvas worldCanvas;
    public GameObject player;

    public SceneRoot()
    {
        SceneManager.sceneLoaded += SceneManager_sceneLoaded;
    }

    void Awake()
    {
        roots = GameObject.FindGameObjectsWithTag("SceneRoot")
            .Select(obj => obj.GetComponent<SceneRoot>())
            .ToList();

        gameRoot = roots.FirstOrDefault(r => r.gameObject.scene.buildIndex == gameSceneIndex);
        stationRoot = roots.FirstOrDefault(r => r.gameObject.scene.buildIndex == stationSceneIndex);

        scene = gameObject.scene;
        sceneIndex = scene.buildIndex;

        var rootObjects = scene.GetRootGameObjects();

        cameraDescs = rootObjects
            .SelectMany(root => root.GetComponentsInChildren<CameraDescription>())
            .ToList();

        cameraDescs.ForEach(cam => cam.sceneIndex = sceneIndex);

        foreach (GameObject obj in disable)
            obj.SetActive(false);

        worldCanvas = GameObject.FindWithTag("WorldCanvas").GetComponent<Canvas>();
        player = GameObject.FindWithTag("Player");
    }

    private void SceneManager_sceneLoaded(Scene arg0, LoadSceneMode mode)
    {
        if (!this.IsDestroyed() && mode == LoadSceneMode.Additive)
        {
            Awake();
        }
    }

    public void SetObjectsActive(bool active)
    {
        objects.ForEach(obj => obj.SetActive(active));

        inverseObjects.ForEach(obj => obj.SetActive(!active));
    }
}
