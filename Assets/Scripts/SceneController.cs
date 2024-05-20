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
    }

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

        currentCam = cameraDesc;
    }

    public void ExitStations()
    {
        MoveToStation(sceneRoot.cameraDescs[0]);
    }
}
