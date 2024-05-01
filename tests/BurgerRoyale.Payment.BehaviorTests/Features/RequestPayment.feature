Feature: RequestPayment

Scenario: Request a Payment
	Given I just ordered a product
	When I request a payment
	Then the payment should be created