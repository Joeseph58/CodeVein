using CodeVein.Data;
using CodeVein.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeVein.Services
{
    public class BuildService
    {
        private readonly Guid _userId;

        public BuildService(Guid userId)
        {
            _userId = userId;
        }
       

        public bool CreateBuild(BuildCreate model)
        {
            var entity =
            new Builds()
            {
                OwnerId = _userId,  //<----need to be bloodcodeid?
                BuildName = model.BuildName,
                BuildCode = model.BuildCode,
                BuildWeapon = model.BuildWeapon,
                BuildSkills = model.BuildSkills,
                BuildDescription = model.BuildDescription

            };

            using (var ctx = new ApplicationDbContext())
            {
                ctx.Builds.Add(entity);
                return ctx.SaveChanges() == 1;
            }




        }
        public IEnumerable<BuildListItem> GetBuild()
        {
            using (var ctx = new ApplicationDbContext())
            {
                var query =
                    ctx
                        .Builds
                        .Where(e => e.OwnerId == _userId)
                        .Select(
                            e =>
                                new BuildListItem
                                {
                                    BuildId = e.BuildId,
                                    BuildName = e.BuildName,
                                    BuildSkills = e.BuildSkills,
                                    BuildDescription = e.BuildDescription,
                                    
                                }
                        );

                return query.ToArray();
            }

        }
        public BuildDetails GetBuildById(int id)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity =
                    ctx
                        .Builds
                        .Single(e => e.BuildId == id && e.OwnerId == _userId);
                return
                    new BuildDetails
                    {
                        BuildId = entity.BuildId,
                        BuildName = entity.BuildName,
                        BuildCode = entity.BuildCode,
                        BuildWeapon = entity.BuildWeapon,
                        BuildSkills = entity.BuildSkills,
                        BuildDescription = entity.BuildDescription,
                    };
            }


        }

        public bool UpdateBuild(BuildEdit model)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity =
                    ctx
                        .Builds
                        .Single(e => e.BuildId == model.BuildId && e.OwnerId == _userId);

                entity.BuildName = model.BuildName;
                entity.BuildSkills = model.BuildSkills;
                entity.ModifiedUtc = DateTimeOffset.UtcNow;

                return ctx.SaveChanges() == 1;
            }
        }


        public bool DeleteBuild(int BuildId)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity =
                    ctx
                        .Builds
                        .Single(e => e.BuildId == BuildId && e.OwnerId == _userId);

                ctx.Builds.Remove(entity);

                return ctx.SaveChanges() == 1;
            }
        }




    }

}

