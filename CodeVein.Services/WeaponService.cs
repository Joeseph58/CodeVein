using CodeVein.Data;
using CodeVein.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeVein.Services
{
    public class WeaponService
    {
        private readonly Guid _userId;

        public WeaponService(Guid userId)
        {
            _userId = userId;
        }



            public bool CreateWeapon(WeaponsCreate model)
            {
                var entity =
                new Weapons()
                {
                    OwnerId = _userId,  //<----need to be bloodcodeid?
                    WeaponName = model.WeaponName,
                    WeaponType = model.WeaponType,
                    WeaponStats = model.WeaponStats,
                    WeaponLocation = model.WeaponLocation
                    

                };

                using (var ctx = new ApplicationDbContext())
                {
                    ctx.Weapons.Add(entity);
                    return ctx.SaveChanges() == 1;
                }




            }
            public IEnumerable<WeaponsListItem> GetWeapons()
            {
                using (var ctx = new ApplicationDbContext())
                {
                    var query =
                        ctx
                            .Weapons
                            .Where(e => e.OwnerId == _userId)
                            .Select(
                                e =>
                                    new WeaponsListItem
                                    {
                                        WeaponId = e.WeaponId,
                                        WeaponName = e.WeaponName,
                                        WeaponType = e.WeaponType,
                                        WeaponStats = e.WeaponStats,

                                    }
                            );

                    return query.ToArray();
                }

            }
            public WeaponsDetails GetWeaponById(int id)
            {
                using (var ctx = new ApplicationDbContext())
                {
                    var entity =
                        ctx
                            .Weapons
                            .Single(e => e.WeaponId == id && e.OwnerId == _userId);
                    return
                        new WeaponsDetails
                        {
                            WeaponId = entity.WeaponId,
                            WeaponName = entity.WeaponName,
                            WeaponType = entity.WeaponType,
                            WeaponStats = entity.WeaponStats,
                            WeaponLocation = entity.WeaponLocation,
                        };
                }


            }

            public bool UpdateWeapon(WeaponsEdit model)
            {
                using (var ctx = new ApplicationDbContext())
                {
                    var entity =
                        ctx
                            .Weapons
                            .Single(e => e.WeaponId == model.WeaponId && e.OwnerId == _userId);

                    entity.WeaponName = model.WeaponName;
                    entity.WeaponStats = model.WeaponStats;
                    entity.ModifiedUtc = DateTimeOffset.UtcNow;

                    return ctx.SaveChanges() == 1;
                }
            }


            public bool DeleteWeapon(int WeaponId)
            {
                using (var ctx = new ApplicationDbContext())
                {
                    var entity =
                        ctx
                            .Weapons
                            .Single(e => e.WeaponId == WeaponId && e.OwnerId == _userId);

                    ctx.Weapons.Remove(entity);

                    return ctx.SaveChanges() == 1;
                }
            }




        

    }
}
