using UnityEngine;

namespace Temp
{
    public class DebugLogger : MonoBehaviour
    {
        public void LogText(string str = "Action is working!")
        {
            Debug.Log(str);
        }
    }
}
