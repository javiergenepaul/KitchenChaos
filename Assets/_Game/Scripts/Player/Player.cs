using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private float moveSpeed = PlayerConstant.DEFAULT_MOVESPEED;
    [SerializeField] private float rotateSpeed = PlayerConstant.ROTATE_SPEED;
    [SerializeField] private GameInput gameInput;
    private bool isWalking;

    private void Update()
    {
        Vector2 inputVector = gameInput.GetMovementVectorNormalized();
        Vector3 moveDir = new Vector3(inputVector.x, 0f, inputVector.y);

        bool canMove = !Physics.CapsuleCast(transform.position, 
                                            transform.position + Vector3.up * PlayerConstant.PLAYER_HEIGHT, 
                                            PlayerConstant.PLAYER_RADIUS, 
                                            moveDir, moveSpeed * Time.deltaTime);

        if (!canMove)
        {
            Vector3 moveDirX = new Vector3(moveDir.x, 0, 0).normalized;
            canMove = !Physics.CapsuleCast(transform.position, 
                                            transform.position + Vector3.up * PlayerConstant.PLAYER_HEIGHT, 
                                            PlayerConstant.PLAYER_RADIUS, 
                                            moveDirX, moveSpeed * Time.deltaTime);
            if (canMove)
            {
                moveDir = moveDirX;
            } else
            {
                Vector3 moveDirZ = new Vector3(0, 0, moveDir.z).normalized;
                canMove = !Physics.CapsuleCast(transform.position, 
                                    transform.position + Vector3.up * PlayerConstant.PLAYER_HEIGHT, 
                                    PlayerConstant.PLAYER_RADIUS, 
                                    moveDirZ, moveSpeed * Time.deltaTime);
                if (canMove)
                {
                    moveDir = moveDirZ;
                }
            }

        }
        

        if  (canMove) {
            transform.position += moveDir * moveSpeed * Time.deltaTime;
        } 

        isWalking = moveDir != Vector3.zero;
        transform.forward = Vector3.Slerp(transform.forward, moveDir, Time.deltaTime * rotateSpeed);

    }

    public bool IsWalking() {
        return isWalking;
        
    }

}
