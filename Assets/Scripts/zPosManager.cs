using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class zPosManager : MonoBehaviour
{
    [SerializeField] float zPositionDefault = 10f;

    Camera sceneCamera;

    // Start is called before the first frame update
    void Start()
    {
        sceneCamera = FindObjectOfType<Camera>();
    }

    // Update is called once per frame
    void Update()
    {
        float objectPos = gameObject.transform.position.y;
        float cameraPos = sceneCamera.transform.position.y;

        float zPos = objectPos - cameraPos + zPositionDefault;

        gameObject.transform.position = new Vector3(gameObject.transform.position.x, gameObject.transform.position.y, zPos);
    }
}
