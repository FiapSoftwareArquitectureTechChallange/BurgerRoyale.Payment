using BoDi;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.VisualStudio.TestPlatform.TestHost;

namespace BurgerRoyale.Payment.BehaviorTests.Hooks;

[Binding]
public sealed class EnvironmentSetupHooks
{
    [BeforeTestRun]
    public static void BeforeTestRun(IObjectContainer testThreadContainer)
    {
        HttpClient? apiHttpClient;

        var application = new WebApplicationFactory<Program>()
        .WithWebHostBuilder(builder =>
        {
            builder.UseEnvironment("Development");
        });

        apiHttpClient = application.CreateClient();

        var paymentClient =
            new PaymentClient("", apiHttpClient);

        testThreadContainer.RegisterInstanceAs(paymentClient);
    }
}