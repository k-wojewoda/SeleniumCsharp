Feature: BookFlight
	In order to book flight
	As a user of the website
	I want to be able to search for flights, reserve flight and pay

@Case1
Scenario: Flight search
	Given user is on the home page
	And user goes to Flights tab
	And user enters flights booking data
	When user presses Search
	Then the search results are sorted by price ascending, departures and arrivals cities are correct
	# Miami default change to Munich !!!!!!!!!

@Case2
Scenario: Flight booking
	Given user is on the home page
	And user goes to Flights tab
	And user enters flights booking data
	When user presses Search
	And user sees on a search result page departure and arrival times
	And user chooses flight by pressing Book Now
	Then on the checkout page booking summary displays correct information	

@Case3
Scenario: Fastest flight booking and payment
	Given user is on the home page
	And user goes to Flights tab
	And user enters flights booking data
	When user presses Search
	And user chooses fastest flight by pressing Book Now