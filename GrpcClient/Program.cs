using Grpc.Net.Client;
using GrpcServer;
using System;
using System.Threading.Tasks;

namespace GrpcClient
{
    class Program
    {
        static async Task Main(string[] args)
        {
            ////CONFIGURE THE SERVER
            #region Client Test 1
            //var channel = GrpcChannel.ForAddress("https://localhost:5001");
            //var client = new Greeter.GreeterClient(channel);

            //var input = new HelloRequest{ Name = "Jean"};
            //var reply = await client.SayHelloAsync(input);

            //Console.WriteLine(reply.Message);

            #endregion

            var channel = GrpcChannel.ForAddress("https://localhost:5001");
            var customerClient = new Customer.CustomerClient(channel);

            var clientrequested = new CustomerLookupModel { UserId = 1 };

            var customerResponse = await customerClient.GetCustomerInfoAsync(clientrequested);

            Console.WriteLine(customerResponse.FirstName);
            Console.WriteLine(customerResponse.LastName);
            Console.WriteLine(customerResponse.Age);
            Console.WriteLine(customerResponse.EmailAddress);
            Console.ReadKey();
        }
    }
}
