using System;
using UnityEngine;

public class Readme : ScriptableObject {
	public Texture2D icon;
	public string title;
	public Section[] sections;
	public bool loadedLayout;
	
	[Serializable]
	public class Section {
		public string heading, text, linkText, url;
        public Texture2D image1;
        public Texture2D image2;

	}
}
