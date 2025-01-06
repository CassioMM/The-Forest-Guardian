using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


[System.Serializable]
public class InventoryController : MonoBehaviour
{
    public Objects[] slots;
    public Image[] slotImage;
    public int[] slotAmount;

    public int Total = 0;

    public InterfaceController iController;

    public AudioSource Coleta;
    public float ColetaV = 0.539f;

    //public LevelLoader LL;

    //private Documento doc;

    void Start()
    {
        iController = FindObjectOfType<InterfaceController>();
    }


    void Update()
    {
        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2));
        if (Physics.Raycast(ray, out hit, 5f))
        {
            if (hit.collider.tag == "Object")
            {
                iController.itemText.text = "Press (E) to collect the " + hit.transform.GetComponent<ObjectsType>().objecType.itemName;

                if (Input.GetKeyDown(KeyCode.E))
                {
                    for (int i = 0; i < slots.Length; i++)
                    {
                        if (slots[i] == null || slots[i].name == hit.transform.GetComponent<ObjectsType>().objecType.name)
                        {
                            slots[i] = hit.transform.GetComponent<ObjectsType>().objecType;
                            slotAmount[i]++;
                            slotImage[i].sprite = slots[i].itemSprite;
                            Total += 1;

                            Coleta.Play();
                            Coleta.volume = ColetaV;

                            if (Total == 6)
                            {
                                StartCoroutine(LoadLevelTransition(SceneManager.GetActiveScene().buildIndex + 1));
                            }

                            Destroy(hit.transform.gameObject);
                            break;
                        }
                    }
                }
            }
            else if (hit.collider.tag != "Object")
            {
                iController.itemText.text = null;
            }
        }
    }


    IEnumerator LoadLevelTransition(int levelIndex)
    {
        yield return null;

        SceneManager.LoadScene(levelIndex);


    }

    public void InventV(float value)
    {
        ColetaV = value;

    }


}
