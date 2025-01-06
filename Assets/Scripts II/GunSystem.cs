using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections;

public class GunSystem : MonoBehaviour
{
    //Gun stats
    public int damage;
    public float timeBetweenShooting, spread, range, timeBetweenShots, reloadTime;
    public int magazineSize, bulletsPerTap;
    public bool allowButtonHold;
    int bulletsLeft, bulletsShot;
    float time_remaining;

    //bools 
    bool shooting, readyToShoot, reloading;

    //Reference
    public Camera fpsCam;
    public Transform attackPoint;
    public RaycastHit rayHit;
    public LayerMask whatIsEnemy;

    //Graphics
    public GameObject muzzleFlash, bulletHoleGraphic;
    //public CamShake camShake;
    public float camShakeMagnitude, camShakeDuration;
    public TextMeshProUGUI text;

    [SerializeField]
    private Image uiFill;

    private float remainingDuration;

    private void Awake()
    {
        bulletsLeft = magazineSize;
        readyToShoot = true;
        text.enabled = false;
        uiFill.enabled = false;

        time_remaining = reloadTime;
    }

    private void Update()
    {
        MyInput();

        //SetText
        text.enabled = true;
        text.SetText(bulletsLeft / bulletsPerTap + " / " + magazineSize / bulletsPerTap);

        //Reload Fill
        if(uiFill.enabled == true && time_remaining >= 0)
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

    private void MyInput()
    {

        if (allowButtonHold)
        {
            shooting = Input.GetKey(KeyCode.Mouse0);
        }
        else shooting = Input.GetKeyDown(KeyCode.Mouse0);

        if (Input.GetKey(KeyCode.Mouse0) && !reloading)
        {
            AudioSource click = GetComponent<AudioSource>();
            click.Play();

            uiFill.enabled = true;
        }

        //if (Input.GetKeyDown(KeyCode.R) && bulletsLeft < magazineSize && !reloading) Reload();
        if (bulletsLeft < magazineSize && !reloading) Reload();

        //Shoot
        if (readyToShoot && shooting && !reloading && bulletsLeft > 0)
        {
            bulletsShot = bulletsPerTap;
            Shoot();

        }
        
    }


    private void Shoot()
    {
        readyToShoot = false;

        //Spread
        float x = Random.Range(-spread, spread);
        float y = Random.Range(-spread, spread);

        //Calculate Direction with Spread
        Vector3 direction = fpsCam.transform.forward + new Vector3(x, y, 0);

        //RayCast
        if (Physics.Raycast(fpsCam.transform.position, direction, out rayHit, range, whatIsEnemy))
        {
            Debug.Log(rayHit.collider.name);

            if (rayHit.collider.CompareTag("Enemy"))
                rayHit.collider.GetComponent<Monster>().TakeDamage(damage);
        }

        //ShakeCamera
        //camShake.Shake(camShakeDuration, camShakeMagnitude);

        //Graphics
        Instantiate(bulletHoleGraphic, rayHit.point, Quaternion.Euler(0, 180, 0));
        Instantiate(muzzleFlash, attackPoint.position, Quaternion.identity);

        bulletsLeft--;
        bulletsShot--;

        Invoke("ResetShot", timeBetweenShooting);

        if (bulletsShot > 0 && bulletsLeft > 0)
            Invoke("Shoot", timeBetweenShots);
    }

    private void ResetShot()
    {
        readyToShoot = true;
    }

    private void Reload()
    {
        reloading = true;
        Invoke("ReloadFinished", reloadTime);
    }

    private void ReloadFinished()
    {
        bulletsLeft = magazineSize;
        reloading = false;
    }


}
