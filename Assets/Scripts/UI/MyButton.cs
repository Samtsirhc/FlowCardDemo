using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyButton : MonoBehaviour
{
    public E_EventType eventType;
    public int p1;
    public int p2;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void OnClicked_01()
    {
        EventCenter.Broadcast(eventType);
    }
    public void OnClicked_02()
    {
        EventCenter.Broadcast<int, int>(eventType, p1, p2);
    }
}
