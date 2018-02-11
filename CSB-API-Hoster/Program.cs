using System;
using System.Diagnostics;
using Microsoft.Owin.Hosting;

namespace CSB_API_Hoster
{
    class Program
    {
        private static int port = 5556;

        static void Main(string[] args)
        {
            Console.WriteLine("CSB Unlimited - Web API Host");
            Console.WriteLine("(c) CSB Unlimited 2018\n");
            Console.WriteLine("-----------------------------------------------\n");

            Console.WriteLine("Please wait. Web Service is Starting...");

            int tries = 0;
            AppStart:

            try
            {
                using (WebApp.Start<Startup>("http://localhost:" + port.ToString()))
                {
                    Console.WriteLine("Web API Service is Running...\n");
                    Console.WriteLine(@"URL - http://localhost:" + port.ToString() + "/api/user/\n\tGET, GET(id), POST(UserRequest), PUT(UserRequest), DELETE(id) avaiable\n");
                    Console.WriteLine("Web API Tracking Started...\n");
                    Console.WriteLine("-----------------------------------------------");
                    Console.WriteLine("Only accept and return - application/json.\n");
                    Console.WriteLine("Example POST Request :\n" +
                        " { \n" +
                        "\t\"UserVM\": [ { \"Id\":0,\"FirstName\":\"Chathuranga\",\"LastName\":\"Basnayake\",\"Gender\":\"M\",\"Mobile\":\"077xxxxxxx\" } ],\n" +
                        " }");
                    Console.WriteLine("Example Response :\n" +
                        " { \n" +
                        "\t\"UserVM\": { \"Id\":1,\"FirstName\":\"Chathuranga\",\"LastName\":\"Basnayake\",\"Gender\":\"M\",\"Mobile\":\"077xxxxxxx\" },\n" +
                        "\t\"IsSuccess\": true,\n" +
                        "\t\"Status\": 200,\n" +
                        " }");
                    Console.WriteLine("-----------------------------------------------\n");
                    do
                    {
                        Console.WriteLine("Enter 'q' to Stop Web API Service.\n");
                    } while (!(Console.ReadLine().ToLower()).Equals("q"));
                    Console.WriteLine("-----------------------------------------------\n");
                }
            }
            catch(Exception ex)
            {
                Debug.WriteLine(ex);
                Console.WriteLine("Failed to start Web API Service in Port {0}.", port);
                if (tries == 10)
                {
                    Console.Write("Facing some issues while starting Web API Service.\nDo you want to try again? (Yes - y | No - anything else) : ");

                    if (Console.ReadLine().ToLower().Equals("y"))
                        tries = 0;
                }
                if (tries < 10)
                {
                    tries++;
                    port++;
                    Console.WriteLine("Port Changed to {0}. And Trying again...", port);
                    Console.WriteLine();
                    goto AppStart;
                }
                Console.WriteLine();
            }

            Console.WriteLine("Web API Service Stopped...");
        }
    }
}
