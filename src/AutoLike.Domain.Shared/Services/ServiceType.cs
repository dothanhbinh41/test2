using System;
using System.Collections.Generic;
using System.Text;

namespace AutoLike.Services
{
    public enum ServiceType
    {
        Like, // BuffLike, Like Page
        View, // BuffView
        Comment, //BuffComment
        Search, //BuffSearch
        Follow, // BuffSub, Follow
        Share, 
        Reaction,
        Recommend,
        Eye // BuffEye
    } 
}
