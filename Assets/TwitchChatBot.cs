using System;
using System.Net.Sockets;
using System.IO;
using UnityEngine;
using System.Threading;

namespace Assets
{
    public class TwitchChatBot
    {
        const string oauth = "oauth:ehxeo6hhb4ip503m1f2fvdc7fq2ghb";

        const string oauthFormat = "PASS {0}";
        const string messageFormat = "PRIVMSG #{0} :{1}";

        // server to connect to (edit at will)
        private readonly string _server;
        // server port (6667 by default)
        private readonly int _port;
        // user information defined in RFC 2812 (IRC: Client Protocol) is sent to the IRC server 
        private readonly string _user;

        // the bot's nickname
        private readonly string _nick;
        // channel to join
        private readonly string _channel;

        private readonly int _maxRetries;

        private int countofThing = 0;

        private bool stop = false;


        public TwitchChatBot(string server, int port,  string nick, string channel, int maxRetries = 3)
        {
            _server = server;
            _port = port;
            _nick = nick;
            _channel = channel;
            _maxRetries = maxRetries;
        }

        public void Start()
        {
            var retry = false;
            var retryCount = 0;
            stop = false;
            do
            {
                try
                {
                    using (var irc = new TcpClient(_server, _port))
                    using (var stream = irc.GetStream())
                    using (var reader = new StreamReader(stream))
                    using (var writer = new StreamWriter(stream))
                    {
                        if (stop)
                        {
                            return;
                        }

                        writer.WriteLine(string.Format(oauthFormat, oauth));
                        writer.Flush();

                        writer.WriteLine("NICK " + _nick);
                        writer.Flush();

                        while (true)
                        {

                            string inputLine;
                            
                            while ((inputLine = reader.ReadLine()) != null)
                            {
                                Debug.Log("<- " + inputLine);
                                // split the lines sent from the server by spaces (seems to be the easiest way to parse them)
                                string[] splitInput = inputLine.Split(new Char[] { ' ' });

                                if (splitInput[0] == "PING")
                                {
                                    string PongReply = splitInput[1];
                                    //Console.WriteLine("->PONG " + PongReply);
                                    writer.WriteLine("PONG " + PongReply);
                                    writer.Flush();
                                    //continue;
                                }

                                switch (splitInput[1])
                                {
                                    case "001":
                                        writer.WriteLine("JOIN #" + _channel);
                                        writer.Flush();
                                        break;
                                    default:
                                        break;
                                }

                                if (inputLine.Contains("thing"))
                                {
                                    countofThing++;
                                    Debug.Log(countofThing);
                                    writer.WriteLine(string.Format(messageFormat, _channel, "you tested the command!"));
                                    writer.Flush();
                                }

                                

                            }
                        }

                    }
                }
                catch (Exception e)
                {
                    // shows the exception, sleeps for a little while and then tries to establish a new connection to the IRC server
                    Debug.LogError(e.ToString());
                    Thread.Sleep(5000);
                    retry = ++retryCount <= _maxRetries;
                }
            } while (retry);
        }

        public void OnEnd()
        {
            stop = true;
        }

        public void Connect()
        {

        }

        public void Disconnect()
        {

        }
    }
}