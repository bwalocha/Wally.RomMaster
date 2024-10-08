﻿using System;
using AutoMapper;
using FluentAssertions;
using Wally.RomMaster.WolneLekturyService.Application.Contracts.Users.Requests;
using Wally.RomMaster.WolneLekturyService.Application.Contracts.Users.Responses;
using Wally.RomMaster.WolneLekturyService.Application.MapperProfiles;
using Wally.RomMaster.WolneLekturyService.Domain.Users;
using Xunit;

namespace Wally.RomMaster.WolneLekturyService.Tests.UnitTests;

public class MapperTests
{
	private readonly IConfigurationProvider _configuration;
	private readonly IMapper _mapper;

	public MapperTests()
	{
		_configuration = new MapperConfiguration(
			config => config.AddMaps(typeof(IApplicationMapperProfilesAssemblyMarker).Assembly));

		_mapper = _configuration.CreateMapper();
	}

	[Fact]
	public void ShouldHaveValidConfiguration()
	{
		var act = () => _configuration.AssertConfigurationIsValid();

		act.Should()
			.NotThrow();
	}

	[Theory]
	[InlineData(typeof(User), typeof(GetUsersRequest))]
	[InlineData(typeof(User), typeof(GetUsersResponse))]
	[InlineData(typeof(User), typeof(GetUserResponse))]
	public void ShouldSupportMappingFromSourceToDestination(Type source, Type destination)
	{
		var instance = GetInstanceOf(source);
		var idProperty = source.GetProperty(nameof(User.Id)) !;
		idProperty.DeclaringType!.GetProperty(nameof(User.Id)) !.SetValue(instance, new UserId());

		var act = () => _mapper.Map(instance, source, destination);

		act.Should()
			.NotThrow();
	}

	[Theory]
	[InlineData(typeof(GetUsersRequest), typeof(GetUsersResponse))]
	[InlineData(typeof(GetUsersResponse), typeof(GetUsersRequest))]
	public void ShouldNotSupportMappingFromSourceToDestination(Type source, Type destination)
	{
		var instance = GetInstanceOf(source);

		var act = () => _mapper.Map(instance, source, destination);

		act.Should()
			.ThrowExactly<AutoMapperMappingException>();
	}

	private static object GetInstanceOf(Type type)
	{
		return Activator.CreateInstance(type, true) !;
	}
}
