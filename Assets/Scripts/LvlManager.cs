using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class LvlManager : MonoBehaviour
{
    public int lvl;
    public static int agentsDestroyd;

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
        agentsDestroyd = 0;
        StartNewLvl();
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
        shopOpen = true;
        lvl++;
        if (lvl < 6)
        {
            callInterval -= callIntervalDecreas;
        }
        levelDuration += levelDurationIncerace;
        customersThisLvl += customerIncreas;
        ActivateStuff();

    }

    public void StartNewLvl()
    {
        lvlText.text = "Lvl " + lvl;
        LvlAtributes();
        StartCoroutine(CustomerSpawn());
    }
}
