using AutoLike.MongoDB;
using Xunit;

namespace AutoLike;

[CollectionDefinition(AutoLikeTestConsts.CollectionDefinitionName)]
public class AutoLikeDomainCollection : AutoLikeMongoDbCollectionFixtureBase
{

}
