using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFade : MonoBehaviour
{
    [Header("Body materials")]
    [SerializeField] List<GameObject> body_mat = new List<GameObject>();
    [Header("Armor materials")]
    [SerializeField] List<GameObject> armor_mat = new List<GameObject>();
    [Header("eye materials")]
    [SerializeField] List<GameObject> eye_mat = new List<GameObject>();
    [Header("hair materials")]
    [SerializeField] List<GameObject> hair_mat = new List<GameObject>();
    [Header("Undies materials")]
    [SerializeField] List<GameObject> underwear_mat = new List<GameObject>();

    [Space(10)]
    [Header("Dissolved materials")]
    [Tooltip("0 - body, 1- armor, 2-eye, 3-hair, 4-undies")]
    [SerializeField] List<Material> new_Mats = new List<Material>();

    private float dissolveDuration = 2;
    private float dissolveStrenght = 0;

    public void DissolvePlayer()
    {
        ChangeMats(body_mat, 0);
        ChangeMats(armor_mat, 1);
        ChangeMats(eye_mat, 2);
        ChangeMats(hair_mat, 3);
        ChangeMats(underwear_mat, 4);
    }
    private void ChangeMats(List<GameObject> list_mat, int new_mats_id)
    {
        foreach (var mat in list_mat)
        {
            Renderer renderer = mat.GetComponent<Renderer>();
            if (renderer != null)
            {
                renderer.material = new_Mats[new_mats_id];
            }
            StartCoroutine(dissolver(renderer.material));
        }
    }
    private IEnumerator dissolver(Material dissolvedMaterial)
    {
        float elapsedTime = 0;
        Debug.Log("mat: " + dissolvedMaterial);
        while (elapsedTime < dissolveDuration)
        {
            elapsedTime += Time.deltaTime;
            dissolveStrenght = Mathf.Lerp(0, 1, elapsedTime / dissolveDuration);
            dissolvedMaterial.SetFloat("_Dissolve_Strength", dissolveStrenght);

            yield return null;
        }
    }
}
