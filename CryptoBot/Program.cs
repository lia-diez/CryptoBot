using System.Threading.Channels;
using CryptoBot;
using Telegram.Bot.BorisExtensions;

using var cts = new CancellationTokenSource();
var client = new CustomClient(Utilities.GetToken(), cts.Token);
client.UseController<Controller>();
client.Start();
Console.ReadLine();
cts.Cancel();

