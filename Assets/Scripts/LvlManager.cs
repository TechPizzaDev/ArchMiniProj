using System.Collections;
using System.Collections.Generic;
using System.Xml;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class LvlManager : MonoBehaviour
{
    public int lvl;

    public Button nextLvlButton;
    [SerializeField] UI_Shop uiShop;
    [SerializeField] Shoper shoper;
    public bool shopOpen;
    public TMP_Text lvlText;
    public TMP_Text goldText;
    [SerializeField] float levelDuration = 50f;
    [SerializeField] float levelDurationIncerace = 10f;
    CustomerManager customerManager;
    [SerializeField] int customersThisLvl;
    [SerializeField] int customerIncreas = 1;
    public float callInterval = 5f;
    public float callIntervalDecreas = 1f;


    private void Awake()
    {
        
        //shoper = FindAnyObjectByType<Shoper>();
    }

    void Start()
    {
        customerManager = FindAnyObjectByType<CustomerManager>();
        LvlAtributes();
        nextLvlButton.gameObject.SetActive(true);
        //StartCoroutine(LevelTimerCoroutine());
      
    }

    void ActivateStuff()
    {
        IShopCustomer shopCustomer = shoper.GetComponent<IShopCustomer>();
        nextLvlButton.gameObject.SetActive(true);
        if (shopCustomer != null)
        {
            uiShop.Show(shopCustomer);
        }
    }
    public void OpneShop()
    {
        nextLvlButton.gameObject.SetActive(false);
        uiShop.Hide();
        shopOpen = false;
        StartNewLvl();
    }
    private void Update()
    {
        
       
    }
  
    IEnumerator CustomerSpawn()
    {
        int callsMade = 0;
        float elapsedTime = 0f;

        while (callsMade < customersThisLvl && elapsedTime < levelDuration)
        {
            
            customerManager.Spawn();

            callsMade++;
            yield return new WaitForSeconds(callInterval);
            elapsedTime += callInterval;
        }

        Debug.Log("all customer spawnd!");
    }
    public void LvlAtributes()
    {
 
        if (lvl == 1)
        {
            customersThisLvl = 3;
            //unlock station 1'
            // more custmers
        }
        else if (lvl == 2)
        {
            
            shoper.gold += 10;
            //unlock station 2
        }
        else if (lvl == 3)
        {
           

        }
        Debug.Log(levelDuration);
        goldText.text = "Gold " + shoper.gold;
    }
    IEnumerator LevelTimerCoroutine()
    {
        yield return new WaitForSeconds(levelDuration);

        LevelComplete();
    }

    void LevelComplete()
    {
        Debug.Log("Level Complete!");
        shopOpen = true;
        lvl++;
        if(lvl < 6)
        {
            callInterval -= callIntervalDecreas;
        }
        levelDuration += levelDurationIncerace;
        customersThisLvl += customerIncreas;
        ActivateStuff();
        // cut scnen
    }

    public void StartNewLvl()
    {
        lvlText.text = " Lvl " + lvl;
        LvlAtributes();
        StartCoroutine(CustomerSpawn());
        StartCoroutine(LevelTimerCoroutine());
    }
}
