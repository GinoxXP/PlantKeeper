using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [SerializeField] float speed;
    private Vector3 target;

    private Vector3 moveDirection;

    [SerializeField] DetectZone upZone;
    [SerializeField] DetectZone leftZone;
    [SerializeField] DetectZone downZone;
    [SerializeField] DetectZone rightZone;

    private bool isRunning;


    public void OnMove(InputAction.CallbackContext context)
    {
        if(!isRunning && context.performed)
        {
            moveDirection = context.ReadValue<Vector2>();
            if(CheckFreeWay())
            {
                isRunning = true;
                SetTarget();
                StartCoroutine(Move());
            }
        }
    }

    void SetTarget()
    {
        target = transform.position + moveDirection;
    }

    IEnumerator Move()
    {
        while (Vector3.Distance(transform.position, target) > float.Epsilon)
        {
            transform.position = Vector3.MoveTowards(transform.position,
                                                    target,
                                                    speed * Time.deltaTime);
            yield return null;
        }
        isRunning = false;
    }

    bool CheckFreeWay()
    {
        if(moveDirection.x > 0)
            if(rightZone.DetectedObject == null)
                return true;
            else
                return false;

        if(moveDirection.x < 0)
            if(leftZone.DetectedObject == null)
                return true;
            else
                return false;

        if(moveDirection.y > 0)
            if(upZone.DetectedObject == null)
                return true;
            else
                return false;

        if(moveDirection.y < 0)
            if(downZone.DetectedObject == null)
                return true;
            else
                return false;

        return false;
    }

}
