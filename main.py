import asyncio
from multiprocessing.connection import wait
import discord
import os
import time
class MyClient(discord.Client):
    async def on_ready(self):
        print(f'Logged on as {self.user}!')
    
    async def on_message(self, message):

        def is_me(message):
            return message.author == self.user
        if is_me(message):
            return
        #if message.channel.id != 980786397696888883:
        #    print(f'Message `{message.content}` from `{message.author}` in {message.channel.id} was not in the correct channel!')
        message_id = message.id
        channel_id = message.channel.id
        if message.content.startswith('!tag'):
            out = ""
            original_channel = message.channel
            tag_to_find = message.content.split(' ')[1]


            channel = self.get_channel(985136865789218866)
            #messages = await channel.history(limit=200)
            async for message in channel.history(limit=200):
                if message.content.startswith("id: " + tag_to_find):
                    out = message.content.split('\n')[1].split('out: ')[1]
                    break
                if tag_to_find == "list":
                    if message.content.startswith("id: "):
                        #out = "ignore_out"
                        out += message.content.split('\n')[0].split('id: ')[1] + "\n"
                        #await original_channel.send(message.content.split('\n')[0].split('id: ')[1])
                
            if not out == None and not out == "" and not out == "ignore_out":
                await original_channel.send(f"{out}")
            elif out == "ignore_out":
                pass
            else:
                await original_channel.send(f"Tag {tag_to_find} not found!")
        if message.content.startswith('!kick'):
            if message.author.guild_permissions.kick_members:
                try:
                    member = message.mentions[0]
                except IndexError:
                    await message.channel.send("Please mention a member to kick!")
                    return
                try:
                    reason = message.content.split('reason="')[1].split('"')[0]
                except IndexError:
                    await message.channel.send("Please provide a reason for the kick!")
                    return
                embed_description = f"You have been kicked from {member.guild.name} \n \n Reason: {reason} \n \n You may join the server with a new invite link. \n \n Moderator: {message.author.name}"
                kick_embed = discord.Embed(title="Kicked!", description=embed_description, color=0xFF9900)
                
                await member.send(embed=kick_embed)
                await member.kick()
                message_sent = await message.channel.send(f'Kicked {member.mention}')
                await asyncio.sleep(5)
                await message_sent.delete()
                await message.delete()
        if message.content.startswith('!ban'):
            if message.author.guild_permissions.ban_members:
                try:
                    member = message.mentions[0]
                except IndexError:
                    id_to_try = message.content.split("!ban ")[1].split(" reason")[0]
                    if len(id_to_try) == 18:
                        member = message.guild.get_member(id_to_try)
                        if member is None:
                            member = client.fetch_user(id_to_try)
                        print(member.name)
                    else:
                        await message.channel.send("Please mention a member to ban!")
                    return
                try:
                    reason = message.content.split('reason="')[1].split('"')[0]
                except IndexError:
                    await message.channel.send("Please provide a reason for the ban!")
                    return
                embed_description = f"You have been banned from {member.guild.name} \n \n Reason: {reason} \n \n You may join the server with a new invite link. \n \n Moderator: {message.author.name}"
                ban_embed = discord.Embed(title="Banned!", description=embed_description, color=0xFF0000)
                await member.send(embed=ban_embed)
                await member.ban()
                message_sent = await message.channel.send(f'Banned {member.mention}')
                await asyncio.sleep(5)
                await message_sent.delete()
                await message.delete()
        if message.content.startswith('!clear') or message.content.startswith('!purge'):
            if message.author.guild_permissions.manage_messages:
                try:
                    amount = int(message.content.split(' ')[1]) + 1
                except IndexError:
                    await message.channel.send("Please provide an amount of messages to delete!")
                    return
                await message.channel.purge(limit=amount)
                message_sent = await message.channel.send(f'Cleared {amount - 1} messages!')
                await asyncio.sleep(5)
                await message_sent.delete()
                await message.delete()
                
        if message.channel.id == 980789224217403422:
            if not message.content.startswith('Title:'):
                await message.delete()
                await message.author.send(f'Please use the correct format for your message.')
            thread_name = message.content.split('\n')[0].split('Title: ')[1]
            await message.channel.purge(limit=10, check=is_me)
            sent_message = await message.channel.send('Use this format for your message: \n Title: \n Description:')
            await sent_message.pin()
            await message.channel.create_thread(name=f"[{message.author.name}] {thread_name}", message=message)
        if message.channel.id == 985136865789218866:
            if not message.content.startswith('id: '):
                await message.delete()
                await message.author.send(f'Tag format: \n id: <id> \n out: <out>')
            else:
                id = message.content.split('id: ')[1].split('\n')[0]
                out = message.content.split('out: ')[1].split('\n')[0]

    async def on_member_join(self, member):
        channel = self.get_channel(952598947069841480)
        await channel.send(f'Welcome {member.mention}! Please read the <#952600261023649802> and have fun!')

intents = discord.Intents.default()
intents.message_content = True

client = MyClient(intents=intents)
token = open('token.txt', 'r').read()
client.run(token)