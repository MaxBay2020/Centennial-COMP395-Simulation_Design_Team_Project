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

    public GameObject[] WeatherSystem;
    private int particleRandom;

    private float changceOfSun = 2f;
    private float SunLength;
    
    private bool isSnowing = false;
    private float dice;
    private float timer = 0f;

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

        if (!isSnowing)
        {
            dice = Random.Range(0f, 100.0f);
            if (dice < changceOfSun)
            {
                //sun
                particleRandom = Random.Range(0, WeatherSystem.Length);
                Snow(particleRandom);
                isSnowing = true;
                timer = Random.Range(5f, 20f);
            }
        }

        if (isSnowing)
        {
            timer -= Time.deltaTime;
            if(timer <= 0)
            {
                isSnowing = false;
                StopSnow(particleRandom);
            }
        }

    }

    private void Snow(int particleRandom)
    {
        WeatherSystem[particleRandom].SetActive(true);
    }

    private void StopSnow(int particleRandom)
    {
        WeatherSystem[particleRandom].SetActive(false);
    }

}
