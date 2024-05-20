using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SceneController : MonoBehaviour
{
    public int stationSceneIndex;

    public SceneRoot sceneRoot;

    private CameraDescription playerCam;
    private List<CameraDescription> stationCams;

    private CameraDescription currentCam;

    void Start()
    {
        playerCam = sceneRoot.cameraDescs.First();
        stationCams = sceneRoot.roots.First(r => r.scene.buildIndex == stationSceneIndex).cameraDescs;

        currentCam = playerCam;

        //var activeListObjects = sceneRoot.ac GameObject.FindGameObjectsWithTag("ActiveList");
        //
        //playerCameraDesc.activeList = activeListObjects
        //    .First(obj => obj.scene.buildIndex == playerCameraDesc.sceneBuildIndex)
        //    .GetComponent<ActiveList>();
        //
        //playerCameraDesc.activeList.Apply(true);
        //
        //cameraDescs.ForEach(desc =>
        //{
        //    desc.activeList = activeListObjects
        //        .First(obj => obj.scene.buildIndex == desc.sceneBuildIndex)
        //        .GetComponent<ActiveList>();
        //
        //    desc.activeList.Apply(false);
        //});
    }

    //void Awake()
    //{
    //    SceneManager.sceneLoaded += SceneManager_sceneLoaded;
    //    SceneManager.sceneUnloaded += SceneManager_sceneUnloaded;
    //}
    //
    //private void SceneManager_sceneLoaded(Scene arg0, LoadSceneMode arg1)
    //{
    //
    //}
    //
    //private void SceneManager_sceneUnloaded(Scene arg0)
    //{
    //    GameDisableList.ForEach(obj => obj.SetActive(true));
    //}

    // Start is called before the first frame update
    //void Start()
    //{
    //    if (StationBuildIndex == 0)
    //    {
    //        throw new Exception("Station index may not be 0.");
    //    }
    //}

    // Update is called once per frame
    void Update()
    {
        var camera = Camera.main;
        camera.orthographicSize = currentCam.OrthoSize;

        var cameraTransform = camera.transform;
        var cameraPos = cameraTransform.position;
        var targetPos = currentCam.transform.position;
        cameraTransform.position = new Vector3(targetPos.x, targetPos.y, cameraPos.z);
    }

    public void MoveToStation(int stationIndex)
    {
        if (stationIndex < 0)
        {
            MoveToStation(playerCam);
        }
        else
        {
            MoveToStation(stationCams.First(cam => cam.StationIndex == stationIndex));
        }
    }

    private SceneRoot GetRoot(int sceneIndex)
    {
        return sceneRoot.roots.First(r => r.sceneIndex == sceneIndex);
    }

    public void MoveToStation(CameraDescription cameraDesc)
    {
        if (currentCam == cameraDesc)
        {
            return;
        }

        if (currentCam.sceneIndex != cameraDesc.sceneIndex)
        {
            SceneRoot currRoot = GetRoot(currentCam.sceneIndex);
            currRoot.SetObjectsActive(false);

            SceneRoot nextRoot = GetRoot(cameraDesc.sceneIndex);
            nextRoot.SetObjectsActive(true);
        }

        //if (stationIndex == -1)
        //{
        //    SceneManager.UnloadSceneAsync(StationBuildIndex);
        //}
        //else
        //{
        //    LoadSceneParameters param = new(LoadSceneMode.Additive, LocalPhysicsMode.Physics2D);
        //    SceneManager.LoadSceneAsync(StationBuildIndex, param);
        //}

        currentCam = cameraDesc;
    }

    public void ExitStations()
    {
        MoveToStation(sceneRoot.cameraDescs[0]);
    }
}
