using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

public class ARmenager : MonoBehaviour
{

    ARRaycastManager m_ARRaycastMenager;
    static List<ARRaycastHit> raycastHits = new List<ARRaycastHit>();

    public Camera arCamera;
    public GameObject battleArena;

    private void Awake()
    {
        m_ARRaycastMenager = GetComponent<ARRaycastManager>();
    }
    void Start()
    {
        
    }

    void Update()
    {
        //In Uity we add to AR origin the AR raycast menager
        Vector3 centerOfScreen = new Vector3(Screen.width / 2, Screen.height / 2);
        //Middle vector
        Ray ray = arCamera.ScreenPointToRay(centerOfScreen);


        if (m_ARRaycastMenager.Raycast(ray, raycastHits, TrackableType.PlaneWithinPolygon))
        {
            //Intersection
            Pose hitPose = raycastHits[0].pose;

            Vector3 positionToBePlaced = hitPose.position;

            battleArena.transform.position = positionToBePlaced;


        }






    }
}
