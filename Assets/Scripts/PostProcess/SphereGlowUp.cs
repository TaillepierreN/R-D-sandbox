using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SphereGlowUp : MonoBehaviour
{
    [SerializeField] GameObject GM;
    [SerializeField] GameObject camera1;
    [SerializeField] GameObject camera2;
    private AudioSource audioSource;

    private void Awake() {
        audioSource = GetComponent<AudioSource>();
    }

    private void OnTriggerEnter(Collider other) {
        if (other.gameObject.name == "Player")
        {
            GM.GetComponent<postProcessBehavior>().GlowUp();
            StartCoroutine(cameraTricks());
        }
    }
    IEnumerator  cameraTricks()
    {
        audioSource.Play();
        camera1.SetActive(false);
        camera2.SetActive(true);
        yield return new WaitForSeconds(5f);
        camera1.SetActive(true);
        camera2.SetActive(false);
        Destroy(gameObject);
    }
}
