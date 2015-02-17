﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace UGB.XUI
{
/// <summary>
/// this class save create all widgetData and keep them in the prefab
/// </summary>
	[System.Serializable]
	public class WidgetManager : MonoBehaviour
	{
		[SerializeField,HideInInspector]
		public List<WidgetData>
			widgetContainer = new List<WidgetData>();
		
							
		/// <summary>
		/// Gets the widget by layer and name
		/// </summary>
		/// <returns>The widget.</returns>
		/// <param name="_layer">_layer.</param>
		/// <param name="_name">_name.</param>
		public WidgetData GetWidget(string _layer, string _name)
		{
			foreach (WidgetData widgetData in widgetContainer)
			{
				if (widgetData.layerName == _layer && widgetData.widgetName == _name)
					return widgetData;
			}
				
			//Debug.Log("Widget with name: " + _name + " dont exists in layer: " + _layer);
			return null;			
		}
		
		/// <summary>
		/// Create a collection with all IWidget Gameobjects
		/// </summary>
		public void CreateCollection()
		{		
			
			RectTransform[] transForms = this.gameObject.GetComponentsInChildren<RectTransform>();
			
			widgetContainer.Clear();
			
			foreach (RectTransform t in transForms)
			{
				//MAYBE TODO: getall Monobehaviour and compare for iwidget
				if ((IWidget)t.GetComponent(typeof(IWidget)) != null)
				{
					WidgetData widgetData = new WidgetData(t.parent.name, t.name, t);
				
					
					if (this.ExistLayerAndName(t.parent.name, t.name))		
						Debug.LogError("Widget with name: " + t.name + " already exists in layer: " + t.parent.name);
					else
					{
						widgetContainer.Add(widgetData);					
						Debug.Log("Add Widget: " + widgetData.layerName + ", " + widgetData.widgetName);
					}
				}
			}				
		}
		
		public bool ExistLayerAndName(string _layer, string _name)
		{
			foreach (WidgetData widgetData in widgetContainer)
			{
				if (widgetData.layerName == _layer && widgetData.widgetName == _name)
					return true;
			}
			return false;
		}
	}
}
