using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{

    [SerializeField] int health = 10;
    [SerializeField] int healthDecrease = 2;

    // Update is called once per frame
    private void OnTriggerEnter(Collider other)
    {
        health -= healthDecrease;
    }
}
