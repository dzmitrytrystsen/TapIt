using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    [SerializeField] private GameObject coinPrefab;

    [SerializeField] private bool isCoinDestried;
    private Vector2 startPosition;

    void FixedUpdate()
    {
        startPosition = transform.position;

        if (isCoinDestried)
        {
            StartCoroutine(WaitAndCreate());
        }
    }

    void OnTriggerEnter2D(Collider2D target)
    {
        if (target.gameObject.tag == "Player")
        {
            gameObject.GetComponent<SpriteRenderer>().enabled = false;
            gameObject.GetComponent<Collider2D>().enabled = false;
            isCoinDestried = true;
        }
    }

    IEnumerator WaitAndCreate()
    {
        yield return new WaitForSeconds(3f);

        gameObject.GetComponent<SpriteRenderer>().enabled = true;
        gameObject.GetComponent<Collider2D>().enabled = true;
        isCoinDestried = false;
    }
}
