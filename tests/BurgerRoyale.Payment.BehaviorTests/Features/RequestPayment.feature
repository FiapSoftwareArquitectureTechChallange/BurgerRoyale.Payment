Feature: RequestPayment

Scenario: Request a Payment
	Given I just order a product
	When I request a payment
	Then the payment should be created