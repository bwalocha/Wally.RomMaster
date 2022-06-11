using System.Collections.Generic;
using System.Diagnostics;

using Wally.Lib.DDD.Abstractions.DomainModels;
using Wally.RomMaster.Domain.Abstractions;

namespace Wally.RomMaster.Domain.Files;

[DebuggerDisplay("{Code}")]
public class SourceType : ValueObject
{
	private const string _input = "ToSort";
	private const string _output = "RomRoot";
	private const string _datRoot = "DatRoot";

	private static readonly Dictionary<string, SourceType> _dictionary;

	static SourceType()
	{
		_dictionary =
			new Dictionary<string, SourceType> { { _input, Input }, { _output, Output }, { _datRoot, DatRoot }, };
	}

	private SourceType(string code)
	{
		Code = code;
	}

	public string Code { get; }

	public static SourceType Input { get; } = new(_input);

	public static SourceType Output { get; } = new(_output);

	public static SourceType DatRoot { get; } = new(_datRoot);

	public static SourceType Get(string code)
	{
		if (!_dictionary.TryGetValue(code, out var value))
		{
			throw new InvalidSourceTypeCodeException(code);
		}

		return value;
	}

	private class InvalidSourceTypeCodeException : DomainValidationException
	{
		public InvalidSourceTypeCodeException(string code)
			: base($"The Source Type Code '{code}' is invalid.")
		{
		}
	}
}
