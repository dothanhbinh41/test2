using System.Threading.Tasks;

namespace AutoLike.Data;

public interface IAutoLikeDbSchemaMigrator
{
    Task MigrateAsync();
}
