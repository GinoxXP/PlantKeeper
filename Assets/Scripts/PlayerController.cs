using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [SerializeField] float speed;
    private Vector3 target;

    private Vector3 moveDirection;

    [SerializeField] BoxCollider2D upZone;
    [SerializeField] BoxCollider2D leftZone;
    [SerializeField] BoxCollider2D downZone;
    [SerializeField] BoxCollider2D rightZone;

    private bool isRunning;


    public void OnMove(InputAction.CallbackContext context)
    {
        if(!isRunning && context.performed)
        {
            isRunning = true;

            moveDirection = context.ReadValue<Vector2>();
            SetTarget();
            StartCoroutine(Move());
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

}
