using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SafePoint : MonoBehaviour
{
    private UIGameplayManager UIgameManager;
    // Start is called before the first frame update
    void Start()
    {
        UIgameManager=FindAnyObjectByType<UIGameplayManager>();
    }

    // Update is called once per frame
    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Player"))
        {
            other.gameObject.GetComponent<Player>().visible = false;
            UIgameManager.ActivatePanelSafePoint(true);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {

        if (other.CompareTag("Player"))
        {
            other.gameObject.GetComponent<Player>().visible = true;
            UIgameManager.ActivatePanelSafePoint(false);
        }
    }
}
