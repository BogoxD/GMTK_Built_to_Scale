using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAimController : MonoBehaviour
{
    public GameObject mainCamera;
    public GameObject aimCamera;
    public GameObject aimReticle;

    [HideInInspector] public bool isAiming = false;

    void Update()
    {
        Aim();
    }
    void Aim()
    {
        if (Input.GetKey(KeyCode.Mouse1) && !aimCamera.activeInHierarchy)
        {
            isAiming = true;

            mainCamera.SetActive(false);
            aimCamera.SetActive(true);

            //show reticle
            aimReticle.SetActive(true);
        }
        else if (Input.GetKeyUp(KeyCode.Mouse1) && !mainCamera.activeInHierarchy)
        {
            isAiming = false;

            mainCamera.SetActive(true);
            aimCamera.SetActive(false);

            //disable reticle
            aimReticle.SetActive(false);
        }
    }
    public Transform GetAimCamTransform()
    {
        return aimCamera.transform;
    }
}
