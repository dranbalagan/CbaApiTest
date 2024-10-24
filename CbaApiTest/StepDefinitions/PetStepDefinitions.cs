using CbaApiTest.Models;
using CbaApiTest.Support;
using Newtonsoft.Json;
using RestSharp;
using System.Net;
using TechTalk.SpecFlow;
using static CbaApiTest.Models.PostNewPetRequestAndResponseModel;

namespace CbaApiTest.StepDefinitions
{
    [Binding]
    public sealed class PetStepDefinitions
    {
        RestResponse restResponse;
        DeleteAndUpdatedPetResponseModel deserializedActualResponse;

        //POST - Add a new pet 
        [When(@"I call POST request with '([^']*)'")]
        public void WhenICallPOSTRequestWith(int petId)
        {
            var url = "https://petstore.swagger.io/v2/pet";

            var requestBody = new PostNewPetRequestAndResponseModel()
            {
                name = "Dtest",
                photoUrls = new List<string>(new string[]{ "p1", "p2" }),
                id = petId,
                category = new Category()
                {
                    id= 450,
                    name = ""
                },
                tags = new List<Tag> { new Tag
                {
                     id= 451,
                    name = ""
                },
                new Tag
                {
                    id= 452,
                    name = ""
                }      
                },
                status = "available"
            };

            restResponse = HttpRestClient.ExecuteRequest(Method.Post, url, requestBody);
        }

        //GET - Retrive a pet by petId
        [When(@"I call GET petid request with '([^']*)'")]
        public void WhenICallGETPetidRequestWith(int petId)
        {
            var url = $"https://petstore.swagger.io/v2/pet/{petId}";
            restResponse = HttpRestClient.ExecuteRequest(Method.Get, url);
        }

        //GET - Retrive all pets by status
        [When(@"I call GET status request with '([^']*)'")]
        public void WhenICallGETStatusRequestWith(string status)
        {
            var url = $"https://petstore.swagger.io/v2/pet/findByStatus?status={status}";
            restResponse = HttpRestClient.ExecuteRequest(Method.Get,url);
        }

        [Then(@"I should see only the filtered '([^']*)' in the response body")]
        public void ThenIShouldSeeOnlyTheFilteredInTheResponseBody(string status)
        {
            var PetPostResponse = JsonConvert.DeserializeObject<List<GetStatusResponseModel>>(restResponse.Content);
            var IsExpectedStatus = PetPostResponse.All(s => s.status == status);
            IsExpectedStatus.Should().BeTrue();
        }

        //PUT - Update an existing pet status
        [When(@"I call PUT request to update an existing pet status '([^']*)' for PetId '([^']*)'")]
        public void WhenICallPUTRequestToUpdateAnExistingPetStatusForPetId(string status, int petId)
        {
            var url = "https://petstore.swagger.io/v2/pet";

            var requestBody = new PostNewPetRequestAndResponseModel()
            {
                name = "Dtest1",
                photoUrls = new List<string>(new string[] { "p1", "p2" }),
                id = petId,
                category = new Category()
                {
                    id = 450,
                    name = ""
                },
                tags = new List<Tag> { new Tag
                {
                     id= 451,
                    name = ""
                },
                new Tag
                {
                    id= 452,
                    name = ""
                }
                },
                status = "sold"
            };
            restResponse = HttpRestClient.ExecuteRequest(Method.Put, url, requestBody);
        }

        [Then(@"I should see the updated status '([^']*)' in the response body")]
        public void ThenIShouldSeeTheUpdatedStatusInTheResponseBody(string status)
        {
            var PetPostResponse = JsonConvert.DeserializeObject<PostNewPetRequestAndResponseModel>(restResponse.Content);
            PetPostResponse.status.Should().Be(status);
        }

        //POST - Upload an image by petId
        [When(@"I call POST request to upload an image for a '([^']*)'")]
        public void WhenICallPOSTRequestToUploadAnImageForA(int petId)
        {
            var url = $"https://petstore.swagger.io/v2/pet/{petId}/uploadImage";
            string pathToFile = Path.Combine(Environment.CurrentDirectory, @"Files", "parrot.jpg");
            var formData = new Dictionary<string, string>
            {
                { "additionalMetadata","my pet"},
                {"file", pathToFile}
            };
            restResponse = HttpRestClient.ExecuteRequest(Method.Post, url, null,null,null,null,formData);
        }

        //POST - Post call to update form data name for a pet by petId
        [When(@"I call POST request to update an existing pet name '([^']*)' for PetId '([^']*)'")]
        public void WhenICallPOSTRequestToUpdateAnExistingPetNameForPetId(string updatedPetName, int petId)
        {
            var url = $"https://petstore.swagger.io/v2/pet/{petId}";
            var formDataEncodedParams = new Dictionary<string, string>
            {
                 { "name", updatedPetName}
            };
            restResponse = HttpRestClient.ExecuteRequest(Method.Post,url, null,null,null,formDataEncodedParams,null);
        }

        [Then(@"I should see the updated PetId '([^']*)' in the response body")]
        public void ThenIShouldSeeTheUpdatedPetIdInTheResponseBody(string updatedPetId)
        {
            deserializedActualResponse = JsonConvert.DeserializeObject<DeleteAndUpdatedPetResponseModel>(restResponse.Content);
            deserializedActualResponse.message.Should().Be(updatedPetId);
        }

        //DELETE - Delete a pet by petId
        [When(@"I call DELETE request to delete a pet for PetId '([^']*)'")]
        public void WhenICallDELETERequestToDeleteAPetForPetId(int petId)
        {
            var url = $"https://petstore.swagger.io/v2/pet/{petId}";
            restResponse = HttpRestClient.ExecuteRequest(Method.Delete, url);
        }

        [Then(@"I should see code '([^']*)' in the response body")]
        public void ThenIShouldSeeCodeInTheResponseBody(int responseBodyCode)
        {
            deserializedActualResponse = JsonConvert.DeserializeObject<DeleteAndUpdatedPetResponseModel>(restResponse.Content);
            deserializedActualResponse?.code.Should().Be(responseBodyCode);
        }

        [Then(@"I should see the deleted petId '([^']*)' in the response body")]
        public void ThenIShouldSeeTheDeletedPetIdInTheResponseBody(string deletedPetId)
        {
            deserializedActualResponse.message.Should().Be(deletedPetId);
        }

        //Common Steps to reuse
        [Then(@"I should see '([^']*)' response")]
        public void ThenIShouldSeeResponse(HttpStatusCode responseCode)
        {
            restResponse.StatusCode.Should().Be(responseCode);
        }

        [Then(@"I should see '([^']*)' in the response body")]
        public void ThenIShouldSeeInTheResponseBody(int petId)
        {
            var PetPostResponse = JsonConvert.DeserializeObject<PostNewPetRequestAndResponseModel>(restResponse.Content);
            PetPostResponse.id.Should().Be(petId);
        }
    }
}