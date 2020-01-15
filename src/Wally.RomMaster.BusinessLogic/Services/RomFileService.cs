﻿using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Wally.Database;
using Wally.RomMaster.Domain.Models;

namespace Wally.RomMaster.BusinessLogic.Services
{
    public class RomFileService : FileService
    {
        public RomFileService(ILogger<RomFileService> logger, IOptions<AppSettings> appSettings, IUnitOfWorkFactory unitOfWorkFactory, HashAlgorithm crc32)
            : base(logger, appSettings, unitOfWorkFactory, crc32)
        {
        }

        protected override IEnumerable<Folder> GetFolders(IOptions<AppSettings> appSettings)
        {
            if (appSettings == null)
            {
                throw new ArgumentNullException(nameof(appSettings));
            }

            return appSettings.Value.RomRoots;
        }
    }
}
