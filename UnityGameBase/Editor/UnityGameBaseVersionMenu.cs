using UnityEngine;
using System.Collections;
using UnityEditor;
using UGB.Core.Utils;
namespace UGB.Core.Editor
{
	public class UnityGameBaseVersionMenu
	{
		
		[MenuItem("UGB/Unity Game Base Version " + UnityGameBaseVersion.Version,false, int.MaxValue)]
		public static void Version()
		{
			
		}
		
		[MenuItem("UBG/Unity Game Base Version " + UnityGameBaseVersion.Version, true)]
		public static bool ValidateVersion()
		{
			return false;
		}

	}
}