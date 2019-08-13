using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IndicatorPanel : MonoBehaviour {

    public Transform OS;
    public GameObject IndicatorSprite;

    public float ScreenOffset = 5.0f;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (OS.childCount > 0)
        {
            Transform first = OS.GetChild(0);
            Vector3 screenPos = Camera.main.WorldToScreenPoint(first.position);

            //Debug.Log(screenPos);


            if (screenPos.z > 0 &&
                screenPos.x > 0 && screenPos.x < Screen.width &&
                screenPos.y > 0 && screenPos.y < Screen.height)
            {
                IndicatorSprite.SetActive(false);
            }
            else
            {
                IndicatorSprite.SetActive(true);
                if (screenPos.z < 0)
                {
                    screenPos *= -1;
                }

                Vector3 screenCenter = new Vector3(Screen.width, Screen.height, 0.0f) / 2;

                //Debug.Log(screenCenter);

                screenPos -= screenCenter;

                float angle = Mathf.Atan2(screenPos.y, screenPos.x);
                //Debug.Log(angle * Mathf.Rad2Deg);
                //angle -= 90 * Mathf.Deg2Rad;

                float cos = Mathf.Cos(angle);
                float sin = Mathf.Sin(angle);

                //screenPos = screenCenter + new Vector3(sin*150, cos*150, 0.0f);

                Vector3 screenBounds = screenCenter * 0.9f;
                float m = sin / cos;

                //screenPos = new Vector3(0.0f, 0.0f, ScreenOffset);

                if (screenPos.x > screenBounds.x)
                {
                    //float temp = screenBounds.x * m;
                    screenPos = new Vector3(screenBounds.x, screenPos.y, ScreenOffset);
                }
                else if (screenPos.x < -screenBounds.x)
                {
                    screenPos = new Vector3(-screenBounds.x, screenPos.y, ScreenOffset);
                }
                if (screenPos.y > screenBounds.y)
                {
                    //float temp = screenBounds.x * m;
                    screenPos = new Vector3(screenPos.x, screenBounds.y, ScreenOffset);
                }
                else if (screenPos.y < -screenBounds.y)
                {
                    screenPos = new Vector3(screenPos.x, -screenBounds.y, ScreenOffset);
                }

                screenPos += screenCenter;

                screenPos = Camera.main.ScreenToWorldPoint(screenPos);

                IndicatorSprite.transform.localPosition = screenPos;
                IndicatorSprite.transform.localRotation = Quaternion.Euler(0.0f, 0.0f, (angle - 90.0f * Mathf.Deg2Rad) * Mathf.Rad2Deg);
            }
        }
	}

}
