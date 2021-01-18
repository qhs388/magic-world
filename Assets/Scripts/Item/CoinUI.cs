using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CoinUI : MonoBehaviour
{

    public int startCoinQuantity;//初始的金币数量
    public GameObject coinQuantity;

    public static int currentCoinQuantity;//当前的金币数量

    // Start is called before the first frame update
    void Start()
    {
        currentCoinQuantity = startCoinQuantity;
    }

    // Update is called once per frame
    void Update()
    {
        coinQuantity.GetComponent<TMP_Text>().text = "x" + currentCoinQuantity;
    }
}
