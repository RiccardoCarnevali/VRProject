

using System;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using ItemType = Item.ItemType;

public class ItemCombineRecipes : MonoBehaviour
{
    [field: SerializeField] public Item Screwdriver {get; private set;}


    public static Dictionary<HashSet<ItemType>, Item> CanCombine;

    void Awake()
    {
        CanCombine = new();
        AddRecipe(ItemType.SCREWDRIVER_HANDLE, ItemType.SCREWDRIVER_TIP, Screwdriver);
    }

    private void AddRecipe(ItemType first, ItemType second, Item result)
    {
        HashSet<ItemType> combination = new(){first, second};
        CanCombine.Add(combination, result);
    }

    public static bool GetRecipeIfExists(ItemType first, ItemType second, out Item result)
    {
        HashSet<ItemType> combination = new(){first, second};
        result = CanCombine.FirstOrDefault(keyValuePair => keyValuePair.Key.SetEquals(combination)).Value;
        return result != null;
    }
}
