﻿using MelodyFusion.BLL.Interfaces;

namespace MelodyFusion.Extensions;

public static class HostExtensions
{
    public static IHost SetupIdentity(this IHost host)
    {
        using var scope = host.Services.CreateScope();
        var initializer = scope.ServiceProvider.GetRequiredService<IRoleInitializer>();
        initializer.InitializeIdentityData();

        return host;
    }
}