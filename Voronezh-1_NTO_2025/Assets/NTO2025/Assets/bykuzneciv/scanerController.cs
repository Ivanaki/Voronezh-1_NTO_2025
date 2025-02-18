using System.Collections;
using System.Collections.Generic;
using R3;
using UnityEngine;
using UnityEngine.SceneManagement;
public class scanerController : MonoBehaviour
{
    public Subject<string> end = new Subject<string>();
    
    [SerializeField]
    private Animator Animator;
    [SerializeField]
    private float ScanTime = 2;
    [SerializeField]
    private float FadeTime = 1;
    private float LocalTime = 0;
    private string Number;
    
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //print(LocalTime);
        if (LocalTime != 0)
        {
            if (LocalTime + ScanTime < Time.time && LocalTime + ScanTime + FadeTime > Time.time)
            {
                //������ ���������� � ������� <FadeTime> ������
                //print("FADE!!!!");
            }
            if (LocalTime + ScanTime + FadeTime < Time.time)
            {
                end.OnNext(Number);
                //SceneManager.LoadScene(int.Parse(Number));
                //print(int.Parse(Number)+1);
            }
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Paper") && LocalTime == 0f)
        {
            Animator.SetTrigger("X");
            LocalTime = Time.time;
            Number = other.name;
        }
    }
}
