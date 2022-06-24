using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StepForward : MonoBehaviour
{
    int currentGeneration = 0;

    void Start()
    {
        
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space)){
            currentGeneration++;
            Debug.Log("gen" + currentGeneration);
        }
    }

    public int GetCurrentGeneration(){
        return currentGeneration;
    }
}
