namespace BurgerRoyale.Payment.BehaviorTests.StepDefinitions;

[Binding]
public class MakePaymentStepDefinitions(
    ScenarioContext context,
    PaymentClient client)
{
    [When(@"I make a payment")]
    public async Task WhenIMakeAPayment()
    {
        var addPaymentResponse = context.Get<RequestPaymentResponse>();

        Guid paymentId = addPaymentResponse.PaymentId;

        PayPaymentResponse? response = await client.PayAsync(paymentId);

        context.Set(response);
    }

    [Then(@"the payment should be paid")]
    public async Task ThenThePaymentShouldBePaid()
    {
        var response = context.Get<PayPaymentResponse>();
        response.Should().NotBeNull();
        response.IsValid.Should().BeTrue();

        var addPaymentResponse = context.Get<RequestPaymentResponse>();

        Guid paymentId = addPaymentResponse.PaymentId;

        GetPaymentResponse paymentResponse = await client.GetPaymentByIdAsync(paymentId);
        paymentResponse.Should().NotBeNull();
        paymentResponse.Status.Should().Be(PaymentStatus._2);
    }
}