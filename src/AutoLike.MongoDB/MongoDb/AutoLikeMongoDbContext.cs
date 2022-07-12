using AutoLike.Financials;
using AutoLike.Gifts;
using AutoLike.Promotions;
using AutoLike.Services;
using MongoDB.Driver;
using Volo.Abp.Data;
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

    protected override void CreateModel(IMongoModelBuilder modelBuilder)
    {
        base.CreateModel(modelBuilder);

        //builder.Entity<YourEntity>(b =>
        //{
        //    //...
        //});
    }
}
