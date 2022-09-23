using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeManager : MonoBehaviour
{
    [SerializeField] private float secondPerRealTimeSecond; // 게임 세계에서의 100초 = 현실 세계의 1초
    public bool isNight = false;

    [SerializeField] private float nightFogDensity; // 밤 상태의 Fog 밀도
    private float dayFogDensity= 0.01f; // 낮 상태의 Fog 밀도
    [SerializeField] private float fogDensityCalc; // 증감량 비율
    private float currentFogDensity;

    [SerializeField] private float nightLightDensity; // 밤 상태의 intensity 밀도
    float dayLightDensity = 1f;
    float currentLightDensity =1f;


    public static TimeManager instance;
    private void Awake()
    {
        instance = this;
    }

    void Start()
    {
        dayFogDensity = RenderSettings.fogDensity;
        dayLightDensity = RenderSettings.ambientIntensity;

    }

    void Update()
    {

        // 계속 태양을 X 축 중심으로 회전. 현실시간 1초에  0.1f * secondPerRealTimeSecond 각도만큼 회전
        transform.Rotate(Vector3.right, 0.1f * secondPerRealTimeSecond * Time.deltaTime);

        if (transform.eulerAngles.x >= 170) // x 축 회전값 170 이상이면 밤이라고 하겠음
            isNight = true;
        else if (transform.eulerAngles.x <= 10)  // x 축 회전값 10 이하면 낮이라고 하겠음
            isNight = false;

        if (isNight)
        {
            //if (currentFogDensity <= nightFogDensity)
            //{
            //    currentFogDensity += 0.1f * fogDensityCalc * Time.deltaTime;
            //    RenderSettings.fogDensity = currentFogDensity;

            //gameObject.GetComponent<LightSwitch>().LightsON = false
            //}


            if (currentLightDensity >= nightLightDensity)
            {
                currentLightDensity -= 0.5f * fogDensityCalc * Time.deltaTime;
                RenderSettings.ambientIntensity = currentLightDensity;
            }
        }
        if(!isNight)
        {
            //if (currentFogDensity >= dayFogDensity)
            //{
            //    currentFogDensity -= 0.1f * fogDensityCalc * Time.deltaTime;
            //    RenderSettings.fogDensity = currentFogDensity;
            //}


            if (currentLightDensity <= dayLightDensity)
            {
                currentLightDensity += 0.5f * fogDensityCalc * Time.deltaTime;
                RenderSettings.ambientIntensity = currentLightDensity;
            }
        }
    }
}