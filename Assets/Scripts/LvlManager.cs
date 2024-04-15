using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;


//public enum Lvl
//{
//    one,
//    two, three, four, five, six, seven
//}

public class LvlManager : MonoBehaviour
{
    public int lvl;

    


    public float levelDuration = 60f;

    ShopSystem shopSystem;
    private void Awake()
    {
        shopSystem = GetComponent<ShopSystem>();
    }

    void Start()
    {
        StartCoroutine(LevelTimerCoroutine());
    }


    private void Update()
    {
        if(shopSystem.shopping)
        {
            return;
        }

        if( lvl == 0 )
        {
            
        }
        else if(lvl == 1)
        {
            //unlock station 1'
            // more custmers
        }
        else if (lvl == 2)
        {
            //unlock station 2
        }
    }
    IEnumerator LevelTimerCoroutine()
    {
        yield return new WaitForSeconds(levelDuration);

        LevelComplete();
    }

    void LevelComplete()
    {
        Debug.Log("Level Complete!");
        shopSystem.shopping = true;
        lvl++;
        shopSystem.GainGold(lvl * 10);
        // cut scnen
    }

    public void StartNewLvl()
    {
        StartCoroutine(LevelTimerCoroutine());
    }
}
