using System;

namespace Wally.RomMaster.FileService.Domain.Abstractions;

public interface IClockService
{
	DateTime StartTimestamp { get; }

	DateTime GetTimestamp();
}
