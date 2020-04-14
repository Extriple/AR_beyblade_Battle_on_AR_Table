using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.UI;
public class ScaleController : MonoBehaviour
{
    ARSessionOrigin m_ARSessionOrigin;

    public Slider slider;

    private void Awake()
    {
        m_ARSessionOrigin = GetComponent<ARSessionOrigin>(); //After this make in Unity --> Canvas --> Slider
    }

    void Start()
    {
        slider.onValueChanged.AddListener(OnSliderChangeValue);
        
    }

    public void OnSliderChangeValue(float value)
    {
        if (slider != null)
        {
            m_ARSessionOrigin.transform.localScale = Vector3.one / value;

        }
    }


    void Update()
    {
        
    }
}
