using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeManager : MonoBehaviour
{
    [SerializeField] private float secondPerRealTimeSecond; // ���� ���迡���� 100�� = ���� ������ 1��
    public bool isNight = false;
    public bool isLightOff = false;

    [SerializeField] private float nightFogDensity; // �� ������ Fog �е�
    private float dayFogDensity= 0.01f; // �� ������ Fog �е�
    [SerializeField] private float fogDensityCalc; // ������ ����
    private float currentFogDensity;

    [SerializeField] private float nightLightDensity; // �� ������ intensity �е�
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

        // ��� �¾��� X �� �߽����� ȸ��. ���ǽð� 1�ʿ�  0.1f * secondPerRealTimeSecond ������ŭ ȸ��
        //transform.Rotate(Vector3.right, 0.1f * secondPerRealTimeSecond * Time.deltaTime);

        x += 0.1f * secondPerRealTimeSecond * Time.deltaTime;
        transform.rotation = Quaternion.Euler(x, 0, 0);

            print(transform.localEulerAngles.x);

        //if (transform.eulerAngles.x >= 160/* && transform.eulerAngles.x < 170*/)
        //{
        //    isLightOff = true; // x �� ȸ���� 170 �̻��̸� ���̶�� �ϰ���
        //}

        if (transform.localEulerAngles.x >= 140)
        {
            isNight = true;

        }
        // x �� ȸ���� 170 �̻��̸� ���̶�� �ϰ���

        if (transform.localEulerAngles.x >= 160 && transform.localEulerAngles.x < 170)
        {
            isLightOff = true;

        }


        else if (transform.eulerAngles.x <= 10)  // x �� ȸ���� 10 ���ϸ� ���̶�� �ϰ���
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