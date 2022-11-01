using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public GameObject dieEffect;

    public void Hit()
    {
        GameObject gameObject = Instantiate(dieEffect);
        gameObject.transform.position = transform.position;
        gameObject.transform.rotation = transform.rotation;
        gameObject.SetActive(true);
        Destroy(this.gameObject);
        Destroy(gameObject, 1f);
    }
    
   

}
