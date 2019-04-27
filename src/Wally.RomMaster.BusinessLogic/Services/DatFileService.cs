﻿namespace Wally.RomMaster.BusinessLogic.Services
{
    using System;
    using System.Threading.Tasks;
    using Microsoft.Extensions.Logging;
    using Microsoft.Extensions.Options;
    //using Common;
    //using Common.Database;
    //using RomMaster.DatFileParser;
    using System.Collections.Generic;
    using System.Security.Cryptography;
    using RomMaster.Domain.Models;
    using Wally.RomMaster.DatFileParser;
    using Wally.Database;

    public class DatFileService : FileService
    {
        private readonly Parser datFileParser;

        public DatFileService(ILogger<DatFileService> logger, IOptions<AppSettings> appSettings, IUnitOfWorkFactory unitOfWorkFactory, HashAlgorithm crc32, Parser datFileParser)
            : base(logger, appSettings, unitOfWorkFactory, crc32)
        {
            this.datFileParser = datFileParser;
        }

        protected override IEnumerable<Folder> GetFolders(IOptions<AppSettings> appSettings)
        {
            return appSettings.Value.DatRoots;
        }

        protected override async Task PostProcess(File file)
        {
            if (!IsDatFile(file.Path))
            {
                Logger.LogDebug($"DatFile '{file.Path}' can't be processed. Skipping.");
                return;
            }

            using (var uow = unitOfWorkFactory.Create())
            {
                var repoDat = uow.GetRepository<Dat>();
                DatFileParser.Models.DataFile datFile;

                if (await repoDat.AnyAsync(a => a.File.Path == file.Path).ConfigureAwait(false))
                {
                    Logger.LogDebug($"DatFile '{file.Path}' already processed. Skipping.");
                    return;
                }

                try
                {
                    datFile = await this.datFileParser.ParseAsync(file.Path);
                }
                catch (Exception ex)
                {
                    Logger.LogError(ex, ex.Message);
                    return;
                }

                Dat dat = await repoDat.FindAsync(a => a.Name == datFile.Header.Name && a.Version == datFile.Header.Version).ConfigureAwait(false);
                if (dat != null)
                {
                    Logger.LogDebug($"DatFile '{file.Path}' has been processed already. Skipping.");
                    return;
                }

                dat = new Dat
                {
                    Name = datFile.Header.Name,
                    Description = datFile.Header.Description,
                    Version = datFile.Header.Version,
                    Category = datFile.Header.Category,
                    Author = datFile.Header.Author,
                    Date = ParseDateTime(datFile.Header.Date),
                    File = file
                };

                foreach (var game in datFile.Games)
                {
                    var g = new Game
                    {
                        Name = game.Name,
                        Description = game.Description,
                        Year = game.Year
                    };

                    foreach (var rom in game.Roms)
                    {
                        var r = new Rom
                        {
                            Name = rom.Name,
                            Size = rom.Size,
                            Crc = rom.Crc,
                            Md5 = rom.Md5,
                            Sha1 = rom.Sha1
                        };

                        g.Roms.Add(r);
                    }

                    dat.Games.Add(g);
                }

                await repoDat.AddAsync(dat).ConfigureAwait(false);
                
                try
                {
                    await uow.CommitAsync().ConfigureAwait(false);
                }
                catch (Exception ex)
                {
                    Logger.LogError(ex, ex.Message);
                }
            }
        }

        private DateTime? ParseDateTime(string date)
        {
            if (string.IsNullOrEmpty(date))
            {
                return null;
            }

            DateTime result;

            if (DateTime.TryParseExact(date, "MM-dd-yyyy", System.Globalization.CultureInfo.InvariantCulture, System.Globalization.DateTimeStyles.None, out result))
            {
                return result;
            }

            //20140807 16-00-31
            if (DateTime.TryParseExact(date, "yyyyMMdd HH-mm-ss", System.Globalization.CultureInfo.InvariantCulture, System.Globalization.DateTimeStyles.None, out result))
            {
                return result;
            }

            //2018-06-24
            if (DateTime.TryParseExact(date, "yyyy-MM-dd", System.Globalization.CultureInfo.InvariantCulture, System.Globalization.DateTimeStyles.None, out result))
            {
                return result;
            }

            //24.03.2018 20:02:12
            if (DateTime.TryParseExact(date, "dd.MM.yyyy HH:mm:ss", System.Globalization.CultureInfo.InvariantCulture, System.Globalization.DateTimeStyles.None, out result))
            {
                return result;
            }

            //21/04/2019
            if (DateTime.TryParseExact(date, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture, System.Globalization.DateTimeStyles.None, out result))
            {
                return result;
            }

            //Sep 1 2010
            if (DateTime.TryParseExact(date, "MMM d yyyy", System.Globalization.CultureInfo.InvariantCulture, System.Globalization.DateTimeStyles.None, out result))
            {
                return result;
            }


            //Sep 01 2010
            if (DateTime.TryParseExact(date, "MMM dd yyyy", System.Globalization.CultureInfo.InvariantCulture, System.Globalization.DateTimeStyles.None, out result))
            {
                return result;
            }

            //29/01/2010 15:33:00
            if (DateTime.TryParseExact(date, "dd/MM/yyyy HH:mm:ss", System.Globalization.CultureInfo.InvariantCulture, System.Globalization.DateTimeStyles.None, out result))
            {
                return result;
            }

            //2008.08.16
            if (DateTime.TryParseExact(date, "yyyy.MM.dd", System.Globalization.CultureInfo.InvariantCulture, System.Globalization.DateTimeStyles.None, out result))
            {
                return result;
            }

            //03 December 2008
            if (DateTime.TryParseExact(date, "dd MMMM yyyy", System.Globalization.CultureInfo.InvariantCulture, System.Globalization.DateTimeStyles.None, out result))
            {
                return result;
            }

            System.Diagnostics.Debugger.Break();

            return null;
        }

        private bool IsDatFile(string file)
        {
            if (file.Contains('#'))
            {
                //TODO: Archived dat files are not supported yet
                return false;
            }

            switch (System.IO.Path.GetExtension(file).ToLower())
            {
                //Dat
                case ".dat":
                    return true;
                default:
                    return false;
            }
        }
    }
}
