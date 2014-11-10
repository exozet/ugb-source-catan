using UnityEngine;
using System.Collections;

/// <summary>
/// A Text Mesh based text which is localized using the current language. 
/// </summary>
public class LocalizableText : GameComponent
{
	public string m_locaKey;
	LString m_translation;
	TextMesh m_textMesh;
	GUIText m_guiText;
	// Use this for initialization
	void Start ()
	{
		
		m_translation = m_locaKey;
		m_textMesh = this.GetComponent<TextMesh>();
		m_guiText = this.GetComponent<GUIText>();
		if(m_textMesh != null)
		{
			m_textMesh.text = m_translation;
		}
		
		if(m_guiText != null)
		{
			m_guiText.text = m_translation;
		}
	}
	
}

