using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseButton : MonoBehaviour
{
    [SerializeField] private GameObject target;

    public void Click()
    {
        target.SetActive(!target.activeSelf);
    }
}
