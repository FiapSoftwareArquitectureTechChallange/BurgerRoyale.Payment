namespace BurgerRoyale.Payment.BehaviorTests.StepDefinitions;

[Binding]
public class RequestPaymentStepDefinitions(
    ScenarioContext context,
    PaymentClient client)
{
    [Given(@"I just ordered a product")]
    public void GivenIJustOrderedAProduct()
    {
        context["OrderId"] = Guid.NewGuid();

        context["OrderPrice"] = 35.79M;
    }

    [Given(@"I request a payment")]
    [When(@"I request a payment")]
    public async Task WhenIRequestAPayment()
    {
        Guid orderId = context.Get<Guid>("OrderId");

        decimal orderPrice = context.Get<decimal>("OrderPrice");

        var request = new RequestPaymentRequest
        {
            OrderId = orderId,
            Amount = double.Parse(orderPrice.ToString())
        };

        RequestPaymentResponse response = await client.RequestPaymentAsync(request);
        context.Set(response);
    }

    [Then(@"the payment should be created")]
    public async Task ThenThePaymentShouldBeCreated()
    {
        var response = context.Get<RequestPaymentResponse>();
        response.Should().NotBeNull();
        response.IsValid.Should().BeTrue();

        Guid paymentId = response.PaymentId;

        GetPaymentResponse paymentResponse = await client.GetPaymentByIdAsync(paymentId);
        paymentResponse.Should().NotBeNull();
        paymentResponse.IsValid.Should().BeTrue();

        paymentResponse.Id.Should().Be(paymentId);

        Guid orderId = context.Get<Guid>("OrderId");
        decimal orderPrice = context.Get<decimal>("OrderPrice");

        paymentResponse.OrderId.Should().Be(orderId);
        paymentResponse.Value.Should().Be(double.Parse(orderPrice.ToString()));
    }
}