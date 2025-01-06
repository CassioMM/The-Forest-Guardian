using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;



[System.Serializable]
public class PlayerMovimnt : MonoBehaviour
{
    public float heath = 100f;
    private int attacks = 1;

    public static PlayerMovimnt obj;

    public CharacterController controller;
    public float speed = 3f;
    public float runningSpeed = 9f;
    public float gravity = -10f;
    public float jumpHeigth;
    public Animator anim;
    public GameObject sangueNaTela;
    public int damage = 20;

    public Transform groundCheck;
    public float groundDistance;
    public LayerMask groundMask;

    public Vector3 velocity = Vector3.zero;

    bool isGrounded;
    float initialSpeed;

    public bool canMove = true;

    public AudioSource Damage;
    public AudioSource Armadilha;


    [SerializeField] private AudioSource passosAudioSource;
    [SerializeField] private AudioClip[] passosAudioClip;

    public float passosAudioSourceV = 0.478f;
    public float DamageV = 0.478f;

    public Vector2 ForwardZX => new(transform.forward.x, transform.forward.z);


    void Start()
    {
        initialSpeed = speed;

        sangueNaTela.SetActive(false);

    }

    
    void Update()
    {

        /*if (Stoned == true)
        {
            speed = 1f;
            runningSpeed = 3f;
        }

        if (Stoned == false)
        {
            speed = 3f;
            runningSpeed = 9f;

        }*/

        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;

        }

        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            speed = runningSpeed;
        }


        if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            speed = initialSpeed;
        }


        /*if (Input.GetButtonDown("Jump") && isGrounded)
        {
            velocity.y = Mathf.Sqrt(jumpHeigth * -2f * gravity);
        }*/


        float moveX = 0f;
        float moveZ = 0f;

        //Vector3 move = transform.right * moveX + transform.forward * moveZ;


        if (canMove)
        {
            moveX = Input.GetAxis("Horizontal");
            moveZ = Input.GetAxis("Vertical");

            Vector3 move = transform.right * moveX + transform.forward * moveZ;

            controller.Move(speed * Time.deltaTime * move);
            //velocity.y += gravity * Time.deltaTime;
            controller.Move(velocity * Time.deltaTime);

        }

        velocity.y += gravity * Time.deltaTime;



        if (controller.isGrounded)
        {
            if (moveZ != 0)
            {
                anim.SetBool("Parado", false);
                anim.SetBool("Andando", true);
            }
            else
            {
                anim.SetBool("Parado", true);
                anim.SetBool("Andando", false);
            }


        }

        if (heath <= 0)
        {
            anim.SetBool("Morte", true);
            speed = 0;
            StartCoroutine("morte");

        }
    }

    private void OnTriggerEnter(Collider collider)
    {
        
        if (collider.gameObject.tag == "maoInimigo" && attacks > 0)
        {
            attacks--;
            StartCoroutine(DamageCoroutine());
            sangueNaTela.SetActive(true);
            Damage.Play();
            Damage.volume = DamageV;
        }



    }

    private void ResetSpeed()
    {
        speed = 3f;
        runningSpeed = 9f;


    }

    private IEnumerator DamageCoroutine()
    {

        heath -= 20f;
        speed = 3f;
        runningSpeed = 3f;
        yield return new WaitForSeconds(1.2f);
        attacks = 1;
        yield return new WaitForSeconds(3f);
        ResetSpeed();
        yield return new WaitForSeconds(1f);
        sangueNaTela.SetActive(false);


    }


    public void CancelControler(bool value)
    {
        canMove = value;

    }


    void morte()
    {
        SceneManager.LoadScene("Game Over"); //temporario

    }

    public void PersoV(float value)
    {

        passosAudioSourceV = value;
        DamageV = value;

    }


    private void Passos()
    {
        passosAudioSource.PlayOneShot(passosAudioClip[Random.Range(0, passosAudioClip.Length)]);
        passosAudioSource.volume = passosAudioSourceV;
    }

}
