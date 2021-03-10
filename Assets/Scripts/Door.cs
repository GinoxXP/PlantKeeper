using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer), typeof(LoadScene))]
public class Door : MonoBehaviour
{
    private bool isOpen;
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

        isOpen = !KeyOnMap();
        UpdateState();
    }

    bool KeyOnMap()
    {
        var potentialKeys = GameObject.FindGameObjectsWithTag("Key");
        foreach(var potentialKey in potentialKeys)
            if(potentialKey.TryGetComponent(out Key key))
                return true;

        return false;
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
