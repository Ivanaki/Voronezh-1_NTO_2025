using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class lighter : MonoBehaviour
{
    [SerializeField]
    private TextMeshPro text;
    [SerializeField]
    private TextMeshPro text2;
    [SerializeField]
    private Rigidbody car;
    [SerializeField]
    private GameObject ob1;
    [SerializeField]
    private GameObject ob2;
    [SerializeField]
    private GameObject ob3;
    [SerializeField]
    private GameObject ob4;
    [SerializeField]
    private GameObject ob5;
    private float localTime;
    private float a;
    void Start()
    {
        if (PlayerPrefs.GetFloat("record") < 5f)
        {
            PlayerPrefs.SetFloat("record", 999);
        }
        localTime = 0f;
        a = Random.value * (1.5f - 0.5f);
    }
    void Update()
    {
        text.text = Mathf.Floor(PlayerPrefs.GetFloat("record")).ToString() + ":" + ((Mathf.Floor(((PlayerPrefs.GetFloat("record")) - Mathf.Floor(PlayerPrefs.GetFloat("record"))) * 100))).ToString();
        print(global.checkpoints);
        if (localTime == -1)
        {
            ob1.SetActive(false);
            ob2.SetActive(false);
            ob3.SetActive(false);
            ob5.SetActive(true);
        }
        if (localTime > 0)
        {
            if (localTime + 1 < Time.time)
            {
                ob5.SetActive(false);
                ob1.SetActive(false);
                ob2.SetActive(true);
                if (car.velocity.magnitude * 1000000 > 3 && !(localTime + 4 + a < Time.time))
                {
                    localTime = -1f;
                }
                if (localTime + 2 < Time.time)
                {

                    ob1.SetActive(false);
                    ob2.SetActive(false);
                    ob3.SetActive(true);
                    
                    if (localTime + 4 + a < Time.time)
                    {
                        if (global.checkpoints == -1)
                        {
                            global.checkpoints = 0;
                        }
                        
                        ob1.SetActive(false);
                        ob2.SetActive(false);
                        ob3.SetActive(false);
                        ob4.SetActive(true);
                      
                        text2.text = (Mathf.Floor(Time.time - (localTime + 4 + a))).ToString() + ":" + (Mathf.Floor(((Time.time - (localTime + 4 + a)) - Mathf.Floor(Time.time - (localTime + 4 + a)))*100)).ToString();
                    }
                }
            }

        }
        else
        {
            text2.text = "00:00";
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("RaceCar"))
        {
            if (localTime == 0)
            {
                global.checkpoints = -1;
                
                localTime = Time.time;
                ob1.SetActive(true);
                ob2.SetActive(false);
                ob3.SetActive(false);
                ob4.SetActive(false);
                
            }
            if (global.checkpoints >= 7)
            {

                if (Mathf.Floor(Time.time - (localTime + 4 + a)) < PlayerPrefs.GetFloat("record"))
                {
                    PlayerPrefs.SetFloat("record", Time.time - (localTime + 4 + a));
                }
                localTime = 0;
                ob1.SetActive(true);
                ob2.SetActive(false);
                ob3.SetActive(false);
                ob4.SetActive(false);
                global.checkpoints = -1;

                localTime = Time.time;
                ob1.SetActive(true);
                ob2.SetActive(false);
                ob3.SetActive(false);
                ob4.SetActive(false);

            }
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("RaceCar"))
        {
            if (localTime == -1)
            {
                ob1.SetActive(true);
                ob2.SetActive(false);
                ob3.SetActive(false);
                ob4.SetActive(false);
                ob5.SetActive(false);
                localTime = 0;

            }
        }
    }
}