using System;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Utils
{
    [CreateAssetMenu(fileName = nameof(AssetsContext), menuName = "Strategy Game/" + nameof(AssetsContext), order = 0)]
    public sealed class AssetsContext : ScriptableObject
    {
        [SerializeField] private Object[] _objects;

        public Object GetObjectOfType(Type targetType, string targetName = null)
        {
            for (int i = 0; i < _objects.Length; i++)
            {
                var obj = _objects[i];
                if (obj.GetType().IsAssignableFrom(targetType))
                {
                    if (targetName == null || obj.name == targetName)
                    {
                        return obj;
                    }    
                }
            }
            return null;
        }
    }
}
