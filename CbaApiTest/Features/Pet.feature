Feature: Pet

This feature file contains all pet endpoints

@Regression
Scenario Outline: 01) Verify user can add a new pet to the store
	When I call POST request with '<PetId>'
	Then I should see '200' response
	And I should see '<PetId>' in the response body
Examples: 
| PetId  |
| 206260 |

@Regression
Scenario Outline: 02) Verify user can find a pet by PetId
	When I call GET petid request with '<PetId>'
	Then I should see '200' response
	And I should see '<PetId>' in the response body
Examples: 
| PetId  |
| 206260 |

@Regression
Scenario Outline: 03) Verify user can filter pets using status
	When I call GET status request with '<Status>'
	Then I should see '200' response
	And I should see only the filtered '<Status>' in the response body
Examples: 
| Status    |
| available |
| sold      |
| pending   |

@Regression
Scenario: 04) Verify user can upload a pet image
	When I call POST request to upload an image for a '206260'
	Then I should see '200' response
	And I should see code '200' in the response body

@Regression
Scenario: 05) Verify user can update an existing pet status
	When I call PUT request to update an existing pet status 'sold' for PetId '206260'
	Then I should see '200' response
	And I should see the updated status 'sold' in the response body

@Regression
Scenario: 06) Verify user can update an existing pet name 
	When I call POST request to update an existing pet name 'UpdatedPetName' for PetId '206260'
	Then I should see '200' response
	And I should see the updated PetId '206260' in the response body 

@Regression
Scenario: 07) Verify user can delete a pet using petId
	When I call DELETE request to delete a pet for PetId '206260'
	Then I should see '200' response
	And I should see code '200' in the response body
	And I should see the deleted petId '206260' in the response body
