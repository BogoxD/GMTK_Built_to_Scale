using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PlayerAimController))]
public class GrapplingSystem : MonoBehaviour
{
    private PlayerAimController _playerAimController;
    private Transform _currentCamera;
    private SpringJoint _joint;
    private Vector3 _grapplePoint;
    private float _maxDistance = 100f;

    [SerializeField] LineRenderer _lr;
    [SerializeField] Transform firePoint;
    [SerializeField] LayerMask whatIsGrappable;
    
    void Start()
    {
        _playerAimController = GetComponent<PlayerAimController>();
    }
    private void LateUpdate()
    {
        DrawRope();
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0) && _playerAimController.isAiming)
            StartGraple();
        else if (Input.GetKeyUp(KeyCode.Mouse0))
            StopGraple();
    }
    /// <summary>
    /// Call whenever we want to start graple
    /// </summary>
    private void StartGraple()
    {
        RaycastHit hit;
        _currentCamera = _playerAimController.GetAimCamTransform();

        if(Physics.Raycast(_currentCamera.position, _currentCamera.forward, out hit, _maxDistance, whatIsGrappable))
        {
            _grapplePoint = hit.point;
            _joint = firePoint.gameObject.AddComponent<SpringJoint>();
            _lr.positionCount = 2;

            //Transform obj = hit.collider.transform;
            //obj.position = Vector3.MoveTowards(obj.position, transform.position, 5f * Time.deltaTime);

            Debug.Log("Grapple");
        }

    }
    /// <summary>
    /// Call whenever we want to stop graple
    /// </summary>
    private void StopGraple()
    {
        _lr.positionCount = 0;
        Destroy(_joint);
    }
    private void DrawRope()
    {
        if (!_joint) return;
        _lr.SetPosition(0, firePoint.position);
        _lr.SetPosition(1, _grapplePoint);
    }
}
