using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneRoot : MonoBehaviour
{
    public List<SceneRoot> roots;

    public SceneRoot gameRoot { get; private set; }

    public Scene scene;
    public int sceneIndex;

    public List<CameraDescription> cameraDescs;

    public List<GameObject> disable = new();

    public List<GameObject> objects = new();

    public List<GameObject> inverseObjects = new();

    void Awake()
    {
        roots = GameObject.FindGameObjectsWithTag("SceneRoot")
            .Select(obj => obj.GetComponent<SceneRoot>())
            .ToList();

        gameRoot = roots.OrderBy(r => r.gameObject.scene.buildIndex).First();

        scene = gameObject.scene;
        sceneIndex = scene.buildIndex;

        var rootObjects = scene.GetRootGameObjects();

        cameraDescs = rootObjects
            .SelectMany(root => root.GetComponentsInChildren<CameraDescription>())
            .ToList();

        cameraDescs.ForEach(cam => cam.sceneIndex = sceneIndex);

        foreach (GameObject obj in disable)
            obj.SetActive(false);
    }

    public void SetObjectsActive(bool active)
    {
        objects.ForEach(obj => obj.SetActive(active));

        inverseObjects.ForEach(obj => obj.SetActive(!active));
    }
}
