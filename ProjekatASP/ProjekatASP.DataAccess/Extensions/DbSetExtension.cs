using Microsoft.EntityFrameworkCore;
using ProjekatASP.DataAccess.Exceptions;
using ProjekatASP.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjekatASP.DataAccess.Extensions
{
    public static class DbSetExtension
    {

        public static void Deactivate(this DbContext context, Entity entity)
        {
            entity.IsActive = false;
            context.Entry(entity).State = EntityState.Modified;
        }

        public static void Deactivate<T>(this DbContext context, int id)
            where T : Entity
        {
            var item = context.Set<T>().Find(id);

            if (item == null)
            {
                throw new EntityNotFoundException();
            }

            item.IsActive = false;
        }

        public static void Deactivate<T>(this DbContext context, IEnumerable<int> ids)
            where T : Entity
        {
            var enitiesToDeactivate = context.Set<T>().Where(x => ids.Contains(x.Id));

            foreach (var e in enitiesToDeactivate)
            {
                e.IsActive = false;
            }

        }

    }
}
