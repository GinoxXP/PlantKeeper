using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Inventory), typeof(Animator), typeof(SpriteRenderer))]
public class PlayerController : MonoBehaviour
{
    [SerializeField] private float speed;

    private Vector3 target;

    private Vector3 moveDirection;

    [SerializeField] private DetectZone upZone;
    [SerializeField] private DetectZone leftZone;
    [SerializeField] private DetectZone downZone;
    [SerializeField] private DetectZone rightZone;

    private bool isRunning;

    private Inventory inventory;
    private StrokeManager strokeManager;
    private Animator animator;
    private SpriteRenderer spriteRenderer;

    [HideInInspector]
    public bool isCanWalk;

    void Start()
    {
        inventory = GetComponent<Inventory>();
        strokeManager = GameObject.Find("Stroke Manager")
                                  .GetComponent<StrokeManager>();
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();

        StartCoroutine(PauseBeforePlay());
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        if(!isRunning && context.performed && isCanWalk)
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

    IEnumerator PauseBeforePlay()
    {
        yield return new WaitForSeconds(0.2f);
        isCanWalk = true;
        yield return null;
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
        {
            detectedObject = rightZone.DetectedObject;
            spriteRenderer.flipX = false;
        }
        if(moveDirection.x < 0)
        {
            detectedObject = leftZone.DetectedObject;
            spriteRenderer.flipX = true;
        }

        if(moveDirection.y > 0)
            detectedObject = upZone.DetectedObject;
        if(moveDirection.y < 0)
            detectedObject = downZone.DetectedObject;


        if(detectedObject == null)
        {
            strokeManager.Stroke();
            return true;
        }
        else
        {
            if(detectedObject.TryGetComponent(out Box box))
            {
                box.SetMoveDirection(moveDirection);
                strokeManager.Stroke();
                animator.Play("Hit");
            }

            if(detectedObject.TryGetComponent(out Key key))
            {
                strokeManager.Stroke();
                return true;
            }

            if(detectedObject.TryGetComponent(out Door door))
            {
                bool isOpen = door.IsOpen;
                door.TryOpen(inventory.isHaveKey);
                strokeManager.Stroke();
                return isOpen;
            }
        }

        return false;
    }
}
