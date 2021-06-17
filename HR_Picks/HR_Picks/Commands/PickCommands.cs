﻿using DSharpPlus.CommandsNext;
using DSharpPlus.CommandsNext.Attributes;
using System.Threading.Tasks;

namespace HR_Picks.Commands
{
    public class PickCommands : BaseCommandModule
    {
        [Command("MyPick")]
        public async Task HRPick(CommandContext ctx)
        {
            await ctx.Channel.SendMessageAsync("pong").ConfigureAwait(false);
        }
    }
}
