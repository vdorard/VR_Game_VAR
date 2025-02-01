using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    public string requiredKeyID; // Identifiant unique de la clé nécessaire pour cette porte

    private bool isUnlocked = false;

    public bool Unlock(string keyID)
    {
        // Vérifie si la clé correspond
        if (keyID == requiredKeyID)
        {
            isUnlocked = true;
            Destroy(gameObject);
            return true;
        }
        return false;
    }
}
