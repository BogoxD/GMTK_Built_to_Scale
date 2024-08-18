using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAimController : MonoBehaviour
{

    public GameObject mainCamera;
    public GameObject aimCamera;
    public GameObject aimReticle;

    void Update()
    {
        Aim();
    }
    void Aim()
    {
        if(Input.GetKey(KeyCode.Mouse1) && !aimCamera.activeInHierarchy)
        {
            mainCamera.SetActive(false);
            aimCamera.SetActive(true);

            //show reticle
            aimReticle.SetActive(true);
        }
        else if(Input.GetKeyUp(KeyCode.Mouse1) && !mainCamera.activeInHierarchy)
        {
            mainCamera.SetActive(true);
            aimCamera.SetActive(false);

            //disable reticle
            aimReticle.SetActive(false);
        }
    }
}
