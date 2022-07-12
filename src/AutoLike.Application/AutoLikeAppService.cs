using System;
using System.Collections.Generic;
using System.Text;
using AutoLike.Localization;
using Volo.Abp.Application.Services;

namespace AutoLike;

/* Inherit your application services from this class.
 */
public abstract class AutoLikeAppService : ApplicationService
{
    protected AutoLikeAppService()
    {
        LocalizationResource = typeof(AutoLikeResource);
    }
}
