using AutoLike.Financials;
using AutoLike.Gifts;
using AutoLike.Promotions;
using AutoLike.Services;
using AutoLike.Warranties;
using MongoDB.Driver;
using Volo.Abp.Data;
using Volo.Abp.Identity;
using Volo.Abp.MongoDB;

namespace AutoLike.MongoDB;

[ConnectionStringName("Default")]
public class AutoLikeMongoDbContext : AbpMongoDbContext
{
    /* Add mongo collections here. Example:
     * public IMongoCollection<Question> Questions => Collection<Question>();
     */

    public IMongoCollection<Promotion> Promotions => Collection<Promotion>();
    public IMongoCollection<GiftCode> GiftCodes => Collection<GiftCode>();
    public IMongoCollection<Service> Services => Collection<Service>();
    public IMongoCollection<Financial> Financials => Collection<Financial>();
    public IMongoCollection<Warranty> Warranties => Collection<Warranty>();

    protected override void CreateModel(IMongoModelBuilder modelBuilder)
    {
        base.CreateModel(modelBuilder);

        modelBuilder.Entity<IdentityUser>(b =>
        {
            b.P
        });
    }
}
