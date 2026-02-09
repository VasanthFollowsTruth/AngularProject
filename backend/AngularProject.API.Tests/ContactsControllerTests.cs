using System.Net;
using System.Net.Http.Json;
using Application.DTOs.Contacts;

namespace AngularProject.API.Tests;

public class ContactsControllerTests
    : IClassFixture<CustomWebApplicationFactory>
{
    private readonly HttpClient _client;

    public ContactsControllerTests(
        CustomWebApplicationFactory factory)
    {
        _client = factory.CreateClient();

        var token = TestAuthHelper.GenerateTestToken();

        _client.DefaultRequestHeaders.Authorization =
            new System.Net.Http.Headers.AuthenticationHeaderValue(
                "Bearer",
                token);
    }

    [Fact]
    public async Task GetAll_ReturnsOk()
    {
        var response =
            await _client.GetAsync("/api/contacts");
            
        Assert.Equal(
            HttpStatusCode.OK,
            response.StatusCode);
    }

    [Fact]
    public async Task Create_ReturnsBadRequest_WhenInvalid()
    {
        var dto = new ContactCreateDto
        {
            FirstName = "",
            LastName = "",
            Email = "invalid",
            PhoneNumber = "123"
        };

        var response =
            await _client.PostAsJsonAsync(
                "/api/contacts",
                dto);

        Assert.Equal(
            HttpStatusCode.BadRequest,
            response.StatusCode);
    }

    [Fact]
    public async Task GetById_ReturnsOk_WhenExists()
    {
        var create = new ContactCreateDto
        {
            FirstName = "Test",
            LastName = "User",
            Email = "getbyid@test.com",
            PhoneNumber = "+919999999999",
            Address = "Street",
            City = "City",
            State = "State",
            Country = "India",
            PostalCode = "600001"
        };

        var createResponse =
            await _client.PostAsJsonAsync("/api/contacts", create);

        var created =
            await createResponse.Content.ReadFromJsonAsync<ContactReadDto>();

        var response =
            await _client.GetAsync($"/api/contacts/{created!.Id}");

        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
    }

    [Fact]
    public async Task GetById_ReturnsNotFound_WhenMissing()
    {
        var response =
            await _client.GetAsync("/api/contacts/9999");

        Assert.Equal(
            HttpStatusCode.NotFound,
            response.StatusCode);
    }

    [Fact]
    public async Task Create_ReturnsCreated_WhenValid()
    {
        var dto = new ContactCreateDto
        {
            FirstName = "Test",
            LastName = "User",
            Email = "testuser@test.com",
            PhoneNumber = "+919888888888",
            Address = "Test Street",
            City = "Chennai",
            State = "TN",
            Country = "India",
            PostalCode = "600002"
        };

        var response =
            await _client.PostAsJsonAsync(
                "/api/contacts",
                dto);

        Assert.Equal(HttpStatusCode.Created, response.StatusCode);
    }

    [Fact]
    public async Task Create_ReturnsBadRequest_WhenDuplicateEmail()
    {
        var dto = new ContactCreateDto
        {
            FirstName = "Test",
            LastName = "User",
            Email = "dup@test.com",
            PhoneNumber = "+919999999999",
            Address = "Street",
            City = "City",
            State = "State",
            Country = "India",
            PostalCode = "600001"
        };

        await _client.PostAsJsonAsync("/api/contacts", dto);

        var response =
            await _client.PostAsJsonAsync("/api/contacts", dto);

        Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
    }

    [Fact]
    public async Task Update_ReturnsNoContent_WhenValid()
    {
        var dto = new ContactUpdateDto
        {
            FirstName = "Updated",
            LastName = "Name",
            Email = "john@test.com",
            PhoneNumber = "+919999999999",
            Address = "New Street",
            City = "Chennai",
            State = "TN",
            Country = "India",
            PostalCode = "600001"
        };

        var response =
            await _client.PutAsJsonAsync(
                "/api/contacts/1",
                dto);

        Assert.Equal(
            HttpStatusCode.NoContent,
            response.StatusCode);
    }

    [Fact]
    public async Task Update_ReturnsNotFound_WhenMissing()
    {
        var dto = new ContactUpdateDto
        {
            FirstName = "Test",
            LastName = "User",
            Email = "missing1@test.com",
            PhoneNumber = "+91999999999",
            Address = "Street",
            City = "City",
            State = "State",
            Country = "India",
            PostalCode = "600001"
        };

        var response =
            await _client.PutAsJsonAsync(
                "/api/contacts/9999",
                dto);

        Assert.Equal(
            HttpStatusCode.NotFound,
            response.StatusCode);
    }

    [Fact]
    public async Task Delete_ReturnsNoContent_WhenValid()
    {
        var createDto = new ContactCreateDto
        {
            FirstName = "Delete",
            LastName = "Test",
            Email = "delete@test.com",
            PhoneNumber = "+919999999999",
            Address = "Street",
            City = "City",
            State = "State",
            Country = "India",
            PostalCode = "600001"
        };

        var createResponse =
            await _client.PostAsJsonAsync("/api/contacts", createDto);

        var created =
            await createResponse.Content.ReadFromJsonAsync<ContactReadDto>();

        var deleteResponse =
            await _client.DeleteAsync($"/api/contacts/{created!.Id}");

        Assert.Equal(HttpStatusCode.NoContent, deleteResponse.StatusCode);
    }


    [Fact]
    public async Task Delete_ReturnsNotFound_WhenMissing()
    {
        var response =
            await _client.DeleteAsync(
                "/api/contacts/9999");

        Assert.Equal(
            HttpStatusCode.NotFound,
            response.StatusCode);
    }
}
