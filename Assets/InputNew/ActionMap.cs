using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace UnityEngine.InputNew
{
	public class ActionMap
		: ScriptableObject
	{
		public List<InputAction> entries;
		public List<string> schemes;

		public void OnEnable()
		{
			if (entries != null)
			{
				for (var i = 0; i < entries.Count; ++ i)
				{
					entries[i].controlIndex = i;
				}
			}
		}

		public IEnumerable<Type> GetUsedDeviceTypes(int controlSchemeIndex)
		{
			if (entries == null)
				return Enumerable.Empty<Type>();

			var deviceTypes = new HashSet<Type>();
			foreach (var entry in entries)
			{
				if (controlSchemeIndex >= entry.bindings.Count)
					continue;

				var binding = entry.bindings[controlSchemeIndex];

				foreach (var source in binding.sources)
				{
					deviceTypes.Add(source.deviceType);
				}

				////TODO: button axes
			}

			return deviceTypes;
		}
	}
}