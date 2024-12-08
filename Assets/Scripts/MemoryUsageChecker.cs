using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MemoryUsageChecker : MonoBehaviour
{
    void Update()
    {
        long memoryUsage = System.GC.GetTotalMemory(false);
        Debug.Log("Memory Usage: " + memoryUsage + " bytes");
    }
}