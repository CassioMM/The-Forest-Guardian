using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MonsterTP : MonoBehaviour
{
    public Monster monster;
    public PlayerMovimnt player;
    public Mouse_Look visao;

    [SerializeField]
    Transform destination;

    [SerializeField]
    private Image uiFill;

    public float reloadTime;
    float time_remaining;

    void Start()
    {
        uiFill.enabled = false;
        time_remaining = reloadTime;
    }

    private void Update()
    {
        if (uiFill.enabled == true && time_remaining >= 0)
        {
            time_remaining -= Time.deltaTime;
            uiFill.fillAmount = time_remaining / reloadTime;

        }
        else
        {
            uiFill.enabled = false;
            uiFill.fillAmount = reloadTime;
            time_remaining = reloadTime;
        }


    }


    public void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("TripaSeca"))
        {
            StartCoroutine(TP());

            monster.canMove = false;
            monster.navMeshAgent.enabled = false;
            monster.Teleport(destination.position, destination.rotation);
            monster.navMeshAgent.enabled = true;
            monster.canMove = true;

        }


    }


    private void OnDrawGizmos()
    {
        Gizmos.color = Color.white;
        Gizmos.DrawWireSphere(destination.position, .4f);
        var direction = destination.TransformDirection(Vector3.forward);
        Gizmos.DrawRay(destination.position, direction);
    }

    IEnumerator TP()
    {
        player.Armadilha.Play();
        uiFill.enabled = true;
        visao.xRotation = visao.xRotation + 90;
        /*yield return new WaitForSeconds(0.1f);
        visao.xRotation = visao.xRotation - 90;*/
        player.CancelControler(false);
        yield return new WaitForSeconds(reloadTime);
        player.CancelControler(true);

        /*monster.canMove = false;
        monster.navMeshAgent.enabled = false;
        yield return new WaitForSeconds(1f);
        monster.Teleport(destination.position, destination.rotation);
        monster.navMeshAgent.enabled = true;
        monster.canMove = true;*/


    }

}
