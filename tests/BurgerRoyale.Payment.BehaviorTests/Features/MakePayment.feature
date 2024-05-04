Feature: MakePayment

Scenario: Make payment
	Given I just ordered a product
	And I request a payment
	When I make a payment
	Then the payment should be paid