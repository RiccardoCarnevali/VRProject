using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

//Since Tuple is immutable and does not have a public constructor with no arguments it can't be deserialized so we use this helper class instead
[Serializable]
public class InventoryData
{
    public InventoryData() {
        Items = new();
    }

    public string SelectedItem { get; set; }
    public HashSet<string> Items { get; set; }
}

public class SaveSystem
{
    private static Dictionary<string, string> saveData = new();
    private static InventoryData inventoryData = new();

    private static string saveDataPath = Application.persistentDataPath + "/save.goku";
    private static string inventoryDataPath = Application.persistentDataPath + "/inventory.goku";

    public static void SetFlag(string key) {
        saveData[key] = true.ToString();
    }

    public static bool CheckFlag(string key) {
        return saveData.ContainsKey(key) && saveData[key] == true.ToString();
    }

    public static void SetFloat(string key, float value) {
        saveData[key] = value.ToString();
    }

    public static float GetFloat(string key) {
        if (!saveData.ContainsKey(key)) {
            return 0;
        }
        else {
            if (float.TryParse(saveData[key], out float value)) {
                return value;
            }
            else {
                //Someone probably tampered with the save file
                return 0;
            }
        }
    }

    public static void SetInt(string key, int value) {
        saveData[key] = value.ToString();
    }

    public static int GetInt(string key) {
        if (!saveData.ContainsKey(key)) {
            return 0;
        }
        else {
            if (Int32.TryParse(saveData[key], out int value)) {
                return value;
            }
            else {
                //Someone probably tampered with the save file
                return 0;
            }
        }
    }

    public static void AddInventoryItem(Item item) {
        inventoryData.Items.Add(item.Id);
    }

    public static void RemoveInventoryItem(Item item) {
        inventoryData.Items.Remove(item.Id);
    }

    public static List<string> GetInventoryItems() {
        return new List<string>(inventoryData.Items);
    }

    public static void SetSelectedInventoryItem(string itemId) {
        inventoryData.SelectedItem = itemId;
    }

    public static string GetSelectedInventoryItem() {
        return inventoryData.SelectedItem;
    }

    public static void Save() {
        Serialize(saveData, File.Open(saveDataPath, FileMode.Create));
        Serialize(inventoryData, File.Open(inventoryDataPath, FileMode.Create));
    }

    public static bool Load() {
        if (!File.Exists(saveDataPath) || !File.Exists(inventoryDataPath))
            return false;
        saveData = Deserialize<Dictionary<string, string>>(File.Open(saveDataPath, FileMode.Open));
        inventoryData = Deserialize<InventoryData>(File.Open(inventoryDataPath, FileMode.Open));
        return true;
    }

    public static void DeleteSave() {
        File.Delete(saveDataPath);
        File.Delete(inventoryDataPath);

        saveData = new();
        inventoryData = new();
    }

    public static void Serialize<Object>(Object dictionary, Stream stream)
    {
        try // Try to serialize the dictionary to a file
        {
            using (stream)
            {
                BinaryFormatter bin = new BinaryFormatter();
                bin.Serialize(stream, dictionary);
            }
        }
        catch (IOException)
        {
        }
    }

    public static Object Deserialize<Object>(Stream stream) where Object : new()
    {
        Object ret = CreateInstance<Object>();
        try
        {
            using (stream)
            {
                BinaryFormatter bin = new BinaryFormatter();
                ret = (Object)bin.Deserialize(stream);
            }
        }
        catch (IOException)
        {
        }
        return ret;
    }

    public static Object CreateInstance<Object>() where Object : new()
    {
        return (Object)Activator.CreateInstance(typeof(Object));
    }
}
