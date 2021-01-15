using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Shop : MonoBehaviour
{
    [SerializeField] KnifeItem itemPrefab;
    [SerializeField] GameObject shopContainer;

    [Header("Text")]
    [SerializeField] Text counter;
    [SerializeField] Text price;

    

    [Header("Knives")]
    [SerializeField] Image knifeUnlocked;
    [SerializeField] Image knifeLocked;

    [SerializeField] MainManager mainManager;
    private List<KnifeItem> shopItems; 
    public Knife[] knifeArray;

    public KnifeItem KnifeItemSelected { get; set; }


    private KnifeItem SelectedItem
    {
        get { return shopItems.Find(x => x.IsSelected); }
    }

    private void Start()
    {
        SetUp();
    }
    private void Update()
    {
        price.text = SelectedItem.Price.ToString();
    }
    private void SetUp()
    {
        shopItems = new List<KnifeItem>();
        for (int i = 0; i < knifeArray.Length; i++)
        {
            KnifeItem item = Instantiate(itemPrefab, shopContainer.transform);
            item.SetUp(i, this);
            shopItems.Add(item);
        }
        shopItems[GameManager.Instance.SelectedKnife].OnClick();
    }

    public void UnlockKnife()
    {
        if (GameManager.Instance.TotalApple > SelectedItem.Price)
        {
            GameManager.Instance.TotalApple -= SelectedItem.Price;
            SelectedItem.IsUnlocked = true;
            SelectedItem.UpdateUI();
            GameManager.Instance.SelectedKnife = SelectedItem.Index;
            UpdateShopUI();
            SoundManager.Instance.PlayButton();
        }
    }
    public void UpdateShopUI()
    {
        knifeUnlocked.sprite = SelectedItem.KnifeImage.sprite;
        knifeLocked.sprite = SelectedItem.KnifeImage.sprite;

        knifeUnlocked.gameObject.SetActive(SelectedItem.IsUnlocked);
        knifeLocked.gameObject.SetActive(!SelectedItem.IsUnlocked);

        int itemsUnlocked = 0;
        itemsUnlocked = shopItems.FindAll(x => x.IsUnlocked).Count;


        counter.text = itemsUnlocked + "/" + knifeArray.Length;


        GameManager.Instance.SelectedKnifePrefab = knifeArray[GameManager.Instance.SelectedKnife];

        if (mainManager != null)
        {
            mainManager.SelectedKnife.sprite = GameManager.Instance.SelectedKnifePrefab.GetComponent<SpriteRenderer>().sprite;
        }
    }
}
