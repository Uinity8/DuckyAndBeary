using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    static public GameManager Instance;

    ItemController[] gems;
    ExitController[] doors;

    [SerializeField] float passedTime;

    int score;

    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    private void Start()
    {
        gems = FindObjectsOfType<ItemController>();
        doors = FindObjectsOfType<ExitController>();
    }

    private void Update()
    {
        passedTime += Time.deltaTime;
    }

    public void GameClearCheck()
    {
        bool isAllOpen = doors.All(door => door.isOpen);

        Debug.Log(isAllOpen?"Game Clear" : "Not Yet");
    }

    public void AddScore(int value)
    {
        score += value;
    }
}
