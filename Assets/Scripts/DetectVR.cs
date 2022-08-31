using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Management;

public class DetectVR : MonoBehaviour
{
    public GameObject xrOrigin;
    public GameObject desktopCharacter;

    public bool startInVr = true;

    // Start is called before the first frame update
    void Start()
    {
        if(startInVr)
        {


            var xrSettings = XRGeneralSettings.Instance;

            if (xrSettings == null)
            {
                Debug.Log("XRGeneralSettings is null");
                return;
            }


            var xrManager = xrSettings.Manager;

            if (xrManager == null)
            {
                Debug.Log("XRManagerSettings is null");
                return;
            }


            var xrLoader = xrManager.activeLoader;

            if (xrLoader == null)
            {
                Debug.Log("XRLoader is null");
                xrOrigin.SetActive(false);
                desktopCharacter.SetActive(true);
                return;
            }


            // have a VR setup active
            xrOrigin.SetActive(true);
            desktopCharacter.SetActive(false);
        }
        else
        {
            xrOrigin.SetActive(false);
            desktopCharacter.SetActive(true);
        }

    }
}