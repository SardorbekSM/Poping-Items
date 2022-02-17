using Data;
using UnityEngine;
using UnityEngine.Assertions;

namespace View
{
    public class PatternView : MonoBehaviour
    {
        public InteractableType PatternType { get; private set; }

        public void ChangePattern(GameObject pattern, InteractableType type)
        {
            /*var oldPattern = TryGetComponent();
            
            if (oldPattern != null)
            {
                Destroy(oldPattern.gameObject);
            }*/

            PatternType = type;
            pattern.transform.SetParent(gameObject.transform);
            pattern.transform.localPosition = Vector3.zero;
        }
    }
}
