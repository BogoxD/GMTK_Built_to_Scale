using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class HeadMovement : MonoBehaviour
{
    void Start()
    {
        
    }
    void Update()
    {
        transform.LookAt(Camera.main.ScreenToWorldPoint(Input.mousePosition));
    }
}
