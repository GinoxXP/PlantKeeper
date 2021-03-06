using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(LoadScene))]
public class StrokeManager : MonoBehaviour
{
    [SerializeField] private int strokesLeft;

    private LoadScene loadScene;

    private Text strokeCounter;

    private List<Plant> updatablePlants = new List<Plant>();

    void Start()
    {
        loadScene = GetComponent<LoadScene>();
        strokeCounter = GameObject.Find("Stroke Counter")
                                  .GetComponent<Text>();

        strokeCounter.text = strokesLeft.ToString();
    }

    public void Stroke()
    {
        strokesLeft--;
        strokeCounter.text = strokesLeft.ToString();
        UpdatePlants();
        CheckGameOver();
    }

    void CheckGameOver()
    {
        if(strokesLeft < 0)
            loadScene.Reload();
    }

    void UpdatePlants()
    {
        foreach(Plant plant in updatablePlants)
            plant.Stroke();
    }

    public void AddPlantToList(Plant plant)
    {
        updatablePlants.Add(plant);
    }
}
