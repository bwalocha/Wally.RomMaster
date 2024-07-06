﻿using System;
using AutoMapper;
using FluentAssertions;
using Wally.RomMaster.HashService.Application.Contracts.Requests.Users;
using Wally.RomMaster.HashService.Application.Contracts.Responses.Users;
using Wally.RomMaster.HashService.Application.MapperProfiles;
using Wally.RomMaster.HashService.Domain.Users;
using Xunit;

namespace Wally.RomMaster.HashService.Tests.UnitTests;

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
