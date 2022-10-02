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


        transform.rotation = Quaternion.Euler(x - 50, 0, 0);
        if (isNight)
        {
            x += 0.5f * secondPerRealTimeSecond * Time.deltaTime;
        }
        else
            x += 0.1f * secondPerRealTimeSecond * Time.deltaTime;


        //if (transform.eulerAngles.x >= 160/* && transform.eulerAngles.x < 170*/)
        //{
        //    isLightOff = true; // x �� ȸ���� 170 �̻��̸� ���̶�� �ϰ���
        //}

        if (transform.localEulerAngles.x >= 170)
        {
            isNight = true;

        }

        // x �� ȸ���� 170 �̻��̸� ���̶�� �ϰ���
        if (transform.localEulerAngles.x >= 345 && transform.localEulerAngles.x < 350)
        {
            isLightOff = true;
            Invoke("Lightoff", 3f);
        }
        //if(transform.localEulerAngles.x >= 310)
        //{
        //    isLightOff = false;
        //}
        

        else if (transform.eulerAngles.x <= 10)  // x �� ȸ���� 10 ���ϸ� ���̶�� �ϰ���
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