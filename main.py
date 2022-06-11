import discord
import os
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
                    out = message.content.split('\n')[1].split(' ')[1]
                    break
            if not out == None and not out == "":
                await original_channel.send(f"{out}")
            else:
                await original_channel.send(f"No tag found for {tag_to_find}")
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