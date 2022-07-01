using System;
using System.Reflection;

namespace Utils
{
    public static class AssetsInjector
    {
        private static readonly Type _injectAssetAttributeType = typeof(InjectAssetAttribute);

        public static T Inject<T>(this AssetsContext context, T target, Type type)
        {
            if (type.BaseType != null) target = Inject(context, target, type.BaseType);

            var allFields = type.GetFields(BindingFlags.NonPublic
                                           | BindingFlags.Public
                                           | BindingFlags.Instance);

            for (int i = 0; i < allFields.Length; i++)
            {
                var fieldInfo = allFields[i];
                var injectAssetAttribute = fieldInfo.GetCustomAttribute(_injectAssetAttributeType) as InjectAssetAttribute;
                if (injectAssetAttribute == null)
                {
                    continue;
                }
                var objectToInject = context.GetObjectOfType(fieldInfo.FieldType, injectAssetAttribute.AssetName);
                fieldInfo.SetValue(target, objectToInject);
            }

            return target;
        }

        public static T Inject<T>(this AssetsContext context, T target)
        {
            var targetType = target.GetType();
            return Inject(context, target, targetType);
        }
    }
}