using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class weaponPickup : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter(Collider other) {
        Debug.Log("hello" + other.name);
        if (other.name == "Player")
        {
            other.gameObject.GetComponent<weaponEquipped>().equippedWeapon();
            Destroy(this.gameObject);
        }
    }
}
