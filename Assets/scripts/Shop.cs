using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Shop : MonoBehaviour
{
    public string objectName;
    public int price, acces,level1;
    public GameObject block;
    public Text objectPrice,CoinCount;
    void Awake()
    {
        AccesUpdate ();
    }
     
    void AccesUpdate()
    {
        acces = PlayerPrefs.GetInt(objectName + "Access");
        objectPrice.text = price.ToString();
        CoinCount.text = PlayerPrefs.GetInt("coins").ToString();

        if (acces == 1)
        {
            block.SetActive(false);
            objectPrice.gameObject.SetActive(false);
        }
    }

   
   public void OnButtonDown()
    {
        int coins = PlayerPrefs.GetInt("coins");
        if(acces == 0)
        {
            if (coins >= price)
            {
                PlayerPrefs.SetInt(objectName + "Access", 1);
                PlayerPrefs.SetInt("coins", coins - price);
                AccesUpdate();
            }
        }
        else
        {
            SceneManager.LoadScene(level1);
        }
    }
}
