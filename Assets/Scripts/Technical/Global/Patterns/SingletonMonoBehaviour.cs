using System.Collections;
using System.Collections.Generic;

using UnityEngine;

namespace LTG.Technical
{
	public class SingletonMonoBehaviour<T> : MonoBehaviour where T : MonoBehaviour
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
					if (results.Length == 0 /*|| results.Length > 1*/)
					{
						Debugger.Throw(Debugger.ErrorType.SingletonError, typeof(T).ToString(), "Illegal singleton instance.");
						return null;
					}
					instance = results[0];
				}
				return instance;
			}
		}
	}
}
