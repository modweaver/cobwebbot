using System.Globalization;
using CobwebBot.Commands;
using DSharpPlus;
using DSharpPlus.CommandsNext;
using DSharpPlus.CommandsNext.Exceptions;
using DSharpPlus.EventArgs;
using Utilities = CobwebBot.Commands.Utilities;

namespace CobwebBot;

public sealed class CommandExecutor
{
    /// <summary>
    /// Executes commands for CommandsNext. Searches for existing commands, searching documentation if no command matched.
    /// </summary>
    public Task MessageCreatedAsync(DiscordClient client, MessageCreateEventArgs eventArgs)
    {
        System.Console.WriteLine("h");
        // If the message doesn't start with a bot ping, ignore it.
        bool val1 = !eventArgs.Message.Content.StartsWith(client.CurrentUser.Mention, false,
            CultureInfo.InvariantCulture);
        bool val2 = true;
        
        foreach (var prefix in Program.prefixes)
        {
            if (eventArgs.Message.Content.StartsWith(prefix))
            {
                val2 = false;
            }
        }

        if (val1 && val2)
        {
            return Task.CompletedTask;
        }

        // Grab CNext
        CommandsNextExtension commandsNext = client.GetCommandsNext();
        //commandsNext.RegisterCommands<Moderation>();
        commandsNext.RegisterCommands<Utilities>();

        // Remove the mention...
        string fullCommand = eventArgs.Message.Content[client.CurrentUser.Mention.Length..].Trim();

        // See if the message is an actual command...
        Command? command = commandsNext.FindCommand(fullCommand, out string? arguments);

        // Throw command not found exception
        if (command == null)
        {
            throw new CommandNotFoundException(fullCommand);
        }

        // Off with his head! (Execute the command)
        return commandsNext.ExecuteCommandAsync(commandsNext.CreateContext(eventArgs.Message, client.CurrentUser.Mention, command, arguments));
    }
}

