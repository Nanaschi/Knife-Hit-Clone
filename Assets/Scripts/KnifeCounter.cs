using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class KnifeCounter : MonoBehaviour
{
    public static KnifeCounter Instance;
    [SerializeField] GameObject knifeSprite;
    [SerializeField] Color knifeReadyColor;
    [SerializeField] Color knifeWastedColor;

    private List<GameObject> icons = new List<GameObject>();



    private void Awake()
    {
        if (Instance)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }





    public void SetUpKnife(int amount)
    {
        foreach (var icon in icons)
        {
            Destroy(icon);
        }
        icons.Clear();


        for (int i = 0; i < amount; i++)
        {
            GameObject icon = Instantiate(knifeSprite, transform);
            icon.GetComponent<Image>().color = knifeReadyColor;
            icons.Add(icon);
        }
    }



    public void KnifeHit(int amount)
    {
        for (int i = 0; i < icons.Count; i++)
        {
            icons[i].GetComponent<Image>().color = i < amount ? knifeWastedColor : knifeReadyColor;
        }
    }
}
