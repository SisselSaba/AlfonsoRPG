using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainChar : MonoBehaviour
{
    [SerializeField] GameManager gameManager;
    [SerializeField] public float moveSpeed = 5f;
    [SerializeField] FadeScreen fadeScreen;

    Dictionary<int, string> directions = new Dictionary<int, string>();
    Dictionary<string, bool> collidingWalls = new Dictionary<string, bool>();
    string currentDirection = "None";
    bool canInteract = false;

    SignTest signTest;
    Animator animator;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collidingWalls.ContainsKey(collision.gameObject.tag))
        {
            collidingWalls[collision.gameObject.tag] = true;
            return;
        }

        if (collision.gameObject.tag == "signTest")
        {
            collidingWalls["topWall"] = true;
            canInteract = true;
            signTest = collision.gameObject.GetComponent<SignTest>();
        }

        if (collision.gameObject.tag == "testEnemy")
        {
            gameManager.gamePaused = true;

            fadeScreen.sceneNumber = 1;
            fadeScreen.fadingToScene = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collidingWalls.ContainsKey(collision.gameObject.tag))
        {
            collidingWalls[collision.gameObject.tag] = false;
            return;
        }

        if (collision.gameObject.tag == "signTest")
        {
            collidingWalls["topWall"] = false;
            canInteract = false;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        InitDirectionsDictionary();
        InitWallsDictionary();

        animator = gameObject.GetComponent<Animator>();
    }

    private void InitWallsDictionary()
    {
        collidingWalls.Add("topWall", false);
        collidingWalls.Add("rightWall", false);
        collidingWalls.Add("bottomWall", false);
        collidingWalls.Add("leftWall", false);
    }

    private void InitDirectionsDictionary()
    {
        directions.Add(0, "None");
        directions.Add(1, "Up");
        directions.Add(2, "Right");
        directions.Add(3, "Down");
        directions.Add(4, "Left");

        currentDirection = directions[0];
    }

    // Update is called once per frame
    void Update()
    {
        if (gameManager.gamePaused || !gameManager.userInputEnabled)
            return;

        Move();
        Interact();
    }

    private void Interact()
    {
        if (canInteract && Input.GetButton("Jump"))
        {
            Debug.Log(signTest.message);
        }
    }

    private void Move()
    {
        Dictionary<string, bool> moveInputsDown = new Dictionary<string, bool>();

        bool isMoving = false;

        for (int i = 1; i <= 4; i++)
        {
            moveInputsDown.Add(directions[i], Input.GetButton(directions[i]));

            if (moveInputsDown[directions[i]])
                isMoving = true;
        }

        if (!isMoving) {
            currentDirection = directions[0];
            animator.SetBool("WalkingUp", false);
            animator.SetBool("WalkingDown", false);
            animator.SetBool("WalkingLeft", false);
            animator.SetBool("WalkingRight", false);
            return;
        }

        for(int i = 1; i <= 4; i++)
        {
            if (moveInputsDown[directions[i]] && currentDirection != directions[i])
            {
                if (currentDirection == directions[0] || !moveInputsDown[currentDirection])
                    currentDirection = directions[i];
            }
        }

        float deltaX = 0;
        float deltaY = 0;

        if (currentDirection == directions[1])
        {
            animator.SetBool("WalkingUp", true);
            animator.SetBool("WalkingDown", false);
            animator.SetBool("WalkingLeft", false);
            animator.SetBool("WalkingRight", false);
        }
        else if (currentDirection == directions[2])
        {
            animator.SetBool("WalkingUp", false);
            animator.SetBool("WalkingDown", false);
            animator.SetBool("WalkingLeft", false);
            animator.SetBool("WalkingRight", true);
        }
        else if (currentDirection == directions[3])
        {
            animator.SetBool("WalkingUp", false);
            animator.SetBool("WalkingDown", true);
            animator.SetBool("WalkingLeft", false);
            animator.SetBool("WalkingRight", false);
        }
        else if (currentDirection == directions[4])
        {
            animator.SetBool("WalkingUp", false);
            animator.SetBool("WalkingDown", false);
            animator.SetBool("WalkingLeft", true);
            animator.SetBool("WalkingRight", false);
        }

        float movementDistance = Time.deltaTime * moveSpeed;

        if (currentDirection == directions[1] && !collidingWalls["topWall"])
        {
            deltaY = movementDistance;
        }
        else if (currentDirection == directions[2] && !collidingWalls["rightWall"])
        {
            deltaX = movementDistance;
        }
        else if (currentDirection == directions[3] && !collidingWalls["bottomWall"])
        {
            deltaY = -movementDistance;
        }
        else if (currentDirection == directions[4] && !collidingWalls["leftWall"])
        {
            deltaX = -movementDistance;
        }
        else
            return;

        transform.position = new Vector3(transform.position.x + deltaX, transform.position.y + deltaY, transform.position.z);
    }
}
