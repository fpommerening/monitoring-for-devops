
using CustomerService.Business;
using FP.Monitoring.All.Contract;
using Grpc.Core;

namespace CustomerService.Services;

public class CustomerServices(
  IAddressProcessor addressProcessor,
  ICustomerProcessor customerProcessor,
  ICreditRateProvider creditRateProvider)
  : FP.Monitoring.All.Contract.CustomerServices.CustomerServicesBase
{
  public override async Task<RateCustomerResponse> RateCustomer(RateCustomerRequest request, ServerCallContext context)
  {
    var address = new Address(request.Street, request.Town, request.ZipCode);
    var addressIsValid = await addressProcessor.IsValidAsync(address);
    if (addressIsValid)
    {
      var customer = await customerProcessor.ValidateAsync(request.Firstname, request.Name);
      if (customer.IsValid)
      {
        var reliability = await creditRateProvider.GetRateValueAsync(customer, address);
        return new RateCustomerResponse
        {
          Reliability = reliability,
          IsValid = true
        };
      }
    }
  
    return new RateCustomerResponse { Reliability = 0, IsValid = false };
  }
}