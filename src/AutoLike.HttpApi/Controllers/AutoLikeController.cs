using AutoLike.Localization;
using Volo.Abp.AspNetCore.Mvc;

namespace AutoLike.Controllers;

/* Inherit your controllers from this class.
 */
public abstract class AutoLikeController : AbpControllerBase
{
    protected AutoLikeController()
    {
        LocalizationResource = typeof(AutoLikeResource);
    }
}
