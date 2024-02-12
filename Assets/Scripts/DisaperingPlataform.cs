using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisaperingPlataform : MonoBehaviour
{
    public GameObject platform;
    public float fadeOutTime = 1;
    public float fadeInTime = 2;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            platform.GetComponent<Animator>().SetBool("FadeOut", true);
            StartCoroutine(FadeOut());
            StartCoroutine(FadeIn());
        }

        IEnumerator FadeOut()
        {
            yield return new WaitForSeconds(fadeOutTime);
            platform.SetActive(false);
        }

        IEnumerator FadeIn()
        {
            yield return new WaitForSeconds(fadeInTime);
            platform.SetActive(true);
        }
    }

}
