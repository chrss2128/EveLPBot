using Discord.Commands;

using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace EveLPBot
{
    public class EveTimeModule : ModuleBase<SocketCommandContext>
    {
        [Command("evetime")]
        [Summary("displays current evetime")]
        public Task SayAsync()
            => ReplyAsync(DateTime.UtcNow.ToString());
    }
}
