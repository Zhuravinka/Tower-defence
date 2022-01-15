using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower : MonoBehaviour
{
    [SerializeField] Transform objectToPan;
    [SerializeField] Transform targetEnemy;
    // Start is called before the first frame update

    // Update is called once per frame
    void Update()
    {
        objectToPan.LookAt(targetEnemy);
    }
}
