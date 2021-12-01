using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour
{
    [SerializeField] MainChar mainChar;
    [SerializeField] bool followMainChar = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        fixPositionOnMainChar();
    }

    private void fixPositionOnMainChar()
    {
        if (!mainChar || !followMainChar)
            return;

        transform.position = new Vector3(mainChar.transform.position.x, mainChar.transform.position.y, -10);
    }
}
