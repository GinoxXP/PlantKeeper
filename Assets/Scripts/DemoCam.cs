using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DemoCam : MonoBehaviour
{
    private Transform camera;
    private Vector3 startPos;

    private float time;
    void Start()
    {
        camera = gameObject.transform;
        startPos = camera.position;
    }

    void Update()
    {
        camera.position = startPos + new Vector3(Mathf.Sin(time/5) * 8, Mathf.Cos(time/5) * 4);

        time += Time.deltaTime;
    }
}
