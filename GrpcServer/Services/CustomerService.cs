using Grpc.Core;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GrpcServer.Services
{
    public class CustomerService : Customer.CustomerBase
    {
        private readonly ILogger<CustomerService> _logger;

        public CustomerService(ILogger<CustomerService> logger)
        {
            _logger = logger;
        }

        public override Task<CustomerModel> GetCustomerInfo(CustomerLookupModel request, ServerCallContext context)
        {
            var output = new CustomerModel();
            switch (request.UserId)
            {
                default:
                    output = DefaultCustomerModelResponse;
                    break;
            }

            return Task.FromResult(output);
        }

        public override async Task CreateCustomer(
            CreateCustomerResponse request, 
            IServerStreamWriter<CustomerModel> responseStream, 
            ServerCallContext context)
        {
            List<CustomerModel> customers = new List<CustomerModel>();
            customers.Add(DefaultCustomerModelResponse);
            customers.Add(DefaultCustomerModelResponse);
            customers.Add(DefaultCustomerModelResponse);

            foreach (var customer in customers)
            {
                await Task.Delay(3000);
                await responseStream.WriteAsync(customer);
            }
        }

        private static CustomerModel DefaultCustomerModelResponse =>
            new CustomerModel
            {
                FirstName = "Found Name",
                LastName = "Found LastName",
                Age = 10,
                EmailAddress = "Found Email",
                IsAlive = true
            };
    }
}
