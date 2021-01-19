using DebuggingTools.Runtime;
using UnityEngine;

public class DebugTester : MonoBehaviour
{
    private CustomClass customClass = new CustomClass();
    
    private void Start()
    {
        ModifyCustomClass();
        DebugCustomClass();
    }

    private void ModifyCustomClass()
    {
        customClass.Name = "Insert name here.";
        customClass.ImportantValue = 123;
    }

    private void DebugCustomClass()
    {
        Debugger.LogWarning("Testing debug.", customClass);
    }
}
