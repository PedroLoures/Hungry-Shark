using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GettingEaten : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log(other.tag);
        if (other.tag == "Player")
        {
            GameManager.instance.KillFish(gameObject);
        }
    }
}
