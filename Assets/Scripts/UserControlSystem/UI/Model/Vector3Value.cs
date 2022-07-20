using UnityEngine;
using Utils;

namespace UserControlSystem
{
    [CreateAssetMenu(fileName = nameof(Vector3Value), menuName = "Strategy Game/" + nameof(Vector3Value), order = 0)]
    public sealed class Vector3Value : StatelessScriptableObjectValueBase<Vector3>
    { 
        
    }
}