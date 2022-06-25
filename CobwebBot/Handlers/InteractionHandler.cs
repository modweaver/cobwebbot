using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DSharpPlus.EventArgs;
using DSharpPlus;
using DSharpPlus.Entities;

namespace CobwebBot.Handlers
{
    internal class InteractionHandler
    {
        public async static void Handle(DiscordClient s, ComponentInteractionCreateEventArgs e)
        {
            await e.Interaction.CreateResponseAsync(InteractionResponseType.DeferredMessageUpdate);
            var user = e.User;
            var member = await e.Guild.GetMemberAsync(user.Id);
            DiscordRole role;
            var guildId = e.Guild.Id;


            if (e.Id == "modweaver_role_give")
            {
                if (guildId == 843489593319751682) // test server
                {
                    role = e.Guild.GetRole(990313379153473586);
                    var userRoles = member.Roles;
                    if (userRoles.Contains(role))
                    {
                        await member.RevokeRoleAsync(role);
                    }
                    else
                    {
                        await member.GrantRoleAsync(role);
                    }
                }
                else if (guildId == 952598946511986699) // real server
                {
                    role = e.Guild.GetRole(990314173416226816);
                    var userRoles = member.Roles;
                    if (userRoles.Contains(role))
                    {
                        await member.RevokeRoleAsync(role);
                    }
                    else
                    {
                        await member.GrantRoleAsync(role);
                    }
                }
            }
            else if (e.Id == "cobwebapi_role_give")
            {
                if (guildId == 843489593319751682) // test server
                {
                    role = e.Guild.GetRole(990313429078265877);
                    var userRoles = member.Roles;
                    if (userRoles.Contains(role))
                    {
                        await member.RevokeRoleAsync(role);
                    }
                    else
                    {
                        await member.GrantRoleAsync(role);
                    }
                }
                else if (guildId == 952598946511986699) // real server
                {
                    role = e.Guild.GetRole(990314228198027274);
                    var userRoles = member.Roles;
                    if (userRoles.Contains(role))
                    {
                        await member.RevokeRoleAsync(role);
                    }
                    else
                    {
                        await member.GrantRoleAsync(role);
                    }
                }
            }
            else if (e.Id == "cwlauncher_role_give")
            {
                if (guildId == 843489593319751682) // test server
                {
                    role = e.Guild.GetRole(990313457163337778);
                    var userRoles = member.Roles;
                    if (userRoles.Contains(role))
                    {
                        await member.RevokeRoleAsync(role);
                    }
                    else
                    {
                        await member.GrantRoleAsync(role);
                    }
                }
                else if (guildId == 952598946511986699) // real server
                {
                    role = e.Guild.GetRole(990314259537870928);
                    var userRoles = member.Roles;
                    if (userRoles.Contains(role))
                    {
                        await member.RevokeRoleAsync(role);
                    }
                    else
                    {
                        await member.GrantRoleAsync(role);
                    }
                }
            }
            else if (e.Id == "webcrawler_role_give")
            {
                if (guildId == 843489593319751682) // test server
                {
                    role = e.Guild.GetRole(990313520593788988);
                    var userRoles = member.Roles;
                    if (userRoles.Contains(role))
                    {
                        await member.RevokeRoleAsync(role);
                    }
                    else
                    {
                        await member.GrantRoleAsync(role);
                    }
                }
                else if (guildId == 952598946511986699) // real server
                {
                    role = e.Guild.GetRole(990314322934784090);
                    var userRoles = member.Roles;
                    if (userRoles.Contains(role))
                    {
                        await member.RevokeRoleAsync(role);
                    }
                    else
                    {
                        await member.GrantRoleAsync(role);
                    }
                }
            }
        }
    }
}
