using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCon : MonoBehaviour
{
    [SerializeField] private float speed;
    private Vector3 input;
    private bool isMoving;

    private PlayerAni animations;
    [SerializeField] private SpriteRenderer charecterSprite;
    private Rigidbody2D rigidbody;

    // Start is called before the first frame update
    void Start()
    {
        rigidbody = GetComponent<Rigidbody2D>();
        animations = GetComponentInChildren<PlayerAni>();
    }

    // Update is called once per frame
    void Update()
    {
        input = new Vector2(Input.GetAxis("Horizontal"), 0);
        transform.position += input * speed * Time.deltaTime;
        isMoving = input.x != 0 ? true : false;

        if (input.x != 0)
        {
            charecterSprite.flipX = input.x > 0 ? false : true;
        }

        animations.isMoving = isMoving;
    }
}
