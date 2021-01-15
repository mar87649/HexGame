using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using UnityEditor;

namespace Ludiq.Fixes
{
	public static class SerializedPropertyProviderProviderExtensions
	{
		public static void GenerateProviderScriptsFixed(this SerializedPropertyProviderProvider instance)
		{
			if (Directory.Exists(LudiqCore.Paths.propertyProviders))
			{
				foreach (var file in Directory.GetFiles(LudiqCore.Paths.propertyProviders))
				{
					File.Delete(file);
				}
			}

			if (Directory.Exists(LudiqCore.Paths.propertyProvidersEditor))
			{
				foreach (var file in Directory.GetFiles(LudiqCore.Paths.propertyProvidersEditor))
				{
					File.Delete(file);
				}
			}

			PathUtility.CreateDirectoryIfNeeded(LudiqCore.Paths.propertyProviders);
			PathUtility.CreateDirectoryIfNeeded(LudiqCore.Paths.propertyProvidersEditor);

			Type typeOfThis = typeof(SerializedPropertyProviderProvider);
			IEnumerable<Type> typeset = typeOfThis.GetProperty("typeset", BindingFlags.NonPublic | BindingFlags.Instance).GetValue(instance) as IEnumerable<Type>;
			Func<Type, string> getProviderScriptName = type => (string) typeOfThis.GetMethod("GetProviderScriptName", BindingFlags.NonPublic | BindingFlags.Static).Invoke(instance, new object[] { type });
			Func<Type, string> generateProviderSource = type => (string) typeOfThis.GetMethod("GenerateProviderSource", BindingFlags.NonPublic | BindingFlags.Static).Invoke(instance, new object[] { type });

			foreach (var type in typeset.Where(SerializedPropertyUtility.HasCustomDrawer))
			{ 
				var directory = Codebase.IsEditorType(type) ? LudiqCore.Paths.propertyProvidersEditor : LudiqCore.Paths.propertyProviders;
				var path = Path.Combine(directory, getProviderScriptName(type) + ".cs");
				
				VersionControlUtility.Unlock(path);
				File.WriteAllText(path, generateProviderSource(type));
			}

			AssetDatabase.Refresh();
		}
	}
}