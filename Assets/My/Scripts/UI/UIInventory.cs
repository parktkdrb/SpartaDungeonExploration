using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIInventory : MonoBehaviour
{
    public ItemSlot[] slots;

    public GameObject inventoryWindow;

    public Transform slotPanel;
    public Transform dropPosition;
    [Header("Select Item")]
    public TextMeshProUGUI selectItemName;
    public TextMeshProUGUI selectItemDescription;
    public TextMeshProUGUI selectStatName;
    public TextMeshProUGUI selectStataValue;
    public GameObject useButton;
    public GameObject equipButton;
    public GameObject unEquipButton;
    public GameObject dropButton;

    private PlayerController controller;
    private PlayerCondition condition;

    ItemData selectedItem;
    int selectedItemIdex = 0;

    int curEquipIndex;

    // Start is called before the first frame update
    void Start()
    {
        controller = CharacterManager.Instance.Player.controller;
        condition = CharacterManager.Instance.Player.condition;
        dropPosition = CharacterManager.Instance.Player.dropPosition;
        controller.inventory += Toggle;
        CharacterManager.Instance.Player.addItem += AddItem;
        inventoryWindow.SetActive(false);
        slots = new ItemSlot[slotPanel.childCount];

        for (int i = 0; i < slots.Length; i++)
        {
            slots[i] = slotPanel.GetChild(i).GetComponent<ItemSlot>();
            slots[i].index = i;
            slots[i].inventory = this;
        }
        ClearSelectedItemWindow();
    }
    void ClearSelectedItemWindow()
    {
        selectItemName.text = string.Empty;
        selectItemDescription.text = string.Empty;
        selectStatName.text = string.Empty;
        selectStataValue.text = string.Empty;

        useButton.SetActive(false);
        equipButton.SetActive(false);
        unEquipButton.SetActive(false);
        dropButton.SetActive(false);
    }

    public void Toggle()
    {
        if (isOpen())
        {
            inventoryWindow.SetActive(false);
        }
        else
        {
            inventoryWindow.SetActive(true);
        }
    }
    public bool isOpen()
    {
        return inventoryWindow.activeInHierarchy;
    }
    void AddItem()
    {
        ItemData data = CharacterManager.Instance.Player.itemData;

        //아이템이 중복 가능한지? canStack
        if (data.canStack)
        {
            ItemSlot slot = GetItemSatck(data);
            if (slot != null)
            {
                slot.quantity++;
                UpdateUI();
                CharacterManager.Instance.Player.itemData = null;
                return;
            }
        }

        //비어있는 슬롯 가져온다
        ItemSlot emptyslot = GetEmptySlot();

        //있다면
        if (emptyslot != null)
        {
            emptyslot.item = data;
            emptyslot.quantity = 1;
            UpdateUI();
            CharacterManager.Instance.Player.itemData = null;
        }
        else
        {

            //없다면
            ThrowItem(data);
        }
        CharacterManager.Instance.Player.itemData = null;
    }
    void UpdateUI()
    {
        for (int i = 0; i < slots.Length; i++)
        {
            if (slots[i].item != null)
            {
                slots[i].Set();
            }
            else
            {
                slots[i].Clear();
            }
        }
    }
    ItemSlot GetItemSatck(ItemData data)
    {
        for (int i = 0; i < slots.Length; i++)
        {
            if (slots[i].item == data && slots[i].quantity < data.maxStackAmount)
            {
                return slots[i];
            }
        }
        return null;
    }

    ItemSlot GetEmptySlot()
    {
        for (int i = 0; i < slots.Length; i++)
        {
            if (slots[i].item == null)
            {
                return slots[i];
            }
        }
        return null;
    }

    void ThrowItem(ItemData data)
    {
        Instantiate(data.dropPrefab, dropPosition.position, Quaternion.Euler(Vector3.one * Random.value * 360));
    }

    public void SelectItem(int index)
    {
        if (slots[index].item == null) return;

        selectedItem = slots[index].item;
        selectedItemIdex = index;

        selectItemName.text = selectedItem.displayName;
        selectItemDescription.text = selectedItem.description;

        selectStatName.text = string.Empty;
        selectStataValue.text = string.Empty;

        for (int i = 0; i < selectedItem.consumables.Length; i++)
        {
            selectStatName.text += selectedItem.consumables[i].type.ToString() + "\n";
            selectStataValue.text += selectedItem.consumables[i].value.ToString() + "\n";
        }
        useButton.SetActive(selectedItem.type == ItemType.Consumable);
        equipButton.SetActive(selectedItem.type == ItemType.Equipable && !slots[index].equiped);
        unEquipButton.SetActive(selectedItem.type == ItemType.Equipable && slots[index].equiped);
        dropButton.SetActive(true);
    }

    public void OnUseButton()
    {
        if (selectedItem.type == ItemType.Consumable)
        {
            for (int i = 0; i < selectedItem.consumables.Length; i++)
            {
                switch (selectedItem.consumables[i].type)
                {
                    case ConsumableType.Health:
                        condition.Heal(selectedItem.consumables[i].value);
                        break;
                    case ConsumableType.Hunger:
                        condition.Eat(selectedItem.consumables[i].value);
                        break;
                }
            }
            RemoveSelectedItem();
        }
    }
    public void OnDropButton()
    {
        ThrowItem(selectedItem);
        RemoveSelectedItem();
    }
    void RemoveSelectedItem()
    {
        slots[selectedItemIdex].quantity--;

        if (slots[selectedItemIdex].quantity <= 0)
        {
            selectedItem = null;
            slots[selectedItemIdex].item = null;
            selectedItemIdex = -1;
            ClearSelectedItemWindow();
        }
        UpdateUI();
    }

    public void OnEquipButton()
    {
        if (slots[curEquipIndex].equiped)
        {
            UnEquip(curEquipIndex);
        }
        slots[selectedItemIdex].equiped = true;
        curEquipIndex = selectedItemIdex;
       // CharacterManager.Instance.Player.equip.EquipNew(selectedItem);
        UpdateUI();
        SelectItem(selectedItemIdex);
    }

    void UnEquip(int index)
    {
        slots[index].equiped = false;
        //CharacterManager.Instance.Player.equip.UnEquip();
        UpdateUI();

        if(selectedItemIdex == index)
        {
            SelectItem(selectedItemIdex);
        }
    }

    public void OnUnEquipButton()
    {
        UnEquip(selectedItemIdex);
    }
}

