using Grpc.Net.Client;
using Microservice.Demo.GrpcServiceHost;
using System;
using System.Threading;
using Microservice.Demo.GrpcServiceHost.Protos;

namespace Microservice.Demo.GrpcClient
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            Thread.Sleep(1000*5);
            var channel = GrpcChannel.ForAddress("https://localhost:5001");
            var client = new Greeter.GreeterClient(channel);
            var reply = client.SayHelloAsync(
                new HelloRequest { Name = "晓晨" }).GetAwaiter().GetResult();
            Console.WriteLine("Greeter 服务返回数据: " + reply.Message);

            var msgClient = new Verification.VerificationClient(channel);
            var response = msgClient.SendMessage(new VerificationRequest()
                {BizCode = 1, Code = "1221", To = "13585593933"});

            Console.WriteLine($"{response.Ok}");

            Console.ReadKey();
        }
    }
}
