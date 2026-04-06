using UnityEngine;

public class Parser : MonoBehaviour
{
    private void Awake()
    {
        Utils.ParseCSV("data-csv");
    }
}
