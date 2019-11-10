using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class PlayerEscape : MonoBehaviour
{

    [SerializeField] TMP_Text displayText;

    [SerializeField] PlayerAttack pAttack;

    bool triggering = false;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine("disableStartText");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator disableStartText()
    {
        yield return new WaitForSeconds(5);
        if(!triggering)
            displayText.gameObject.SetActive(false);
    }

    private void OnTriggerEnter(Collider collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            triggering = true;
            displayText.gameObject.SetActive(true);

            if (GameHandler.PortalsDestroyed)
                displayText.text = "All Portals destroyed.  Attack to escape!";
            else
                displayText.text = GameHandler.PortalsLeft + " portals remain.  Destroy them and return here!";
        }
    }

    private void OnTriggerExit(Collider collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            triggering = false;
            displayText.gameObject.SetActive(false);
        }
    }

    private void OnTriggerStay(Collider collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            if (GameHandler.PortalsDestroyed && Input.GetKeyDown(pAttack.attackKey)){
                SceneManager.LoadScene("Game Winner Menu", LoadSceneMode.Single);
            }
        }
    }

}
