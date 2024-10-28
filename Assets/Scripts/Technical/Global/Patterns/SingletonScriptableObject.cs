using System.Collections;
using System.Collections.Generic;

using UnityEngine;

namespace LTG.Technical
{
	public abstract class SingletonScriptableObject<T> : ScriptableObject where T : ScriptableObject
	{
		//==================================
		//=========CLASS
		//==================================

		//Singleton
		private static T instance = null;
		public static T Instance
		{
			get
			{
				if (instance == null)
				{
					T[] results = Resources.FindObjectsOfTypeAll<T>();
					if (results.Length == 0 || results.Length > 1)
					{
						Debugger.Throw(Debugger.ErrorType.SingletonError, typeof(T).ToString(), "Illegal singleton instance.");						
						return null;
					}
					instance = results[0];
					instance.hideFlags = HideFlags.DontUnloadUnusedAsset;
				}
				return instance;
			}
		}
	}
}
