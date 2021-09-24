using CommandService.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CommandService.Data
{
    public class CommandRepo : ICommandRepo
    {
        private readonly AppDBContext _context;

        public CommandRepo(AppDBContext context)
        {
            _context = context;
        }

        public void CreateCommand(int platfromId, Command command)
        {
            try
            {
                if (command == null)
                {
                    throw new ArgumentNullException(nameof(command));
                }

                command.PlatformId = platfromId;
                _context.Commands.Add(command);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public void CreatePlatfrom(Platform platform)
        {
            try
            {
                if (platform == null)
                {
                    throw new ArgumentNullException(nameof(platform));
                }

                _context.Platforms.Add(platform);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public Command GetCommand(int platfromId, int commandId)
        {
            try
            {
                return _context.Commands.SingleOrDefault(x => x.PlatformId == platfromId && x.Id == commandId);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public IEnumerable<Command> GetCommandsForPlatform(int platfromId)
        {
            try
            {
                return _context.Commands.Where(x => x.PlatformId == platfromId)
                                        .OrderBy(x => x.Platform.Name)
                                        .AsNoTracking()
                                        .ToList();

            }
            catch (Exception)
            {

                throw;
            }
        }

        public IEnumerable<Platform> GetPlatforms()
        {
            try
            {
                return _context.Platforms.AsNoTracking().ToList();
            }
            catch (Exception)
            {

                throw;
            }
        }

        public bool PlatfromExists(int platfromId)
        {
            try
            {
                return _context.Platforms.Any(p => p.Id == platfromId);
            }
            catch (Exception)
            {

                throw;
            }
        }



        public bool ExternalPlatfromExists(int externalPlatfromId)
        {
            try
            {
                return _context.Platforms.Any(p => p.ExternalID == externalPlatfromId);
            }
            catch (Exception)
            {

                throw;
            }
        }
        public bool SaveChanges()
        {
            try
            {
                return _context.SaveChanges() > 0;
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
