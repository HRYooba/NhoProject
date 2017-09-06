using System;
using UnityEngine;
using UnityEditor;

namespace CustomAttribute {
    [AttributeUsage(AttributeTargets.Field, Inherited = true, AllowMultiple = false)]
    public sealed class RangeExAttribute : PropertyAttribute
    {
        public readonly float min;
        public readonly float max;

        public RangeExAttribute(float min, float max)
        {
            this.min = min;
            this.max = max;
        }
    }

    [CustomPropertyDrawer(typeof(RangeExAttribute))]
    internal sealed class RangeExDrawer : PropertyDrawer
    {
        private float value;

        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            var rangeAttribute = (RangeExAttribute)base.attribute;

            if (property.propertyType == SerializedPropertyType.Float)
            {
                value = EditorGUI.Slider(position, label, value, rangeAttribute.min, rangeAttribute.max);
                value = Mathf.Floor(value * 10.0f) * 0.1f;
                property.floatValue = value;
            }
            else
            {
                EditorGUI.LabelField(position, label.text, "Use Range with float or int.");
            }
        }
    }
}
