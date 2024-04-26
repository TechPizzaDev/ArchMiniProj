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
    [SerializeField] float levelDuration = 5f;

  
    private void Awake()
    {
        
        //shoper = FindAnyObjectByType<Shoper>();
    }

    void Start()
    {
        LvlAtributes();
        nextLvlButton.gameObject.SetActive(false);
        StartCoroutine(LevelTimerCoroutine());
      
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

    public void LvlAtributes()
    {
 
        if (lvl == 1)
        {
            //unlock station 1'
            // more custmers
        }
        else if (lvl == 2)
        {
            levelDuration = 10f;
            shoper.gold += 10;
            //unlock station 2
        }
        else if (lvl == 3)
        {
            levelDuration = 15f;
            //unlock station 2
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
        ActivateStuff();
        // cut scnen
    }

    public void StartNewLvl()
    {
        lvlText.text = " Lvl " + lvl;
        LvlAtributes();
        StartCoroutine(LevelTimerCoroutine());
    }
}
