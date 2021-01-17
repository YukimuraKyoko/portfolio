using System;
using UdonSharp;
using UnityEngine;
using UnityEngine.UI;
using VRC.SDKBase;
using VRC.Udon;

public class Clock : UdonSharpBehaviour
{
	float degreesPerHour = 30f;
	float degreesPerMinute = 6f;
	float degreesPerSecond = 6f;
	public Transform hoursTransform;
	public Transform minutesTransform;
	public Transform secondsTransform;
	public GameObject NumberTime;
	public GameObject NumberTime2;
	public GameObject NumberTime3;
	//public GameObject VRCPlayerTime;
	public GameObject TimeIn;
	string min;
	string sec;

	public void Start()
	{
		DateTime time = DateTime.Now;
		hoursTransform.localRotation =
		Quaternion.Euler(0f, time.Hour * degreesPerHour, 0f);
		hoursTransform.localRotation =
		Quaternion.Euler(0f, time.Minute * degreesPerMinute, 0f);
		hoursTransform.localRotation =
		Quaternion.Euler(0f, time.Second * degreesPerSecond, 0f);
		if (time.Minute < 10)
		{
			min = "0";
		}
		else
		{
			min = "";
		}

		TimeIn.GetComponent<Text>().text = "Time In: " + time.Hour + ":" + min + time.Minute;
	}





	public void Update()
	{
		DateTime time = DateTime.Now;
		hoursTransform.localRotation =
			Quaternion.Euler(0f, time.Hour * degreesPerHour, 0f);
		minutesTransform.localRotation =
			Quaternion.Euler(0f, time.Minute * degreesPerMinute, 0f);
		secondsTransform.localRotation =
			Quaternion.Euler(0f, time.Second * degreesPerSecond, 0f);
		
		if(time.Minute < 10)
		{
			min = "0";
		}
		else
		{
			min = "";
		}


		NumberTime.GetComponent<Text>().text = time.Hour + ":" + min + time.Minute;
		NumberTime2.GetComponent<Text>().text = time.Hour + ":" + min + time.Minute;
		NumberTime3.GetComponent<Text>().text = time.Hour + ":" + min + time.Minute;
		//VRCPlayerTime.GetComponent<Text>().text = time.Hour + ":" + min + time.Minute;



	}
}
