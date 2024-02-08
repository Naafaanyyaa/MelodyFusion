﻿using MelodyFusion.DLL.Entities.Abstract;
using MelodyFusion.DLL.Entities.Identity;

namespace MelodyFusion.DLL.Entities
{
    public class SubscriptionDto : BaseEntity
    {
        public decimal Amount { get; set; }
        public string UserId { get; set; } = string.Empty;
        public virtual UserDto User { get; set; } = new UserDto();
    }
}