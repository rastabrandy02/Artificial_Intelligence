using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chest : MonoBehaviour
{
    public GameObject top;
    public bool hasToOpen;
    public bool isOpen;
    // Start is called before the first frame update
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        if (hasToOpen)
        {
           StartCoroutine( Open());
            hasToOpen = false;
            
        }
    }

    IEnumerator Open()
    {
        for(int i = 0; i <36; i++)
        {
            
            top.transform.Rotate(-5.0f, 0.0f, 0.0f);
            Debug.Log(i);
            yield return new WaitForSeconds(0.05f);
            
        }
        isOpen = true;
    }
}
