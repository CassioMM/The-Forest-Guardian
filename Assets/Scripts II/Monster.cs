using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Monster : MonoBehaviour
{
    public NavMeshAgent navMeshAgent;
    public float heath = 20f;
    public int startWaitTime = 4;
    public float timeToRotate = 2;
    public float speedWalk = 3.5f;
    public float speedRun = 9;
    public float WaitMove = 3f;
    public Rigidbody Rb;

    public float viewRadius = 15;
    public float viewAngle = 90;
    public LayerMask playerMask;
    public LayerMask obstacleMask;


    public Animator anim;

    public Transform[] waypoints;
    public Transform AlvoP;

    int m_CurrentWaypointIndex;

    Vector3 playerLastPosition = Vector3.zero;
    Vector3 m_PlayerPosition;

    float m_WaitTime;
    float m_TimeToRotate;
    bool m_playerInRange;
    bool m_PlayerNear;
    bool m_IsPatrol;
    bool m_CaughtPlayer;
    public bool canMove;
    bool estadoSom = false;

    private GameObject maoInimigo;

    public AudioSource AudioPersegui��o;
    [SerializeField] private AudioSource passosAudioSource;
    [SerializeField] private AudioClip[] passosAudioClip;


    // Start is called before the first frame update
    void Start()
    {
        m_PlayerPosition = Vector3.zero;
        m_IsPatrol = true;
        m_CaughtPlayer = false;
        m_playerInRange = false;
        m_WaitTime = startWaitTime;
        m_TimeToRotate = timeToRotate;

        m_CurrentWaypointIndex = 0;
        navMeshAgent = GetComponent<NavMeshAgent>();

        navMeshAgent.isStopped = false;
        navMeshAgent.speed = speedWalk;
        navMeshAgent.SetDestination(waypoints[m_CurrentWaypointIndex].position);

        maoInimigo = GameObject.FindWithTag("maoInimigo");
        maoInimigo.SetActive(false);



    }

    // Update is called once per frame
    void Update()
    {
        if (canMove == true)
        {
            EnviromentView();
            Move(speedWalk);

            if (!m_IsPatrol)
            {
                Chasing();
                
            }
            else
            {
                Patroling();
                
            }


        }
        else
        {
            Stop();

        }
        

    }

    private void Chasing()
    {
        m_PlayerNear = false;
        playerLastPosition = Vector3.zero;

        if (!m_CaughtPlayer)
        {
            Move(speedRun);
            navMeshAgent.SetDestination(m_PlayerPosition);
        }

        if (navMeshAgent.remainingDistance <= navMeshAgent.stoppingDistance)
        {
            if(m_WaitTime <= 0 && !m_CaughtPlayer && Vector3.Distance(transform.position, GameObject.FindWithTag("TripaSeca").transform.position) >= 6f)
            {
                m_IsPatrol = true;
                m_PlayerNear = false;
                Move(speedWalk);
                m_TimeToRotate = timeToRotate;
                m_WaitTime = startWaitTime;
                navMeshAgent.SetDestination(waypoints[m_CurrentWaypointIndex].position);
            }
            else
            {
                if(Vector3.Distance(transform.position, GameObject.FindWithTag("TripaSeca").transform.position) >= 1.5f)
                {
                    Stop();
                    m_WaitTime -= Time.deltaTime;
                }
            }
        }

        if(Vector3.Distance(transform.position, GameObject.FindWithTag("TripaSeca").transform.position) <= 1f){
            Rb.isKinematic = true;
        }
        else { 
            Rb.isKinematic = false; 
        }

        if (Vector3.Distance(transform.position, GameObject.FindWithTag("TripaSeca").transform.position) <= 1.5f)
        {
            Attack();
        }
        
    }

    private void Patroling()
    {
        //Se o monstro estiver perto do player, anda at� sua ultima posi��o
        if (m_PlayerNear)
        {
            if (m_TimeToRotate <= 0)
            {
                Move(speedWalk);
                LookingPlayer(playerLastPosition);
            }
            else
            {
                Stop();
                m_TimeToRotate -= Time.deltaTime;
            }
        }
        // Se n�o, ele n�o est� proximo do player, para e depois de um tempo caminha at� o ponto mais proximo
        else
        {
            m_PlayerNear = false;
            playerLastPosition = Vector3.zero;
            navMeshAgent.SetDestination(waypoints[m_CurrentWaypointIndex].position);
            if (navMeshAgent.remainingDistance <= navMeshAgent.stoppingDistance)
            {
                if (m_WaitTime <= 0)
                {
                    NextPoint();
                    Move(speedWalk);
                    m_WaitTime = startWaitTime;
                }
                else
                {
                    Stop();
                    m_WaitTime -= Time.deltaTime;
                }
            }
        }
     
    }

    public void NextPoint()
    {
        m_CurrentWaypointIndex = (m_CurrentWaypointIndex + 1) % waypoints.Length;
        navMeshAgent.SetDestination(waypoints[m_CurrentWaypointIndex].position);
    }


    //Movimentos e Anima��es
    void Move(float speed)
    {
        navMeshAgent.isStopped = false;
        navMeshAgent.speed = speed;

        maoInimigo.SetActive(false);

        anim.SetBool("Walking", true);
        anim.SetBool("Idle", false);
        anim.SetBool("Attack", false);
        anim.SetBool("Stunned", false);

    }

    void Stop()
    {
        navMeshAgent.isStopped = true;
        navMeshAgent.speed = 0f;


        maoInimigo.SetActive(false);

        anim.SetBool("Walking", false);
        anim.SetBool("Idle", true);
        anim.SetBool("Attack", false);
        anim.SetBool("Stunned", false);
    }

    void Attack ()
    {
        //navMeshAgent.isStopped = true;
        navMeshAgent.speed = 0f;

        maoInimigo.SetActive(true);

        anim.SetBool("Idle", false);
        anim.SetBool("Walking", false);
        anim.SetBool("Attack", true);
        anim.SetBool("Stunned", false);

    }

    void Stunned()
    {
        navMeshAgent.isStopped = true;
        navMeshAgent.speed = 0f;

        maoInimigo.SetActive(false);

        anim.SetBool("Idle", false);
        anim.SetBool("Walking", false);
        anim.SetBool("Attack", false);
        anim.SetBool("Stunned", true);

    }

    void CaughtPlayer()
    {
        m_CaughtPlayer = true;
    }


    void LookingPlayer(Vector3 player)
    {
        navMeshAgent.SetDestination(player);
        if(Vector3.Distance(transform.position, player)<= 0.3)
        {
            if(m_WaitTime <= 0)
            {
                m_PlayerNear = false;
                Move(speedWalk);
                navMeshAgent.SetDestination(waypoints[m_CurrentWaypointIndex].position);
                m_WaitTime = startWaitTime;
                m_TimeToRotate = timeToRotate;
            }
            else
            {
                Stop();
                m_WaitTime -= Time.deltaTime;
            }
        }
    }


    /*Cria uma esfera de colis�o entre a posi��o do Monstro e o player, se esse colisor for menor que I
    defina uma angulo entre a posi��o do player e a do monstro e se for menor que o angulo / 2, cria uma posi��o distancia
    entre o player e o monstro. E se quando o Raycast estiver diferente disso tudo. O player esta perto e cancela o patrulhamento, n�o
    esta patrlhando.*/
    void EnviromentView()
    {
        Collider[] playerInRange = Physics.OverlapSphere(transform.position, viewRadius, playerMask);   //  Faz uma esfera sobreposta ao redor do inimigo para detectar a m�scara do jogador no raio de vis�o

        for (int i = 0; i < playerInRange.Length; i++)
        {
            Transform player = playerInRange[i].transform;
            Vector3 dirToPlayer = (player.position - transform.position).normalized;
            if (Vector3.Angle(transform.forward, dirToPlayer) < viewAngle / 2)
            {
                float dstToPlayer = Vector3.Distance(transform.position, player.position);          //  Dist�ncia do inimigo e do jogador
                if (!Physics.Raycast(transform.position, dirToPlayer, dstToPlayer, obstacleMask))
                {
                    transform.LookAt(AlvoP);
                    m_playerInRange = true;
                    m_IsPatrol = false;
                }
                else
                {
                    m_playerInRange = false;
                }
            }
            if (Vector3.Distance(transform.position, player.position) > viewRadius)
            {
                
                m_playerInRange = false;    
            }


            if (m_playerInRange)
            {
               
                m_PlayerPosition = player.transform.position;
            }
        }
    }

    private void Passos()
    {
        passosAudioSource.PlayOneShot(passosAudioClip[Random.Range(0, passosAudioClip.Length)]);
    }

    public void TakeDamage(int damage)
    {
        heath -= damage;

        if (heath <= 0)
        {
            Stunned();
            StartCoroutine(DamageCoroutine());
        }

    }

    private IEnumerator DamageCoroutine()
    {
        canMove = false;
        navMeshAgent.speed = 0f;
        m_playerInRange = false;
        m_CaughtPlayer = true;
        yield return new WaitForSeconds(WaitMove);
        resetSpeed();
        
    }

    private void resetSpeed()
    {
        heath = 20f;
        canMove = true;
        navMeshAgent.speed = speedWalk;
        m_CaughtPlayer = false;

    }

    public void Teleport(Vector3 position, Quaternion rotation)
    {
        transform.position = position;
        Physics.SyncTransforms();
        transform.rotation = rotation;


    }

    public void CancelMControler(bool value)
    {
        canMove = value;


    }

    public void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(transform.position, viewRadius);
    }

}
