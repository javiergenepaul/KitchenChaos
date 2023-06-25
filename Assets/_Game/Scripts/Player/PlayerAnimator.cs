using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimator : MonoBehaviour
{
    [SerializeField] private Player player;
    private Animator animator;

    private void Awake() {
        animator = GetComponent<Animator>();
        
    }

    private void Update() {
        animator.SetBool(PlayerConstant.IS_WALKING, player.IsWalking());

    }
}