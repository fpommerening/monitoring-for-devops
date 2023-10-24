
using CustomerService.Business;
using FP.Monitoring.All.Contract;
using Grpc.Core;

namespace CustomerService.Services;

public class CustomerServices : FP.Monitoring.All.Contract.CustomerServices.CustomerServicesBase
{
  private readonly IAddressProcessor _addressProcessor;
  private readonly ICustomerProcessor _customerProcessor;
  private readonly ICreditRateProvider _creditRateProvider;
  
  public CustomerServices(IAddressProcessor addressProcessor,
    ICustomerProcessor customerProcessor,
    ICreditRateProvider creditRateProvider)
  {
    _addressProcessor = addressProcessor;
    _customerProcessor = customerProcessor;
    _creditRateProvider = creditRateProvider;
  }
  
  public override async Task<RateCustomerResponse> RateCustomer(RateCustomerRequest request, ServerCallContext context)
  {
    var address = new Address(request.Street, request.Town, request.ZipCode);
    var addressIsValid = await _addressProcessor.IsValidAsync(address);
    if (addressIsValid)
    {
      var customer = await _customerProcessor.ValidateAsync(request.Firstname, request.Name);
      if (customer.IsValid)
      {
        var reliability = await _creditRateProvider.GetRateValueAsync(customer, address);
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