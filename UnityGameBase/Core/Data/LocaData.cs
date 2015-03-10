using System.Collections.Generic;
using System.Xml.Serialization;
using UnityEngine;
using System.IO;
using System;
using System.Text;
using UGB.Core.Utils;
using UGB.Core.Globalization;


namespace UGB.Core.Data
{
	
    /// <summary>
    /// Loca data. Contains loading code for localization files. Allows access to translated values. 
    /// </summary>
    public class LocaData
    {
		
        private LocaData()
        {
			
        }
		
        XmlLocaData mXmlData;
		
        public string GetText(string pKey)
        {
            if (pKey == null)
                return "Empty Key!";
		
            if (mXmlData.mData.ContainsKey(pKey))
                return mXmlData.mData[pKey];

			return "KNF:" + pKey;
            }
				
		public string[] GetKeys ()
		{
			string[] keys = new string[mXmlData.mData.Keys.Count];
			mXmlData.mData.Keys.CopyTo (keys, 0);
			return keys;
        }
		
	#if UNITY_EDITOR
        public void AddText(string pKey, string pText)
        {
            mXmlData.mData[pKey] = pText;
        }
	#endif
		
        public static LocaData Load()
        {
            if (Application.isPlaying)
            {
                return Load(Game.Instance.gameLoca.currentLanguage.ToString());
            }
            return null;
        }
		
		#if UNITY_EDITOR
        public static LocaData LoadFromEditor(string pLanguageShort)
        {
            LocaData lData = new LocaData();
            string path = Application.dataPath + "/Resources/loca/loca_" + pLanguageShort;
            try
            {
                FileInfo file = new FileInfo(path);
                if (!file.Exists)
                {
                    Debug.LogWarning("Localization: File not found: " + file.FullName);
                    return lData;
                }
				
                path = Path.GetFileNameWithoutExtension(path);
                path = "loca/" + path;
                TextAsset ta = Resources.Load(path) as TextAsset;
				
                MemoryStream ms = new MemoryStream(ta.bytes);
				
                XmlSerializer s = new XmlSerializer(typeof(XmlLocaData));
                XmlLocaData data = s.Deserialize(ms) as XmlLocaData;
				
                lData.mXmlData = data;
				
                lData.mXmlData.PostRead();
				
            }
            catch (Exception e)
            {
                Debug.LogWarning("Error loading loca file for requested language. " + e.Message);
                lData.mXmlData = new XmlLocaData();
                lData.mXmlData.mLanguage = pLanguageShort;
            }
			
            return lData;
        }
        #endif
        
        
        public static LocaData Load(string pLanguageShort)
        {
            LocaData lData = new LocaData();
            lData.mXmlData = new XmlLocaData();
            lData.mXmlData.mLanguage = pLanguageShort;
			
            string path = "loca/loca_" + pLanguageShort;
            try
            {
                // iOS related crash with connected Xcode (EXC_BAD_ACCESS) - check for file 'exists'
                TextAsset ta = Resources.Load<TextAsset>(path);
                if (ta == null)
                {
                    throw new FileNotFoundException("File not found: " + path);
                }

                MemoryStream ms = new MemoryStream(ta.bytes);
				
                XmlSerializer s = new XmlSerializer(typeof(XmlLocaData));
                XmlLocaData data = s.Deserialize(ms) as XmlLocaData;
				
                lData.mXmlData = data;
				
                lData.mXmlData.PostRead();
				
            }
            catch (Exception e)
            {
                Debug.LogWarning("Error loading loca file for requested language. " + e.Message);
				
            }
			
            return lData;
        }

	#if UNITY_EDITOR
        public void Save()
        {
            if (Application.isPlaying)
            {
                Debug.LogError("Not available at runtime!");
                return;
            }
			
            mXmlData.PreWrite();
            string path = "Assets/Resources/loca/"; 

            DirectoryInfo di = new DirectoryInfo(path);
            if (!di.Exists)
            {
                di.Create();
            }

            path += "loca_" + mXmlData.mLanguage + ".xml";
			
            Debug.Log("Writing Loca file to : " + path);
			
            XmlSerializer serializer = new XmlSerializer(typeof(XmlLocaData));
			
			
            TextWriter writer = new StreamWriter(path);
            serializer.Serialize(writer, mXmlData);
			
            writer.Flush();
            writer.Close();
			
        }
	#endif
		
		
    }

}