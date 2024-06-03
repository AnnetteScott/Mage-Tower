using System.IO;
using UnityEngine;
using System.Runtime.Serialization.Formatters.Binary;

public static class SaveSystem
{
    /// <summary>
    /// Loads the data from the SavedData class which gets it's data from the 
    /// GlobalData class. Save it as a binary file.
    /// </summary>
    public static void savePlayer()
    {
        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.persistentDataPath + "/player.mt";

        FileStream stream = new FileStream(path, FileMode.Create);

        SavedData data = new SavedData();

        formatter.Serialize(stream, data);
        stream.Close();
    }

    /// <summary>
    /// Reads the binary file and loads the data back into the GlobalData class.
    /// </summary>
    public static void loadPlayer()
    {
        string path = Application.persistentDataPath + "/player.mt";
        if(File.Exists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);

            SavedData data = (SavedData)formatter.Deserialize(stream);
            stream.Close();

            data.LoadData();
            
        }
        else
        {
            Debug.LogError("Save file not found in " + path);
        }
    }
}
