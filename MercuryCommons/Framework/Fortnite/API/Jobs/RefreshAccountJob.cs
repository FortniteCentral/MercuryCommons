﻿using System;
using System.Threading.Tasks;
using Quartz;

namespace MercuryCommons.Framework.Fortnite.API.Jobs;

public class RefreshAccountJob : IJob
{
    public async Task Execute(IJobExecutionContext context)
    {
        if (context.MergedJobDataMap["client"] is not FortniteApiClient client)
        {
            throw new InvalidOperationException("Tried to execute refresh token job but client was null or invalid type.");
        }

        var response = await client.AccountPublicService.RefreshAccessTokenAsync().ConfigureAwait(false);
        if (response.IsSuccessful)
        {
            await client.OnRefreshAsync(response.Data).ConfigureAwait(false);
        }
        else
        {
            throw new InvalidOperationException($"Failed to refresh account. ({response.Error?.Error})");
        }
    }
}