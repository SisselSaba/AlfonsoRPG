using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Companion : MonoBehaviour
{
    [SerializeField] MainChar mainChar;

    List<Vector3> storedPositions = new List<Vector3>();
    Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        Vector3 currentMainCharPos = new Vector3(mainChar.transform.position.x, mainChar.transform.position.y, mainChar.transform.position.z);
        Vector3 companionPos = gameObject.transform.position;

        float movementDistance = Time.deltaTime * mainChar.moveSpeed;

        if (currentMainCharPos.x > companionPos.x)
        {
            float nextPosX = companionPos.x + movementDistance;

            while (nextPosX < currentMainCharPos.x)
            {
                storedPositions.Add(new Vector3(nextPosX, currentMainCharPos.y, currentMainCharPos.z));
                nextPosX += movementDistance;
            }
        }
        else if (currentMainCharPos.x < companionPos.x)
        {
            float nextPosX = companionPos.x - movementDistance;

            while (nextPosX > currentMainCharPos.x)
            {
                storedPositions.Add(new Vector3(nextPosX, currentMainCharPos.y, currentMainCharPos.z));
                nextPosX -= movementDistance;
            }
        }
        else if (currentMainCharPos.y > companionPos.y)
        {
            float nextPosY = companionPos.y + movementDistance;

            while (nextPosY < currentMainCharPos.y)
            {
                storedPositions.Add(new Vector3(currentMainCharPos.x, nextPosY, currentMainCharPos.z));
                nextPosY += movementDistance;
            }
        }
        else if (currentMainCharPos.y < companionPos.y)
        {
            float nextPosY = companionPos.y - movementDistance;

            while (nextPosY > currentMainCharPos.y)
            {
                storedPositions.Add(new Vector3(currentMainCharPos.x, nextPosY, currentMainCharPos.z));
                nextPosY -= movementDistance;
            }
        }

        storedPositions.Add(currentMainCharPos);

        animator = gameObject.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 currentMainCharPos = new Vector3(mainChar.transform.position.x, mainChar.transform.position.y, mainChar.transform.position.z);
        Vector3 lastMainCharPos = storedPositions.Last();

        if (currentMainCharPos.x == lastMainCharPos.x && currentMainCharPos.y == lastMainCharPos.y)
        {
            animator.SetBool("WalkingUp", false);
            animator.SetBool("WalkingDown", false);
            animator.SetBool("WalkingLeft", false);
            animator.SetBool("WalkingRight", false);
            return;
        }

        storedPositions.Add(currentMainCharPos);
        Vector3 nextPosition = storedPositions[0];

        if (nextPosition.x > gameObject.transform.position.x)
        {
            animator.SetBool("WalkingUp", false);
            animator.SetBool("WalkingDown", false);
            animator.SetBool("WalkingLeft", false);
            animator.SetBool("WalkingRight", true);
        }
        else if (nextPosition.x < gameObject.transform.position.x)
        {
            animator.SetBool("WalkingUp", false);
            animator.SetBool("WalkingDown", false);
            animator.SetBool("WalkingLeft", true);
            animator.SetBool("WalkingRight", false);
        }
        else if (nextPosition.y > gameObject.transform.position.y)
        {
            animator.SetBool("WalkingUp", true);
            animator.SetBool("WalkingDown", false);
            animator.SetBool("WalkingLeft", false);
            animator.SetBool("WalkingRight", false);
        }
        else if (nextPosition.y < gameObject.transform.position.y)
        {
            animator.SetBool("WalkingUp", false);
            animator.SetBool("WalkingDown", true);
            animator.SetBool("WalkingLeft", false);
            animator.SetBool("WalkingRight", false);
        }

        gameObject.transform.position = new Vector3(nextPosition.x, nextPosition.y, nextPosition.z);
        storedPositions.RemoveAt(0);
    }
}
