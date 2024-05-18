using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class LvlManager : MonoBehaviour
{
    public int lvl;
    public static int agentsDestroyd;
    public int ingridents = 1;
    public Button nextLvlButton;
    [SerializeField] UI_Shop uiShop;
    [SerializeField] Shoper shoper;
    public bool shopOpen;
    public TMP_Text lvlText;
    public TMP_Text ingridentsText;
    public TMP_Text goldText;
    public TMP_Text customerText;
    [SerializeField] float levelDuration = 50f;
    [SerializeField] float levelDurationIncerace = 10f;
    CustomerManager customerManager;
    public int customersThisLvl;
    [SerializeField] int customerIncreas = 1;
    public float callInterval = 5f;
    public float callIntervalDecreas = 1f;

    public float incomeIncrease = 1f;
    public float income = 100;

    private void Awake()
    {
        //shoper = FindAnyObjectByType<Shoper>();
    }

    void Start()
    {
        customerManager = FindAnyObjectByType<CustomerManager>();
        LvlAtributes();
        nextLvlButton.gameObject.SetActive(true);
        agentsDestroyd = 0;
     
        UpdateText();

        //Debug shop
        shoper.gold += 3000;
        ActivateStuff();
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
    public void GetGold(float time)
    {
        shoper.gold += (int)time;
        uiShop.UpdadtNumbers();
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
        if (agentsDestroyd >= customersThisLvl)
        {
            LevelComplete();
        }
    }

    IEnumerator CustomerSpawn()
    {
        int callsMade = 0;

        while (callsMade < customersThisLvl)
        {
            if(shoper.bellOn)
            {
                SoundManager.Instance.BellSound.Play();
            }
           
            customerManager.Spawn();
            callsMade++;
            yield return new WaitForSeconds(callInterval);
        }

        Debug.Log("All customers spawned!");
    }

    public void AgentDestroyed()
    {
        agentsDestroyd++;
        if (agentsDestroyd >= customersThisLvl)
        {
            LevelComplete();
            agentsDestroyd = 0;
        }
    }
    public void GiveGold(float time)
    {

        shoper.gold += (int)(income * time * incomeIncrease);
    }
    public void LvlAtributes()
    {
        if (lvl == 1)
        {
            customersThisLvl = 3;
            //unlock station 1
        }
        else if (lvl == 2)
        {
            shoper.gold += 10;
            //unlock station 2
        }
        else if (lvl == 3)
        {
            // other level attributes
        }
        Debug.Log(levelDuration);
        goldText.text = "Gold " + shoper.gold;
    }

    void LevelComplete()
    {
        Debug.Log("Level Complete!");
        ingridents -= customersThisLvl;
        ingridentsText.text = " " + ingridents;
        shopOpen = true;
        lvl++;
        if (lvl < 6)
        {
            callInterval -= callIntervalDecreas;
        }
        levelDuration += levelDurationIncerace;
        customersThisLvl += customerIncreas;
        ActivateStuff();
        UpdateText();

    }

    public void StartNewLvl()
    {
        if(ingridents <= 0)
        {
            // lose. You forgot to buy ingridents
            Debug.Log(" YOU LOST ");
            SceneManager.LoadSceneAsync(3);
        }
        ingridentsText.text = " "+ ingridents;
        lvlText.text = "Lvl " + lvl;
        LvlAtributes();
        StartCoroutine(CustomerSpawn());
    }

    public void UpdateText()
    {
        ingridentsText.text = " " + ingridents;
        lvlText.text = "Lvl " + lvl;
        customerText.text = "Costumers Expected " + customersThisLvl;
    }
}
