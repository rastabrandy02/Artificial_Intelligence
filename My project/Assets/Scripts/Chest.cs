using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Chest : MonoBehaviour
{
    public GameObject top;
    public bool hasToOpen;
    public bool isOpen;
    delegate IEnumerator State();
    private State state;
    // Start is called before the first frame update
    IEnumerator Start()
    {
        state = Idle;

        while (enabled)
            yield return StartCoroutine(state());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator Idle()
    {

        if (hasToOpen)
        {
            hasToOpen = false;
            state = Open;
            

        }
        yield return null;
    }
    IEnumerator Open()
    {
       
        
            
            for (int i = 0; i < 36; i++)
            {
                //if (top.transform.localRotation.eulerAngles.x < -180)
                //{
                //break;
                //}

                top.transform.Rotate(-5.0f, 0.0f, 0.0f);
                Debug.Log(top.transform.localRotation.eulerAngles.x);
                //Debug.Log(i);

                yield return new WaitForSeconds(0.05f);
            }
           
            isOpen = true;
        state = Idle;
        
        
    }
}
