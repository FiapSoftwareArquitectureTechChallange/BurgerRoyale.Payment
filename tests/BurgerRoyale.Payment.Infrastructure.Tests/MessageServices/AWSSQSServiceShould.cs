using Amazon.SQS;
using Amazon.SQS.Model;
using BurgerRoyale.Payment.Infrastructure.BackgroundMessage;
using Moq;

namespace BurgerRoyale.Payment.Infrastructure.Tests.MessageServices;

internal class AWSSQSServiceShould
{
    private Mock<IAmazonSQS> awsClientMock;
    
    private AWSSQSService service;

    [SetUp]
    public void SetUp()
    {
        awsClientMock = new Mock<IAmazonSQS>();

        service = new AWSSQSService(awsClientMock.Object);
    }

    [Test]
    public async Task Create_Queue_When_Does_Not_Exist()
    {
        #region Arrange(Given)

        string queueName = "myqueue";
        string queueUrl = $"http://localhost/{queueName}";

        string message = "my Message";
        string messageId = Guid.NewGuid().ToString();

        awsClientMock
            .Setup(x => x.GetQueueUrlAsync(
                It.IsAny<GetQueueUrlRequest>(),
                It.IsAny<CancellationToken>()
            ))
            .ThrowsAsync(new QueueDoesNotExistException("Exception message"));

        awsClientMock
            .Setup(x => x.CreateQueueAsync(
                It.IsAny<CreateQueueRequest>(),
                It.IsAny<CancellationToken>()
            ))
            .ReturnsAsync(new CreateQueueResponse()
            {
                QueueUrl = queueUrl
            });

        awsClientMock
            .Setup(x => x.SendMessageAsync(
                It.IsAny<string>(),
                It.IsAny<string>(),
                It.IsAny<CancellationToken>()
            ))
            .ReturnsAsync(new SendMessageResponse()
            {
                MessageId = messageId
            });

        #endregion

        #region Act(When)

        string response = await service.SendMessageAsync(queueName, message);

        #endregion

        #region Assert(Then)

        Assert.That(response, Is.Not.Null);
        Assert.That(response, Is.EqualTo(messageId));

        #endregion
    }
}