using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    static public GameManager Instance;

    ItemController[] gems;
    ExitController[] doors;

    float passedTime;

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

    public void GameClear()
    {
        
    }


}
