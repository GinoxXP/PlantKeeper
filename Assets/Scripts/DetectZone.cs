using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public class DetectZone : MonoBehaviour
{
    public GameObject detectedObject;
    public GameObject DetectedObject
    {
        get{return detectedObject;}
    }

    void OnTriggerStay2D(Collider2D collider)
    {
        if(collider.gameObject.TryGetComponent(out Rigidbody2D rb))
            if(detectedObject != rb.gameObject)
                detectedObject = rb.gameObject;
    }

    void OnTriggerExit2D(Collider2D collider)
    {
        if(collider.gameObject.TryGetComponent(out Rigidbody2D rb))
            detectedObject = null;
    }
}
