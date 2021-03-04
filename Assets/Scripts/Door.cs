using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer), typeof(LoadScene))]
public class Door : MonoBehaviour
{
    [SerializeField] private bool isOpen;
    public bool IsOpen
    {
        get{return isOpen;}
    }

    [SerializeField] Sprite closed;
    [SerializeField] Sprite opened;

    private SpriteRenderer sprite;

    private LoadScene loadScene;

    void Start()
    {
        sprite = GetComponent<SpriteRenderer>();
        loadScene = GetComponent<LoadScene>();
    }

    void UpdateState()
    {
        if(isOpen)
            sprite.sprite = opened;
        else
            sprite.sprite = closed;
    }

    public void TryOpen(bool isHaveKey)
    {
        if(isHaveKey)
        {
            isOpen = true;
            UpdateState();
        }
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if(col.tag == "Player")
        {
            loadScene.Load();
        }
    }
}
