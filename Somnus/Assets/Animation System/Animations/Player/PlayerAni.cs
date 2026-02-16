using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAni : MonoBehaviour
{
    [SerializeField] private Animator animator;

    public bool isMoving;
    // Update is called once per frame
    void Update()
    {
        animator.SetBool("Move", isMoving);
    }
}
