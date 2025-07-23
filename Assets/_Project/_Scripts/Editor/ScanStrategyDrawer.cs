﻿using System;
using System.Collections.Generic;
using System.Linq;
using Smash.Player.AbilityStrategies;
using UnityEditor;
using UnityEngine;

namespace Smash.Editor
{
	[CustomPropertyDrawer(typeof(ScanningStrategy), true)]
	public class ScanningStrategyDrawer : PropertyDrawer
	{
		private static Dictionary<string, Type> m_s_typeMap;
		public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            if (m_s_typeMap == null) BuildTypeMap();
            
            var typeRect = new Rect(position.x, position.y, position.width, EditorGUIUtility.singleLineHeight);
            var contentRect = new Rect(position.x, position.y + EditorGUIUtility.singleLineHeight, position.width, position.height - EditorGUIUtility.singleLineHeight);
            
            EditorGUI.BeginProperty(position, label, property);
            var typeName = property.managedReferenceFullTypename;
            var displayName = GetShortTypeName(typeName);

            if (EditorGUI.DropdownButton(typeRect, new GUIContent(displayName ?? "Select Strategy Type"), FocusType.Keyboard)) {
                var menu = new GenericMenu();
                if (m_s_typeMap == null || m_s_typeMap.Count == 0) {
                    menu.AddDisabledItem(new GUIContent("No Scanning Strategies available"));
                    menu.ShowAsContext();
                    return;
                }

                foreach (var kvp in m_s_typeMap) {
                    var name = kvp.Key;
                    var type = kvp.Value;
                    menu.AddItem(new GUIContent(name), type.FullName == typeName, () => {
                        property.managedReferenceValue = Activator.CreateInstance(type);
                        property.serializedObject.ApplyModifiedProperties();
                    });
                }
                menu.ShowAsContext();
            }

            if (property.managedReferenceValue != null) {
                EditorGUI.indentLevel++;
                EditorGUI.PropertyField(contentRect, property, GUIContent.none, true);
                EditorGUI.indentLevel--;
            }
            
            EditorGUI.EndProperty();
        }

        public override float GetPropertyHeight(SerializedProperty property, GUIContent label) 
        {
            return EditorGUI.GetPropertyHeight(property, label, true) + EditorGUIUtility.singleLineHeight;
        }

        static void BuildTypeMap() 
        {
            var baseType = typeof(ScanningStrategy);
            m_s_typeMap = AppDomain.CurrentDomain.GetAssemblies()
                .SelectMany(asm => {
                    try { return asm.GetTypes(); }
                    catch { return Type.EmptyTypes; }
                })
                .Where(t => !t.IsAbstract && baseType.IsAssignableFrom(t))
                .ToDictionary(t => ObjectNames.NicifyVariableName(t.Name), t => t);
        }

        static string GetShortTypeName(string fullTypeName) 
        {
            if (string.IsNullOrEmpty(fullTypeName)) return null;
            var parts = fullTypeName.Split(' ');
            return parts.Length > 1 ? parts[1].Split('.').Last() : fullTypeName;
        }
	}
}