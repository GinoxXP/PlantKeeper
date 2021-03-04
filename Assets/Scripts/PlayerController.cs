using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Inventory))]
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

    private Inventory inventory;

    void Start()
    {
        inventory = GetComponent<Inventory>();
    }

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
        GameObject detectedObject = null;

        if(moveDirection.x > 0)
            detectedObject = rightZone.DetectedObject;
        if(moveDirection.x < 0)
            detectedObject = leftZone.DetectedObject;

        if(moveDirection.y > 0)
            detectedObject = upZone.DetectedObject;
        if(moveDirection.y < 0)
            detectedObject = downZone.DetectedObject;


        if(detectedObject == null)
            return true;
        else
        {
            if(detectedObject.TryGetComponent(out Box box))
            {
                box.SetMoveDirection(moveDirection);
                //TODO hit animation
            }

            if(detectedObject.TryGetComponent(out Key key))
            {
                return true;
            }

            if(detectedObject.TryGetComponent(out Door door))
            {
                door.TryOpen(inventory.isHaveKey);
                return door.IsOpen;
            }
        }

        return false;
    }
}
