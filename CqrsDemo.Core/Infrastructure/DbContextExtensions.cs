using System.Diagnostics;
using Microsoft.EntityFrameworkCore;
using CqrsDemo.Core.Domain;

namespace CqrsDemo.Infrastructure
{
    //[DebuggerStepThrough]
    //public static class DbContextExtensions
    //{
    //    public static async Task<T> FindOrThrowAsync<T>(this DbContext context, params object[] keyValues) where T : class 
    //        => await context.FindAsync<T>(keyValues) ?? throw new EntityNotFoundException(typeof(T), keyValues);

    //    public static async Task<T> SingleOrThrowAsync<T>(this DbContext context, int id, Func<IQueryable<T>, IQueryable<T>> query) where T : Entity
    //    {
    //        var entity = await query(context.Set<T>())
    //            .SingleOrDefaultAsync(x => x.Id == id);

    //        return entity ?? throw new EntityNotFoundException(typeof(T), id);
    //    }
        
    //    public static async Task<T> FindSingleAsync<T>(this IQueryable<T> query, int id) where T : Entity
    //    {
    //        var result = await query.SingleOrDefaultAsync(x => x.Id == id);
    //        return result ?? throw new EntityNotFoundException(typeof(T), id);
    //    }
    //}
}