using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.XR.ARFoundation;

public class ARPlaneController : MonoBehaviour
{

    ARPlaneManager m_ARPlaneManager;
    ARmenager ARmanager;

    public GameObject placeButton;
    public GameObject adjustButton;
    public GameObject searchForGameButton;
    public GameObject slider;

    public TextMeshProUGUI infoUIPanelText;



    private void Awake()
    {
        m_ARPlaneManager = GetComponent<ARPlaneManager>();
        ARmanager = GetComponent<ARmenager>(); 

    }

    void Start()
    {
        placeButton.SetActive(true);
        slider.SetActive(true);

        adjustButton.SetActive(false);
        searchForGameButton.SetActive(false);
        infoUIPanelText.text = "Move phone to detect planes and place the Battle Arena";
    }

    void Update()
    {
        
    }

    public void DisableARControllerDetection()
    {
        m_ARPlaneManager.enabled = false;
        ARmanager.enabled = false;
        SetAllPlanesActiveOrDeactive(false);

        slider.SetActive(false);
        placeButton.SetActive(false);
        adjustButton.SetActive(true);
        searchForGameButton.SetActive(true);
        infoUIPanelText.text = "Good now you can start Battle";


    }


    //Plane and Adjust button 
    public void EnableARControllerDetection()
    {
        m_ARPlaneManager.enabled = true;
        ARmanager.enabled = true;
        SetAllPlanesActiveOrDeactive(true);

        slider.SetActive(true);
        placeButton.SetActive(true);
        adjustButton.SetActive(false);
        searchForGameButton.SetActive(false);
        infoUIPanelText.text = "Missing place... Try again move phone";
    }

    private void SetAllPlanesActiveOrDeactive(bool value)
    {
        foreach(var plane in m_ARPlaneManager.trackables)
        {
            plane.gameObject.SetActive(value);
        }
    }


}
