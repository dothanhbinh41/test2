﻿using AutoLike.Agencies;
using AutoLike.Comments;
using AutoLike.Financials;
using AutoLike.Gifts;
using AutoLike.IdentityServer;
using AutoLike.Orders;
using AutoLike.Promotions;
using AutoLike.Services;
using AutoLike.Trackings;
using AutoLike.Transactions;
using AutoLike.Users;
using MongoDB.Driver; 
using Volo.Abp.Data;
using Volo.Abp.Identity;
using Volo.Abp.MongoDB;

namespace AutoLike.MongoDB;
 
public class AutoLikeMongoDbContext : AbpMongoDbContext
{
    /* Add mongo collections here. Example:
     * public IMongoCollection<Question> Questions => Collection<Question>();
     */

    public IMongoCollection<Promotion> Promotions => Collection<Promotion>();
    public IMongoCollection<GiftCode> GiftCodes => Collection<GiftCode>();
    public IMongoCollection<Service> Services => Collection<Service>();
    public IMongoCollection<Financial> Financials => Collection<Financial>();
    public IMongoCollection<Comment> Comments => Collection<Comment>();
    public IMongoCollection<QRCode> QRCodes => Collection<QRCode>();
    public IMongoCollection<Agency> Agencies => Collection<Agency>();
    public IMongoCollection<AgencyKey> AgencyKeys => Collection<AgencyKey>();
    public IMongoCollection<Order> Orders => Collection<Order>();
    public IMongoCollection<Transaction> Transactions => Collection<Transaction>();
    public IMongoCollection<Tracking> Trackings => Collection<Tracking>();
    public IMongoCollection<UserActionLock> UserActionLocks => Collection<UserActionLock>();
    public IMongoCollection<OrderProcess> OrderProcesses => Collection<OrderProcess>(); 
    public IMongoCollection<UserGiftCode> UserGiftCodes => Collection<UserGiftCode>(); 

    protected override void CreateModel(IMongoModelBuilder modelBuilder)
    {
        base.CreateModel(modelBuilder);

        //modelBuilder.Entity<IdentityUser>(b =>
        //{
            
        //});
    }
}
