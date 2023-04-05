using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CoinCollector : MonoBehaviour
{
    [SerializeField] private TMP_Text _coinNumber;

    private float _coins = 0;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Coin")
        {
            _coins++;
            _coinNumber.text = _coins.ToString();
            Debug.Log(_coins);
            Destroy(collision.gameObject);
        }
    }
}
