using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeManager : MonoBehaviour
{
    [SerializeField] private float secondPerRealTimeSecond; // 게임 세계에서의 100초 = 현실 세계의 1초
    public bool isNight = false;
    public bool isLightOff = false;

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

    float x = 139;


    void Update()
    {

        // 계속 태양을 X 축 중심으로 회전. 현실시간 1초에  0.1f * secondPerRealTimeSecond 각도만큼 회전
        //transform.Rotate(Vector3.right, 0.1f * secondPerRealTimeSecond * Time.deltaTime);


        transform.rotation = Quaternion.Euler(x - 50, 0, 0);
        if (isNight)
        {
            x += 0.5f * secondPerRealTimeSecond * Time.deltaTime;
        }
        else
            x += 0.1f * secondPerRealTimeSecond * Time.deltaTime;


        //if (transform.eulerAngles.x >= 160/* && transform.eulerAngles.x < 170*/)
        //{
        //    isLightOff = true; // x 축 회전값 170 이상이면 밤이라고 하겠음
        //}

        if (transform.localEulerAngles.x >= 170)
        {
            isNight = true;

        }

        // x 축 회전값 170 이상이면 밤이라고 하겠음
        if (transform.localEulerAngles.x >= 345 && transform.localEulerAngles.x < 350)
        {
            isLightOff = true;
            Invoke("Lightoff", 3f);
        }
        //if(transform.localEulerAngles.x >= 310)
        //{
        //    isLightOff = false;
        //}
        

        else if (transform.eulerAngles.x <= 10)  // x 축 회전값 10 이하면 낮이라고 하겠음
            isNight = false;

        if (isNight)
        {
            
            if (currentLightDensity >= nightLightDensity)
            {
                currentLightDensity -= 5f * fogDensityCalc * Time.deltaTime;
                RenderSettings.ambientIntensity = currentLightDensity;
            }
        }
        if(!isNight)
        {
            if (currentLightDensity <= dayLightDensity)
            {
                currentLightDensity += 0.5f * fogDensityCalc * Time.deltaTime;
                RenderSettings.ambientIntensity = currentLightDensity;
            }
        }
    }

    public void Lightoff()
    {
        isLightOff = false;

    }
}