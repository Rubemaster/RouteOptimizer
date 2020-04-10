using UnityEngine;

[System.Serializable]
public class LocationData
{
    public float lat;
    public float lon;

    public static LocationData CreateFromJSON(string jsonString)
    {
        return JsonUtility.FromJson<LocationData>(jsonString);
    }

    // Given JSON input:
    // {"name":"Dr Charles","lives":3,"health":0.8}
    // this example will return a PlayerInfo object with
    // name == "Dr Charles", lives == 3, and health == 0.8f.
}
