using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntryPoints : MonoBehaviour
{
    // workaround because dictionaries are not serializable
    [SerializeField] private List<EntryPoint> entryPoints = new List<EntryPoint>()
    {
        new EntryPoint { name = "default", position = Vector2.zero }
    };

    public Vector2 GetEntryPointFrom(string entryPointName)
    {
        Debug.Log("Looking for entry point: " + entryPointName);
        Debug.Log("Defined entry points: " + entryPoints);
        foreach (EntryPoint e in entryPoints)
        {
            Debug.Log("Entry point: " + e.ToString());
        }
        foreach (EntryPoint entryPoint in entryPoints)
        {
            if (entryPoint.name == entryPointName)
            {
                Debug.Log("Found entry point: " + entryPoints);
                return entryPoint.position;
            }
        }

        return Vector2.zero;
    }

    [System.Serializable]
    private class EntryPoint
    {
        public string name;
        public Vector2 position;

        override public string ToString()
        {
            return "Source scene " + name + ": " + position.ToString();
        }
    }
}


