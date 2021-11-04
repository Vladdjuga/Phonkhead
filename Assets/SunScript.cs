using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;
public class SunScript : MonoBehaviour
{
    public Light2D SUN;
    void Start()
    {
        SUN = GetComponent<Light2D>();
    }

    public float time = 0f;
    int is_night = 1;
    public float r = 1f;
    public float g = 1f;
    public float b = 1f;
    public Color evening;
    public Color day;
    public Color morning;
    public Color night;
    public int curr_time = 0;
    public int day_speed = 10;
    void Update()
    {
        time += 1 * Time.deltaTime/ day_speed;
        SUN.intensity -= is_night * Time.deltaTime / day_speed;
        switch (curr_time)
        {
            case 0:
                if (r > evening.r)
                {
                    r -= 0.001f;
                }
                else if(r<evening.r)
                {
                    r += 0.001f;
                }
                if (g > evening.g)
                {
                    g -= 0.001f;
                }
                else if (g < evening.g)
                {
                    g += 0.001f;
                }
                if (b > evening.b)
                {
                    b -= 0.001f;
                }
                else if (b < evening.b)
                {
                    b += 0.001f;
                }
                break;

            case 1:
                if (r > day.r)
                {
                    r -= 0.001f;
                }
                else if (r < day.r)
                {
                    r += 0.001f;
                }
                if (g > day.g)
                {
                    g -= 0.001f;
                }
                else if (g < day.g)
                {
                    g += 0.001f;
                }
                if (b > day.b)
                {
                    b -= 0.001f;
                }
                else if (b < day.b)
                {
                    b += 0.001f;
                }
                break;
            case 2:
                if (r > evening.r)
                {
                    r -= 0.001f;
                }
                else if (r < evening.r)
                {
                    r += 0.001f;
                }
                if (g > evening.g)
                {
                    g -= 0.001f;
                }
                else if (g < evening.g)
                {
                    g += 0.001f;
                }
                if (b > evening.b)
                {
                    b -= 0.001f;
                }
                else if (b < evening.b)
                {
                    b += 0.001f;
                }
                break;
            case 3:
                if (r > night.r)
                {
                    r -= 0.001f;
                }
                else if (r < night.r)
                {
                    r += 0.001f;
                }
                if (g > night.g)
                {
                    g -= 0.001f;
                }
                else if (g < night.g)
                {
                    g += 0.001f;
                }
                if (b > night.b)
                {
                    b -= 0.001f;
                }
                else if (b < night.b)
                {
                    b += 0.001f;
                }
                break;
        }

        SUN.color = new Color(r,g,b, 0.5f);

        if (SUN.intensity >= 0.7)
            curr_time = 1;
        else if (SUN.intensity >= 0.5)
            curr_time = 0;
        else if(SUN.intensity >= 0.4)
            curr_time = 2;
        else if(SUN.intensity >= 0.0)
            curr_time = 3;

        if (SUN.intensity<=0.1|| SUN.intensity >=1)
        {
            time = 0;
            is_night *= -1;
        }
    }
    public void SunSetDay()
    {
        SUN.intensity = 1f;
    }

    public void SunSetEvning()
    {
        SUN.intensity = 0.5f;
    }

    public void SunSetNight()
    {
        SUN.intensity = 0.2f;
    }
}
