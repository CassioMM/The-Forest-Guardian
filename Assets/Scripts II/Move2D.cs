using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Move2D : MonoBehaviour
{
    public Rigidbody2D rb;
    public int moveSpeed;
    private float direction;

    public Animator animator;

    private Vector3 facingRight;
    private Vector3 facingLeft;

    public bool taNoChao;
    public Transform detectaChao;
    public LayerMask oQueEhChao;

    public int pulosExtras = 1;

    public float IntiX, InteY;

    public bool canMove = true;

    // Start is called before the first frame update
    void Start()
    {
        facingRight = transform.localScale;
        facingLeft = transform.localScale;
        facingLeft.x = facingLeft.x * -1;
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (canMove)
        {
            rb.linearVelocity = new Vector2(direction * moveSpeed, rb.linearVelocity.y);


            if (Input.GetAxis("Horizontal") != 0)
            {
                //esta correndo
                animator.SetBool("taCorrendo", true);

            }
            else
            {
                //esta parado
                animator.SetBool("taCorrendo", false);
            }


            taNoChao = Physics2D.OverlapCircle(detectaChao.position, 0.2f, oQueEhChao);

            if (Input.GetButtonDown("Jump2D") && taNoChao == true)
            {
                rb.linearVelocity = Vector2.up * 12;

                // ativar a anima��o do pulo
                animator.SetBool("taPulando", true);
            }

            if (Input.GetButtonDown("Jump2D") && taNoChao == false && pulosExtras > 0)
            {
                rb.linearVelocity = Vector2.up * 12;
                pulosExtras--;

                // ativar a anima��o do pulo duplo
                animator.SetBool("puloDuplo", true);
            }

            if (taNoChao && rb.linearVelocity.y == 0)
            {
                pulosExtras = 1;
                animator.SetBool("taPulando", false);
                animator.SetBool("puloDuplo", false);
            }


            direction = Input.GetAxis("Horizontal");

            if (direction > 0)
            {
                //olhando para a direita
                transform.localScale = facingRight;
            }

            if (direction < 0)
            {
                //olhando para a direita
                transform.localScale = facingLeft;
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        

        if (collision.gameObject.tag == "respawn")
        {
            StartCoroutine(TP());
        }

        if (collision.gameObject.tag == "finish")
        {
            StartCoroutine(LoadLevelTransition(SceneManager.GetActiveScene().buildIndex + 1));
        }

    }

    IEnumerator TP()
    {
        canMove = false;
        yield return new WaitForSeconds(0.1f);
        gameObject.transform.position = new Vector3(IntiX, InteY, 0);
        yield return new WaitForSeconds(0.1f);
        canMove = true;
    }

    IEnumerator LoadLevelTransition(int levelIndex)
    {
        yield return null;

        SceneManager.LoadScene(levelIndex);


    }

    public void StartGame2()
    {
        SceneManager.LoadScene("Menu", LoadSceneMode.Single);
    }

}
