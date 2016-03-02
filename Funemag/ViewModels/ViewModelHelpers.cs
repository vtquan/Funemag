using Funemag.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Funemag.ViewModels
{
    public static class ViewModelHelpers
    {
        public static ICollection<AssignedPlatformData> PopulatePlatformData(this ICollection<Platform> platforms, List<Platform> allPlatforms)
        {
            var assignedPlatforms = new List<AssignedPlatformData>();

            foreach (var item in allPlatforms)
            {
                assignedPlatforms.Add(new AssignedPlatformData
                {
                    PlatformId = item.PlatformId,
                    Name = item.Name,
                    Assigned = platforms.Contains(item)
                });
            }           

            return assignedPlatforms;
        }

        public static void AddOrUpdatePlatforms(this ICollection<Platform> platforms, IEnumerable<AssignedPlatformData> assignedPlatforms, List<Platform> allPlatforms)
        {
            if (assignedPlatforms == null)
                return;

            foreach (var platform in platforms.ToList())
            {
                platforms.Remove(platform);
            }

            foreach (var platform in assignedPlatforms.Where(p => p.Assigned))
            {
                platforms.Add(allPlatforms.Find(p => p.PlatformId == platform.PlatformId));
            }
        }
    }
}