using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PlayerAimController))]
public class GrapplingSystem : MonoBehaviour
{
    private PlayerAimController _playerAimController;
    private Transform _currentCamera;
    private GameObject _grabbedObject;
    private SpringJoint _joint;
    private Vector3 _grapplePoint;


    [SerializeField] LineRenderer _lr;
    [SerializeField] Transform firePoint;
    [SerializeField] LayerMask whatIsGrappable;

    void Start()
    {
        _playerAimController = GetComponent<PlayerAimController>();
        _grabbedObject = new GameObject();
    }
    private void LateUpdate()
    {
        DrawRope();
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
            StartGraple();
        else if (Input.GetKeyUp(KeyCode.Mouse0))
            StopGraple();
    }
    /// <summary>
    /// Call whenever we want to start graple
    /// </summary>
    private void StartGraple()
    {
        _currentCamera = _playerAimController.GetAimCamTransform();

        //CAST RAY FROM CENTRE OF SCREEN TO FIT CROSSHAIR
        Vector3 rayOrigin = new Vector3(0.5f, 0.5f, 0.5f);
        float rayLenght = 500f;

        Ray ray = Camera.main.ViewportPointToRay(rayOrigin);

        Debug.DrawRay(ray.origin, ray.direction * rayLenght, Color.red);

        if (Physics.Raycast(ray, out RaycastHit hit, rayLenght, whatIsGrappable))
        {
            //ray intersected collider
            Debug.Log(hit.collider);

            firePoint.LookAt(hit.point);
            _grabbedObject = hit.collider.gameObject;
            _grapplePoint = hit.point;
            
            _joint = firePoint.gameObject.AddComponent<SpringJoint>();
            _joint.connectedBody = _grabbedObject.GetComponent<Rigidbody>();
            _joint.connectedAnchor = _grapplePoint;
            _joint.autoConfigureConnectedAnchor = false;

            _lr.positionCount = 2;
        }
    }
    /// <summary>
    /// Call whenever we want to stop graple
    /// </summary>
    private void StopGraple()
    {
        _grabbedObject = null;
        _lr.positionCount = 0;
        Destroy(_joint);
    }
    private void DrawRope()
    {
        if (!_joint) return;
        _lr.SetPosition(0, firePoint.position);
        _lr.SetPosition(1, _grapplePoint);

        if(_grabbedObject)
        _grapplePoint = _grabbedObject.transform.position;
    }
}
