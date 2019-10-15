using CodeVein.Data;
using CodeVein.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeVein.Services
{
    public class BloodCodeService
    {

        private readonly Guid _userId;

        public BloodCodeService(Guid userId)
        {
            _userId = userId;
        }

        public bool CreateBloodCode(BloodCodeCreate model)
        {
            var entity =
            new BloodCode()
            {
                OwnerId = _userId,  //<----need to be bloodcodeid?
                BcName = model.BcName,
                BcSkills = model.BcSkills,
                BcDescription = model.BcDescription,
                BcLocation = model.BcLocation

            };

            using (var ctx = new ApplicationDbContext())
            {
                ctx.BloodCode.Add(entity);
                return ctx.SaveChanges() == 1;
            }




        }
        public IEnumerable<BloodCodeListItem> GetBloodCode()
        {
            using (var ctx = new ApplicationDbContext())
            {
                var query =
                    ctx
                        .BloodCode
                        .Where(e => e.OwnerId == _userId)
                        .Select(
                            e =>
                                new BloodCodeListItem
                                {
                                    BloodCodeId = e.BloodCodeId,
                                    BcName = e.BcName,
                                    BcSkills = e.BcSkills,
                                    BcDescription = e.BcDescription,
                                    BcLocation = e.BcLocation
                                }
                        );

                return query.ToArray();
            }

        }
        public BloodCodeDetails GetBloodCodeById(int id)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity =
                    ctx
                        .BloodCode
                        .Single(e => e.BloodCodeId == id && e.OwnerId == _userId);
                return
                    new BloodCodeDetails
                    {
                        BloodCodeId = entity.BloodCodeId,
                        BcName = entity.BcName,
                        BcSkills = entity.BcSkills,
                        BcDescription = entity.BcDescription,
                        BcLocation = entity.BcLocation
                    };
            }


        }
        
        public bool UpdateBloodCode(BloodCodeEdit model)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity =
                    ctx
                        .BloodCode
                        .Single(e => e.BloodCodeId == model.BloodCodeId && e.OwnerId == _userId);

                entity.BcName = model.BcName;
                entity.BcSkills = model.BcSkills;
                entity.ModifiedUtc = DateTimeOffset.UtcNow;

                return ctx.SaveChanges() == 1;
            }
        }


        public bool DeleteBloodCode(int BloodCodeId)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity =
                    ctx
                        .BloodCode
                        .Single(e => e.BloodCodeId == BloodCodeId && e.OwnerId == _userId);

                ctx.BloodCode.Remove(entity);

                return ctx.SaveChanges() == 1;
            }
        }




    }


}
