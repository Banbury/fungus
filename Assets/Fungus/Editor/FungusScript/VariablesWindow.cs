﻿using UnityEngine;
using UnityEditor;
using System;
using System.Collections;
using System.Collections.Generic;

namespace Fungus.Script
{

	public class VariablesWindow : EditorWindow 
	{
		Vector2 scrollPos = new Vector2();

		public void OnInspectorUpdate()
		{
			Repaint();
		}

		void OnGUI()
		{
			FungusScript fungusScript = FungusEditorWindow.GetFungusScript();

			if (fungusScript == null)
			{
				GUILayout.Label("No Fungus Script object selected");
				return;
			}

			bool showValues = Application.isPlaying;

			float columnWidth = (position.width - 40) / (showValues ? 3 : 2);

			scrollPos = GUILayout.BeginScrollView(scrollPos);

			GUILayout.BeginHorizontal();
			GUILayout.Label("Key", GUILayout.Width(columnWidth));
			GUILayout.Label("Type", GUILayout.Width(columnWidth));
			if (showValues)
			{
				GUILayout.Label("Value", GUILayout.Width(columnWidth));
			}
			GUILayout.EndHorizontal();

			GUIStyle boxStyle = new GUIStyle(GUI.skin.box);
			boxStyle.margin.left = 0;
			boxStyle.margin.right = 0;
			boxStyle.margin.top = 0;
			boxStyle.margin.bottom = 0;

			foreach (Variable variable in fungusScript.variables)
			{
				GUILayout.BeginHorizontal(boxStyle);

				string keyString = variable.key;
				string typeString = "";
				string valueString = "";

				switch (variable.type)
				{
				case VariableType.Boolean:
					typeString = "Boolean";
					valueString = variable.booleanValue ? "True" : "False";
					break;
				case VariableType.Integer:
					typeString = "Integer";
					valueString = variable.integerValue.ToString();
					break;
				case VariableType.Float:
					typeString = "Float";
					valueString = variable.floatValue.ToString();
					break;
				case VariableType.String:
					typeString = "String";
					if (variable.stringValue.Length == 0)
					{
						valueString = "\"\"";
					}
					else
					{
						valueString = variable.stringValue;
					}
					break;
				}

				GUILayout.Label(keyString, GUILayout.Width(columnWidth));
				GUILayout.Label(typeString, GUILayout.Width(columnWidth));
				if (showValues)
				{
					GUILayout.Label(valueString, GUILayout.Width(columnWidth));
				}

				GUILayout.EndHorizontal();
			}
			GUILayout.EndScrollView();
		}
	}

}