using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerController : MonoBehaviour
{
    public Rigidbody2D rigidb;
    public Animator ani;
    [SerializeField] LayerMask groundLayer;
    [SerializeField] float Char_speed;
    [SerializeField] float Char_jumpForce;
    [SerializeField] bool isGrounded;
    [SerializeField] int jumpStep; 
    float horizontal;
    // Start is called before the first frame update
    void Start()
    {
        rigidb = GetComponentInParent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        isGrounded = Physics2D.OverlapCircle(new Vector2(transform.position.x, transform.position.y - 0.25f), 0.25f, groundLayer);
        Run();
    }
    public void Run()
    {
        horizontal = Input.GetAxisRaw("Horizontal");
        ani.SetFloat("Speed", Mathf.Abs(horizontal));
    }
}
