using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Networking;

public class Unit : MonoBehaviour
{
    [SerializeField] Animator unitAnimator;
    private Vector3 targetPosition;

    // Start is called before the first frame update
    private void Awake()
    {
        targetPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        
        float stoppingDistance = .1f;
        if(Vector3.Distance(targetPosition, transform.position) > stoppingDistance )
        {
            Vector3 moveDirection = (targetPosition - transform.position).normalized;
            float moveSpeed = 4f;

            transform.position += moveDirection * moveSpeed * Time.deltaTime;

            unitAnimator.SetBool("isWalking", true);

            float rotateSpeed = 10f;
            transform.forward = Vector3.Lerp(transform.forward, moveDirection, Time.deltaTime * rotateSpeed);

        }
        else
        {
            unitAnimator.SetBool("isWalking", false);
        }
    }

    public void Move(Vector3 targetPosition)
    {
        this.targetPosition = targetPosition;
    }

}
