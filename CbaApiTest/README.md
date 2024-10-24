# CbaApiTest Introduction

This project has been created for automation assesment.  This project contains API automation

## Technology Stack used for this project

* Specflow for BDD
* C#.Net core and Nunit
* RestSharp for HTTP Client
* Fluent Assertion

## Prerequisites to run tests in local machine

* Install Visual Studio 
* Install dependencies - Specflow for Visual Studio 2022 

### Executing test case as per the given Sequence 

* Go to the project folder and open .sln file through Visual studio
* Build the solution
* Go to test tab on VS and select Test Explorer
* To run all tests - Right click on the PetFeature and run (This runs the test cases as per the given sequence) (OR)
* To run individual test - Exapand test file and right click on specific test case, then either debug or run the test case
* Note: Please always run DELETE Pet testcase at the last

### Further expansion of the test suit

Basic future proof frame work has been set and this is reusable for any endpoints.

* Negative scenarios
* More positive scenarios
* Can add File readable function to read data from external files
* Some of the step definitions can be moved to common class to make test suite more reusable
* Test cases can be scheduled to run parallely
* Specflow living doc report functionolity can be added


