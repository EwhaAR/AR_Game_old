using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;
using UnityEngine.UI;

public class ARPlacement : MonoBehaviour
{

    public GameObject arObjectToSpawn;  // bowl to place on the plane
    public GameObject placementIndicator;
    private GameObject spawnedObject;
    private Pose PlacementPose;
    private ARRaycastManager aRRaycastManager;
    private bool placementPoseIsValid = false;

    // #ES: No more plane showing after the bowl is placed
    private ARPlaneManager arPlaneManager;

    // Player Guide
    MinigameManager minigameScript;   
    private bool isObjectPlaced = false; 

    void Awake()
    {
        aRRaycastManager = FindObjectOfType<ARRaycastManager>();
        arPlaneManager = FindObjectOfType<ARPlaneManager>();

        minigameScript = GameObject.Find("ESGameManager").GetComponent<MinigameManager>();
    }

    // need to update placement indicator, placement pose and spawn 
    void Update()
    {

        if(isObjectPlaced) return;

        UpdatePlacementPose();
        UpdatePlacementIndicator();
        
        // Create the bowl instance and place it on the plane only once
        if(spawnedObject == null && placementPoseIsValid && Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
        {
            ARPlaceObject();
            
            // Player Guide
            minigameScript.showGuidelines(2);  // 2: Touch the screen with two fingers to create new object to throw
            isObjectPlaced = true;
        }

    }

    void UpdatePlacementIndicator()
    {
        if(spawnedObject == null && placementPoseIsValid)
        {
            placementIndicator.SetActive(true);
            placementIndicator.transform.SetPositionAndRotation(PlacementPose.position, PlacementPose.rotation);

            // Player Guide
            minigameScript.showGuidelines(1);  // 1: Touch the screen to place the container object
        }
        else
        {
            placementIndicator.SetActive(false);
            
            // Stop showing perceived planes
            // foreach (var plane in arPlaneManager.trackables) {
            //     // 오브젝트가 생성되었기 때문에 Plane 인스턴스 생성을 멈추게 한다.
            //     plane.gameObject.SetActive(false);
            // }
        }
    }

    void UpdatePlacementPose()
    {
        // Player Guide
        minigameScript.showGuidelines(0);  // 0: detecting planes

        // Get the position of the screen center
        var screenCenter = Camera.main.ViewportToScreenPoint(new Vector3(0.5f, 0.5f));
        var hits = new List<ARRaycastHit>();
        aRRaycastManager.Raycast(screenCenter, hits, TrackableType.Planes);

        placementPoseIsValid = hits.Count > 0;
        if(placementPoseIsValid)
        {
            PlacementPose = hits[0].pose;  // Get the very first hit
        }
    }

    // Create the new instance of the bowl
    void ARPlaceObject()
    {
        spawnedObject = Instantiate(arObjectToSpawn, PlacementPose.position, PlacementPose.rotation);
    
        var rigidbody = spawnedObject.GetComponent<Rigidbody>();
        rigidbody.isKinematic = true;
    }
}


