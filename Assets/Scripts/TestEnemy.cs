using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestEnemy : MonoBehaviour
{
    [SerializeField] GameManager gameManager;

    [SerializeField] float moveSpeed = 2.5f;
    string direction = "right";
    float minPosX;
    [SerializeField] float maxPosX = 1;

    // Start is called before the first frame update
    void Start()
    {
        minPosX = transform.position.x;
    }

    // Update is called once per frame
    void Update()
    {
        if (gameManager.gamePaused)
            return;

        Move();
    }

    private void Move()
    {
        float newPosX = transform.position.x + (Time.deltaTime * moveSpeed * (direction == "right" ? 1 : -1));

        if (newPosX >= maxPosX)
        {
            newPosX = maxPosX;
            direction = "left";
        }
        else if (newPosX <= minPosX)
        {
            newPosX = minPosX;
            direction = "right";
        }

        transform.position = new Vector3(newPosX, transform.position.y, transform.position.z);
    }
}
