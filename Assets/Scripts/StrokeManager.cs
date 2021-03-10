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

    private Animator playerAnimator;
    private PlayerController playerController;

    void Start()
    {
        loadScene = GetComponent<LoadScene>();
        strokeCounter = GameObject.Find("Stroke Counter")
                                  .GetComponent<Text>();

        strokeCounter.text = strokesLeft.ToString();

        GameObject player = GameObject.Find("Player");
        playerAnimator = player.GetComponent<Animator>();
        playerController = player.GetComponent<PlayerController>();
    }

    public void Stroke()
    {
        strokesLeft--;
        strokeCounter.text = strokesLeft >= 0 ? strokesLeft.ToString() : "X";
        UpdatePlants();
        CheckGameOver();
    }

    void CheckGameOver()
    {
        if(strokesLeft < 0)
            StartCoroutine(GameOver());
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

    IEnumerator GameOver()
    {
        playerController.isCanWalk= false;
        playerAnimator.Play("Cry");

        yield return new WaitForSeconds(1);

        loadScene.Reload();
		yield return null;
    }
}
