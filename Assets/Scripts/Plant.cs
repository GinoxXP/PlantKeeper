using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer), typeof(BoxCollider2D))]
public class Plant : MonoBehaviour
{
    public enum PlantType
    {
        White, Orange, Violet, Blue, Red
    }

    [SerializeField] PlantType plantType;
    [SerializeField] Sprite[] growthStages;
    [SerializeField] Sprite[] bloomSprites;
    [SerializeField] int bloomStage;

    [SerializeField] private int currentStage;
    [SerializeField] private bool isBloom;
    [SerializeField] private bool isCanGrow;

    [System.Serializable]
    public struct Interval
    {
        public int start;
        public int end;
    }

    [SerializeField] private Interval blockInterval;

    private SpriteRenderer spriteRenderer;
    private BoxCollider2D boxCollider;

    void Start()
    {
        if(isCanGrow)
            GameObject.Find("Stroke Manager")
                      .GetComponent<StrokeManager>()
                      .AddPlantToList(this);

        spriteRenderer = GetComponent<SpriteRenderer>();
        boxCollider = GetComponent<BoxCollider2D>();

        UpdateStage();
    }

    void UpdateStage()
    {
        if(currentStage >= blockInterval.start &&
           currentStage <= blockInterval.end)
            boxCollider.enabled = true;
        else
            boxCollider.enabled = false;

        if(isBloom && currentStage != bloomStage)
            isBloom = false;

        if(currentStage == bloomStage)
            isBloom = true;

        if(isBloom)
            spriteRenderer.sprite = bloomSprites[(int) plantType];
        else
            spriteRenderer.sprite = growthStages[currentStage];
    }

    public void Stroke()
    {
        currentStage++;

        if(currentStage >= growthStages.Length)
            currentStage = 0;

        UpdateStage();
    }
}
