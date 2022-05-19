using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuUI : MonoBehaviour
{
    private GameObject controllerObj;
    private Controller controller;

    [SerializeField] private GameObject noSeedText;
    [SerializeField] private Transform inventoryContent;
    [SerializeField] private Transform itemPrefab;
    [SerializeField] private GameObject inventoryUI;
    [SerializeField] private GameObject uiIntro;
    [SerializeField] private GameObject shopUI;

    public void Start()
    {
        controllerObj = GameObject.Find("/Scripts/Controller");
        controller = controllerObj.GetComponent<Controller>();
    }

    public void ToggleInventory()
    {
        Inventory menuInventory = controller.data.inventory;

        if (inventoryUI.activeSelf)
        { // set to inactive and hide
            inventoryUI.SetActive(false);
        } else
        { // set to active and show
            inventoryUI.SetActive(true);
            foreach (Transform child in inventoryContent.transform)
            {
                GameObject.Destroy(child.gameObject);
            }
            if (menuInventory.GetCount() > 0)
            {
                // hide the "no seeds yet" text
                noSeedText.SetActive(false);
                for (int i = 0; i < menuInventory.GetCount(); i++)
                {
                    Seed seedData = controller.data.inventory.GetItem(i);
                    Transform prefab = Instantiate(itemPrefab, inventoryContent);

                    Transform itemImage = prefab.transform.GetChild(0);
                    Transform itemText = prefab.transform.GetChild(1);

                    itemText.GetComponent<TMPro.TextMeshProUGUI>().text = seedData.preDescription;
                    itemImage.GetComponent<Image>().sprite = Resources.Load<Sprite>(seedData.textureName + "-seed");
                }
            }
        }
    }

    public void ToggleShop()
    {
        if ( shopUI.activeSelf )
        {
            shopUI.SetActive(false);
        } else
        {
            shopUI.SetActive(true);
        }
    }

    public void CloseShop()
    {
        shopUI.SetActive(false);
    }

    public void CloseTutorial()
    {
        uiIntro.SetActive(false);
    }
}
