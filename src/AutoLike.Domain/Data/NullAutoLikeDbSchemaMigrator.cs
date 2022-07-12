using System.Threading.Tasks;
using Volo.Abp.DependencyInjection;

namespace AutoLike.Data;

/* This is used if database provider does't define
 * IAutoLikeDbSchemaMigrator implementation.
 */
public class NullAutoLikeDbSchemaMigrator : IAutoLikeDbSchemaMigrator, ITransientDependency
{
    public Task MigrateAsync()
    {
        return Task.CompletedTask;
    }
}
