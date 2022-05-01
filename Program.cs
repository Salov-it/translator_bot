using Telegram.Bot;
using Telegram.Bot.Exceptions;
using Telegram.Bot.Extensions.Polling;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using translator_bot;
using static translator_bot.Config;
using static translator_bot.interpreter;
using Telegram.Bot.Types.ReplyMarkups;
using System;
using System.IO;


Config config = new Config();
interpreter reter = new interpreter();


//подключения бота

var botClient = new TelegramBotClient(Token);

using var cts = new CancellationTokenSource();

 void Text(string s1)
{
    try
    {
        //Pass the filepath and filename to the StreamWriter Constructor
        StreamWriter sw = new StreamWriter("E:\\Test.txt",true);
        //Write a line of text
     
      
        sw.WriteLine(s1);
        
        
        
        //Write a second line of text
       
        //Close the file
        sw.Close();
    }
    catch (Exception e)
    {
        Console.WriteLine("Exception: " + e.Message);
    }
    finally
    {
        Console.WriteLine("Executing finally block.");
    }
}


void Opentext()
{
    String line;
    try
    {
        Config config = new Config();
        StreamReader sr = new StreamReader("E:\\Test.txt");
        //Read the first line of text
        line = sr.ReadLine();
        //Continue to read until you reach end of file
        while (line != null)
        {
            //write the lie to console window
            config.Searcn2 = line;
            //Read the next line
            line = sr.ReadLine();
        }
        //close the file
        sr.Close();
        Console.ReadLine();
    }
    catch (Exception e)
    {
        Console.WriteLine("Exception: " + e.Message);
    }
    finally
    {
        Console.WriteLine("Executing finally block.");
    }
}
  
void Xset(string text3)
{
  
    Config config1 = new Config();

    
    if (xset == false)
    {  
      config1.X1  = text3;
        xset = true;
      
    }
    else
    {
        xset = false;
    }
}

//клавиатура
ReplyKeyboardMarkup replyKeyboardMarkup = new(new[]
  {
        new KeyboardButton[] { "перевести на ru" },
        new KeyboardButton[] { "перевести на англ" }
    });


// StartReceiving does not block the caller thread. Receiving is done on the ThreadPool.
var receiverOptions = new ReceiverOptions
{
    AllowedUpdates = { } // receive all update types
};
botClient.StartReceiving(
    HandleUpdateAsync,
    HandleErrorAsync,
    receiverOptions,
    cancellationToken: cts.Token);

var me = await botClient.GetMeAsync();

Console.WriteLine($"Start listening for @{me.Username}");
Console.ReadLine();

// Send cancellation request to stop bot
cts.Cancel();

async Task HandleUpdateAsync(ITelegramBotClient botClient, Update update, CancellationToken cancellationToken)
{
    // Only process Message updates: https://core.telegram.org/bots/api#message
    if (update.Type != UpdateType.Message)
        return;
    // Only process text messages
    if (update.Message!.Type != MessageType.Text)
        return;

    var chatId = update.Message.Chat.Id;
    var messageText = update.Message.Text;

    Console.WriteLine($"Received a '{messageText}' message in chat {chatId}.");

    // Echo received message text
   if(messageText == "/start")
    {
        Message sentMessage = await botClient.SendTextMessageAsync(chatId: chatId, text: "Бот переводчик сообщений отправте текст перевода затем выберите на какой язык перевести");
       
    }
     interpreter inter = new interpreter();
    Xset(messageText);
    if (messageText == "перевести на англ")
    {
        inter.yandextranslatAsync(x1, "en");
        Message sentMessage = await botClient.SendTextMessageAsync(chatId: chatId, text: result);
    }
    else if(messageText == "перевести на ru")
    {
        inter.yandextranslatAsync(x1, "ru");
        Message sentMessage = await botClient.SendTextMessageAsync(chatId: chatId, text: result);
    }

    








}

Task HandleErrorAsync(ITelegramBotClient botClient, Exception exception, CancellationToken cancellationToken)
{
    var ErrorMessage = exception switch
    {
        ApiRequestException apiRequestException
            => $"Telegram API Error:\n[{apiRequestException.ErrorCode}]\n{apiRequestException.Message}",
        _ => exception.ToString()
    };

    Console.WriteLine(ErrorMessage);
    return Task.CompletedTask;
}

