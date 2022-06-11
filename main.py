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
    async def on_member_join(self, member):
        channel = self.get_channel(952598947069841480)
        await channel.send(f'Welcome {member.mention}! Please read the <#952600261023649802> and have fun!')

intents = discord.Intents.default()
intents.message_content = True

client = MyClient(intents=intents)
token = open('token.txt', 'r').read()
client.run(token)