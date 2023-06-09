using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro; 

public class TimeController : MonoBehaviour
{
    [SerializeField] private float timeMultiplier;
    [SerializeField] private float startHour;
    [SerializeField] private TextMeshProUGUI timeText;
    [SerializeField] private Light sunLight;
    [SerializeField] private float sunriseHour;
    [SerializeField] private float sunsetHour;
    [SerializeField] private Color dayAmbientLight;
    [SerializeField] private Color nightAmbientLight;
    [SerializeField] private AnimationCurve lightCurve;
    [SerializeField] private float maxSunLight;
    [SerializeField] private Light moonLight;
    [SerializeField] private float maxMoonLight;

    private DateTime currentTime;
    private TimeSpan sunriseTime;
    private TimeSpan sunsetTime;

    


    
    void Start()
    {
        currentTime = DateTime.Now.Date + TimeSpan.FromHours(startHour);                        //Start the time from time specified in inspector (12 midday)
        sunriseTime = TimeSpan.FromHours(sunriseHour);                                          //Sunrise time set to 6
        sunsetTime = TimeSpan.FromHours(sunsetHour);                                            //Sunset time set to 21
    }

    
    void Update()
    {
        UpdateTimeOfDay();
        RotateSun();
        UpdateLightSettings();
    }

    private void UpdateTimeOfDay()
    {
        currentTime = currentTime.AddSeconds(Time.deltaTime * timeMultiplier);                  //Multiply the second counter by 100
        if (timeText != null)                                                                   //If the UI of timeText has a time then display in the below format
        {
            timeText.text = "Time of Day-" + currentTime.ToString("HH:mm");
        }
    }


    private void RotateSun()
    {
        float sunLightRotation;
        if (currentTime.TimeOfDay > sunriseTime && currentTime.TimeOfDay < sunsetTime)
        {
            TimeSpan sunriseToSunset = TimeDifference(sunriseTime, sunsetTime);
            TimeSpan timeSinceSunrise = TimeDifference(sunriseTime, currentTime.TimeOfDay);
            double percentage = timeSinceSunrise.TotalMinutes / sunriseToSunset.TotalMinutes;
            sunLightRotation = Mathf.Lerp(0, 180, (float)percentage);
        }
        else
        {
            TimeSpan sunsetToSunrise = TimeDifference(sunsetTime, sunriseTime);
            TimeSpan timeSinceSunset = TimeDifference(sunsetTime, currentTime.TimeOfDay);
            double percentage = timeSinceSunset.TotalMinutes / sunsetToSunrise.TotalMinutes;
            sunLightRotation = Mathf.Lerp(180, 360, (float)percentage);

        }

        sunLight.transform.rotation = Quaternion.AngleAxis(sunLightRotation, Vector3.right);
    }

    private void UpdateLightSettings()
    {
        float dotProduct = Vector3.Dot(sunLight.transform.forward, Vector3.down);
        sunLight.intensity = Mathf.Lerp(0, maxSunLight, lightCurve.Evaluate(dotProduct));
        moonLight.intensity = Mathf.Lerp(maxMoonLight, 0, lightCurve.Evaluate(dotProduct));
        RenderSettings.ambientLight = Color.Lerp(nightAmbientLight, dayAmbientLight, lightCurve.Evaluate(dotProduct));

    }
    private TimeSpan TimeDifference(TimeSpan fromTime, TimeSpan toTime) 
    {
        TimeSpan difference = toTime - fromTime;
        if (difference.TotalSeconds < 0)
        {
            difference += TimeSpan.FromHours(24);
        }

        return difference;
    }

}
