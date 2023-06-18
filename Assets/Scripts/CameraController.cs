using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    private const float MIN_FOLLOW_Y_OFFSET = 2f;
    private const float MAX_FOLLOW_Y_OFFSET = 12f;
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float rotateSpeed = 5f;
    [SerializeField] private CinemachineVirtualCamera cinemachineVirtualCamera;
    CinemachineTransposer cinemachineTransposer;


    // Start is called before the first frame update
    void Start()
    {
        cinemachineTransposer = cinemachineVirtualCamera.GetCinemachineComponent<CinemachineTransposer>();
        targetFollowOffset = cinemachineTransposer.m_FollowOffset;
    }

    private Vector3 targetFollowOffset;

    // Update is called once per frame
    void Update()
    {
        HandleMovement();
        HandleRotatation();
        HandleZoom();
    }

    private void HandleZoom()
    {
        float zoomAmount = 1f;

        if (Input.mouseScrollDelta.y > 0)
        {
            targetFollowOffset.y -= zoomAmount;
        }

        if (Input.mouseScrollDelta.y < 0)
        {
            targetFollowOffset.y += zoomAmount;
        }

        targetFollowOffset.y = Mathf.Clamp(targetFollowOffset.y, MIN_FOLLOW_Y_OFFSET, MAX_FOLLOW_Y_OFFSET);

        float zoomSpeed = 5f;
        cinemachineTransposer.m_FollowOffset = Vector3.Lerp(cinemachineTransposer.m_FollowOffset, targetFollowOffset, Time.deltaTime * zoomSpeed);
    }

    private void HandleRotatation()
    {
        Vector3 inptRotateDir = new Vector3(0, 0, 0);
        if (Input.GetKey(KeyCode.E)) inptRotateDir.y += -1;
        if (Input.GetKey(KeyCode.Q)) inptRotateDir.y += 1;
        transform.eulerAngles += inptRotateDir * rotateSpeed * Time.deltaTime;
    }

    private void HandleMovement()
    {
        Vector3 inputMoveDir = new Vector3(0, 0, 0);


        if (Input.GetKey(KeyCode.W)) inputMoveDir.z += 1f;

        if (Input.GetKey(KeyCode.A)) inputMoveDir.x += -1f;

        if (Input.GetKey(KeyCode.S)) inputMoveDir.z += -1f;

        if (Input.GetKey(KeyCode.D)) inputMoveDir.x += 1f;

        transform.position += transform.TransformDirection(inputMoveDir) * Time.deltaTime * moveSpeed;
    }
}
