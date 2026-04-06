using Newtonsoft.Json;
using UnityEngine;

public class Deserializer : MonoBehaviour
{
    private void Start()
    {
        TextAsset json = Resources.Load<TextAsset>("Database");
        if (json == null) {Debug.Log("Не нашел Database, парсер сначала надо включить"); return; }

        Database database = JsonConvert.DeserializeObject<Database>(json.text);

        foreach (var item in database.items) Debug.Log($"Название: {item.Name} | Описание: {item.Description}");
    } 
}
