using Microsoft.EntityFrameworkCore.Diagnostics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFCoreInterceptor.Logging
{
    public class EFSaveChangesInterceptor : SaveChangesInterceptor
    {
        public override InterceptionResult<int> SavingChanges(DbContextEventData eventData, InterceptionResult<int> result)
        {
            var context = eventData.Context;
            if (context != null)
            {
                foreach (var entry in context.ChangeTracker.Entries())
                {
                    Console.WriteLine($"Entity {entry.Entity.GetType().Name} is {entry.State}");
                }
            }
            return base.SavingChanges(eventData, result);
        }
    }
}
