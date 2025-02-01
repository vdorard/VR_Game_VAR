using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collisionscript : MonoBehaviour
{
   void PickUpObject(GameObject obj)
{
    Rigidbody rb = obj.GetComponent<Rigidbody>();
    if (rb != null)
    {
        rb.isKinematic = true; // Désactive les interactions physiques
    }
}
void DropObject(GameObject obj)
{
    Rigidbody rb = obj.GetComponent<Rigidbody>();
    if (rb != null)
    {
        rb.isKinematic = false; // Réactive les interactions physiques
    }
}

}
