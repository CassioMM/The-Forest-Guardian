using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;



[System.Serializable]
public class PlayerMovimnt2 : MonoBehaviour
{
    public float heath = 100f;
    public float time = 3f;

    public static PlayerMovimnt obj;

    public CharacterController controller;
    public float speed = 3f;
    public float runningSpeed = 9f;
    public float gravity = -10f;
    public float jumpHeigth;
    public Animator anim;
    public GameObject sangueNaTela;
    public GameObject feitiçoNaTela;
    public GameObject QuickTime;
    public float QuickValue = 50f;
    public int damage = 20;

    public Transform groundCheck;
    public float groundDistance;
    public LayerMask groundMask;

    public MonsterIARA MIara;

    public Vector3 velocity = Vector3.zero;
    bool isGrounded;
    float initialSpeed;
    public bool canMove = true;
    public bool canG = true;

    public AudioSource Damage;
    public AudioSource Feitiço;

    [SerializeField] private AudioSource passosAudioSource;
    [SerializeField] private AudioClip[] passosAudioClip;
    public float passosAudioSourceV = 0.478f;
    public float DamageV = 0.478f;

    public Vector2 ForwardZX => new(transform.forward.x, transform.forward.z);


    void Start()
    {
        initialSpeed = speed;

        sangueNaTela.SetActive(false);
        QuickTime.SetActive(false);

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

        if (collider.gameObject.tag == "maoInimigo")
        {
            StartCoroutine(DamageCoroutine());
            sangueNaTela.SetActive(true);
            Damage.Play();
            Damage.volume = DamageV;
        }

        if (canG)
        {
            if (collider.gameObject.tag == "Encanto")
            {
                StartCoroutine(QuickFuncion());
                QuickTime.SetActive(true);
                feitiçoNaTela.SetActive(true);
                //Feitiço.Play();
            }

        }

        if (canG == false)
        {
            QuickTime.SetActive(false);
            feitiçoNaTela.SetActive(false);

        }

        /*if (collider.gameObject.tag == "maoInimigo")
        {

            Damage.Play();

            StartCoroutine(DamageCoroutine());

            heath -= 4f;
            speed = 3f;
            runningSpeed = 3f;
            Invoke("resetSpeed", 3);

            sangueNaTela.SetActive(true);
            QuickTime.SetActive(true);
        }*/

    }

    private void ResetSpeed()
    {
        speed = 3f;
        runningSpeed = 9f;
        canG = true;

    }

    private IEnumerator DamageCoroutine()
    {
        QuickValue -= .98f;
        heath -= 0.2f;
        speed = 3f;
        runningSpeed = 3f;
        yield return new WaitForSeconds(3f);
        ResetSpeed();
        yield return new WaitForSeconds(1f);
        sangueNaTela.SetActive(false);


    }


    private IEnumerator QuickFuncion()
    {

        speed = 0f;
        runningSpeed = 0f;

        if (canG)
        {
            QuickValue -= .98f;
        }
        //QuickValue -= .98f;
        //feitiçoNaTela.SetActive(true);

        if (Input.GetKey(KeyCode.F) && QuickValue < 100f)
        {
            QuickValue += 5f;

        }

        if (QuickValue >= 100f)
        {
            StartCoroutine(MIara.DamageCoroutine());
            canG = false;
            speed = 3f;
            runningSpeed = 9f;
            yield return new WaitForSeconds(1f);
            ResetSpeed();


            feitiçoNaTela.SetActive(false);
            QuickTime.SetActive(false);


        }


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

    public void QuickTimeEvent(float value)
    {

        QuickValue = 50;

    }

    private void Passos()
    {
        passosAudioSource.PlayOneShot(passosAudioClip[Random.Range(0, passosAudioClip.Length)]);
        passosAudioSource.volume = passosAudioSourceV;
    }

}
