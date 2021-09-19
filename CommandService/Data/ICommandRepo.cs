using CommandService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CommandService.Data
{
    public interface ICommandRepo
    {
        bool SaveChanges();

        //PlatFroms
        IEnumerable<Platform> GetPlatforms();
        void CreatePlatfrom(Platform platform);
        bool PlatfromExists(int platfromId);

        //Commands
        IEnumerable<Command> GetCommandsForPlatform(int platfromId);
        Command GetCommand(int platfromId, int commandId);
        void CreateCommand(int platfromId, Command command);

    }
}
