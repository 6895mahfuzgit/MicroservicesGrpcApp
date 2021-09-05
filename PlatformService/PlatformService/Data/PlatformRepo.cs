using Microsoft.EntityFrameworkCore;
using PlatformService.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace PlatformService.Data
{
    public class PlatformRepo : IPlatformRepo
    {
        private readonly ApplicationDBContext _applicationDBContext;

        public PlatformRepo(ApplicationDBContext applicationDBContext)
        {
            _applicationDBContext = applicationDBContext;
        }

        public void CreatePlatfrom(Platform platform)
        {
            if (platform == null)
            {
                throw new ArgumentException();
            }

            _applicationDBContext.Platforms.Add(platform);
        }

        public IEnumerable<Platform> GetAllPlatfrom()
        {
            return _applicationDBContext.Platforms.AsNoTracking().ToList();
        }

        public Platform GetPlatform(int id)
        {
            return _applicationDBContext.Platforms.SingleOrDefault(x => x.Id == id);
        }

        public bool SaveChanges()
        {
            return _applicationDBContext.SaveChanges() > 0;
        }
    }
}
