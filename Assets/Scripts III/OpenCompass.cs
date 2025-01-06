using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenCompass : MonoBehaviour
{
    public GameObject compass;
    public Transform player;

    public float pickUpRange;
    public bool equipped;

    // Start is called before the first frame update
    void Start()
    {
        if (!equipped)
        {
            compass.SetActive(false);


        }

        if (equipped)
        {
            compass.SetActive(true);

        }
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 distanceToPlayer = player.position - transform.position;
        if (!equipped && distanceToPlayer.magnitude <= pickUpRange && Input.GetKeyDown(KeyCode.E))
        {

            compass.SetActive(true);

        }
            
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(transform.position, pickUpRange);
    }
}
