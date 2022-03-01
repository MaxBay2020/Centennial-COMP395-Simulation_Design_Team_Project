using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public enum Timing
    {
        BUSY=2,
        NOT_BUSY=5

    }

    public enum Weather
    {
        GOOD=2,
        BAD=1
    }

    [Header("Customer Types")]
    public List<GameObject> customers = new List<GameObject>();

    private Vector3 spawnPoint = new Vector3(1.5f, 0.15f, 0);

    [Header("Timing")]
    public Timing currentTiming = Timing.BUSY;
    public float currentSeconds;

    [Header("Weather")]
    public Weather currentWeather = Weather.GOOD;



    // Update is called once per frame
    void Update()
    {
        // factor: Timing
        if (currentSeconds < (int)currentTiming) currentSeconds += Time.deltaTime;
        else
        {
            int index = Random.Range(0, 2);
            Instantiate(customers[index], spawnPoint, Quaternion.identity);
            currentSeconds = 0;
        }
    }

}
