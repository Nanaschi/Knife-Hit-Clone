using NUnit.Framework;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class KnifeItem : MonoBehaviour
{
    [Header("Images")]
    [SerializeField] Image backgroundImage;
    [SerializeField] Image knifeImage;

    [SerializeField] GameObject selectedImage;

    [Header("Colors")]
    [SerializeField] Color knifeUnlockColor;
    [SerializeField] Color knifeLockColor;
    [SerializeField] Color knifeUnlockBackgroundColor;
    [SerializeField] Color knifeLockBackgroundColor;

    [SerializeField] int price;

    private Shop shop;
    private Knife knife;

    private const string KNIFE_UNLOCKED = "KnifeUnlocked_";

    public int Index { get; set; }

    public bool IsUnlocked
    {
        get
        {
            if (Index == 0)
            {
                return true;
            }
            return PlayerPrefs.GetInt(KNIFE_UNLOCKED + Index, defaultValue: 0) == 1;
        }
        set
        {
            PlayerPrefs.SetInt(KNIFE_UNLOCKED + Index, value ? 1 : 0);
        }
        
    }
    public bool IsSelected 
    {
        get => selectedImage.activeSelf;
        set
        {
            if (value)
            {
                if(shop.KnifeItemSelected != null)
                {
                    shop.KnifeItemSelected.IsSelected = false;
                }
                shop.KnifeItemSelected = this;
            }
            selectedImage.SetActive(value);

        }
    }

    public int Price { get => price; }
    public Image KnifeImage { get => knifeImage; }

    public void SetUp(int index, Shop shop)
    {
        this.shop = shop;
        Index = index;
        knife = this.shop.knifeArray[Index];
        KnifeImage.sprite = knife.GetComponent<SpriteRenderer>().sprite;
        UpdateUI();

    }



    public void OnClick()
    {
        if (IsUnlocked && IsSelected)
        {
            GeneralUI.Instance.CloseShop();
        }
        if (!IsSelected)
        {
            IsSelected = true;
        }
        if (IsUnlocked)
        {
            GameManager.Instance.SelectedKnife = Index;
        }
        shop.UpdateShopUI();
    }

    public void UpdateUI()
    {
        backgroundImage.color = IsUnlocked ? knifeUnlockBackgroundColor : knifeLockBackgroundColor;
        KnifeImage.GetComponent<Mask>().enabled = !IsUnlocked;

        KnifeImage.transform.GetChild(0).GetComponent<Image>().color = IsUnlocked ? knifeUnlockColor : knifeLockColor;
        KnifeImage.transform.GetChild(0).gameObject.SetActive(!IsUnlocked);

    }


}
