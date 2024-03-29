﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using FluentAssertions;
using FluentAssertions.Common;
using FluentAssertions.Types;
using Wally.RomMaster.FileService.WebApi;

namespace Wally.RomMaster.FileService.Tests.ConventionTests.Helpers;

public static class TypeHelpers
{
	private static readonly List<string> _prefixes = new()
	{
		"Wally.RomMaster.FileService",
	};

	public static IEnumerable<Assembly> GetAllInternalAssemblies()
	{
		var assemblies = typeof(Startup).Assembly.GetReferencedAssemblies()
			.Where(a => _prefixes.Exists(b => a.FullName.StartsWith(b)));

		foreach (var assembly in assemblies)
		{
			yield return Assembly.Load(assembly);
		}

		yield return typeof(Startup).Assembly;
	}

	public static void HaveOnlyPrivateParameterlessConstructor(this TypeSelectorAssertions typeSelectorAssertions)
	{
		const string because = "entity '{0}' should have private parameterless constructor only.";

		foreach (var type in typeSelectorAssertions.Subject)
		{
			var constructors = type.GetConstructors();
			foreach (var constructor in constructors)
			{
				constructor.Should()
					.HaveAccessModifier(CSharpAccessModifier.Private, because, type);
				constructor.GetParameters()
					.Should()
					.BeEmpty(because, type);
			}

			type.Should()
				.HaveDefaultConstructor(because, type);
		}
	}

	public static Type GetTypeDefinitionIfGeneric(this Type type)
	{
		return type.IsGenericType ? type.GetGenericTypeDefinition() : type;
	}

	public static bool IsGenericType(this Type type, Type genericType)
	{
		return type.IsGenericType && type.GetGenericTypeDefinition() == genericType;
	}

	public static Type? GetGenericInterface(this Type type, Type genericInterface)
	{
		return type.IsGenericType(genericInterface)
			? type
			: type.GetInterfaces()
				.FirstOrDefault(t => t.IsGenericType(genericInterface));
	}

	public static bool ImplementsInterface(this Type type, Type interfaceType)
	{
		if (!interfaceType.IsInterface)
		{
			throw new ArgumentException($"Parameter '{nameof(interfaceType)}' is not an Interface");
		}

		foreach (var @interface in type.GetTypeInfo()
					.ImplementedInterfaces)
		{
			if (@interface == interfaceType)
			{
				return true;
			}
		}

		return false;
	}

	public static bool ImplementsGenericInterface(this Type type, Type interfaceType)
	{
		if (!interfaceType.IsInterface)
		{
			throw new ArgumentException($"Parameter '{nameof(interfaceType)}' is not an Interface");
		}

		if (type.IsGenericType(interfaceType))
		{
			return true;
		}

		foreach (var @interface in type.GetTypeInfo()
					.ImplementedInterfaces)
		{
			if (@interface.IsGenericType(interfaceType))
			{
				return true;
			}
		}

		return false;
	}

	public static bool InheritsGenericClass(this Type type, Type classType)
	{
		if (!classType.IsClass)
		{
			throw new ArgumentException($"Parameter '{nameof(classType)}' is not a Class");
		}

		while (type != null && type != typeof(object))
		{
			var current = type.IsGenericType ? type.GetGenericTypeDefinition() : type;
			if (classType == current)
			{
				return true;
			}

			if (type.BaseType == null)
			{
				break;
			}

			type = type.BaseType;
		}

		return false;
	}

	public static TypeSelector GetAllTypes(this IEnumerable<Assembly> assemblies)
	{
		return assemblies.SelectMany(a => a.GetTypes())
			.Types();
	}

	public static TypeSelector GetAllExportedTypes(this IEnumerable<Assembly> assemblies)
	{
		return assemblies.SelectMany(a => a.GetExportedTypes())
			.Types();
	}

	/// <summary>
	///     Determines if this property is marked as init-only.
	/// </summary>
	/// <param name="property">The property.</param>
	/// <returns>True if the property is init-only, false otherwise.</returns>
	public static bool IsInitOnly(this PropertyInfo property)
	{
		if (!property.CanWrite)
		{
			return false;
		}

		var setMethod = property.SetMethod;
		if (setMethod == null)
		{
			return true;
		}

		// Get the modifiers applied to the return parameter.
		var setMethodReturnParameterModifiers = setMethod.ReturnParameter.GetRequiredCustomModifiers();

		// Init-only properties are marked with the IsExternalInit type.
		return setMethodReturnParameterModifiers.Contains(typeof(IsExternalInit));
	}
}
