using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using ItemType = Item.ItemType;

public class ItemCombineRecipes : MonoBehaviour
{
    [SerializeField] private Item screwdriver;
    [SerializeField] private Item scissors;

    public static Dictionary<HashSet<ItemType>, Item> CanCombine;

    void Awake()
    {
        CanCombine = new();
        AddRecipe(ItemType.SCREWDRIVER_HANDLE, ItemType.SCREWDRIVER_TIP, screwdriver);
        AddRecipe(ItemType.SCISSORS_INDEX_HALF, ItemType.SCISSORS_THUMB_HALF, scissors);
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
