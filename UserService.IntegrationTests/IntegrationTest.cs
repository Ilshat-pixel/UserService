// UserService.Tests/IntegrationTests.cs
using System.Net;
using System.Text;
using Newtonsoft.Json;
using UserService.Application.Queries.GetUserDetails;
using UserService.Domain;
using UserService.IntegrationTests;
using UserService.WebApi;

public class IntegrationTests : IClassFixture<TestingWebAppFactory<Program>>
{
    private readonly TestingWebAppFactory<Program> _factory;

    public IntegrationTests(TestingWebAppFactory<Program> factory)
    {
        _factory = factory;
    }

    private HttpClient CreateClientWithDevice(string device)
    {
        var client = _factory.CreateClient();
        client.DefaultRequestHeaders.Add("x-Device", device);
        return client;
    }

    private async Task<HttpResponseMessage> CreateUserAsync(HttpClient client, User user)
    {
        var content = new StringContent(
            JsonConvert.SerializeObject(user),
            Encoding.UTF8,
            "application/json"
        );
        return await client.PostAsync("api/user/create", content);
    }

    [Fact]
    public async Task CreateUser_MailDevice_ValidData_ReturnsSuccess()
    {
        var client = CreateClientWithDevice("mail");

        var user = new User { FirstName = "John", Email = "john@example.com" };

        var response = await CreateUserAsync(client, user);

        response.EnsureSuccessStatusCode();
    }

    [Fact]
    public async Task CreateUser_MailDevice_MissingEmail_ReturnsBadRequest()
    {
        var client = CreateClientWithDevice("mail");

        var user = new User { FirstName = "John" };

        var response = await CreateUserAsync(client, user);

        Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
    }

    [Fact]
    public async Task CreateUser_MailDevice_InvalidEmailFormat_ReturnsBadRequest()
    {
        var client = CreateClientWithDevice("mail");

        var user = new User { FirstName = "John", Email = "invalid-email-format" };

        var response = await CreateUserAsync(client, user);

        Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
    }

    [Fact]
    public async Task CreateUser_MobileDevice_ValidData_ReturnsSuccess()
    {
        var client = CreateClientWithDevice("mobile");

        var user = new User { Phone = "71234567890" };

        var response = await CreateUserAsync(client, user);

        response.EnsureSuccessStatusCode();
    }

    [Fact]
    public async Task CreateUser_MobileDevice_MissingPhoneNumber_ReturnsBadRequest()
    {
        var client = CreateClientWithDevice("mobile");

        var user = new User { };

        var response = await CreateUserAsync(client, user);

        Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
    }

    [Fact]
    public async Task CreateUser_MobileDevice_InvalidPhoneNumberFormat_ReturnsBadRequest()
    {
        var client = CreateClientWithDevice("mobile");

        var user = new User { Phone = "123456" };

        var response = await CreateUserAsync(client, user);

        Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
    }

    [Fact]
    public async Task CreateUser_WebDevice_ValidData_ReturnsSuccess()
    {
        var client = CreateClientWithDevice("web");

        var user = new User
        {
            FirstName = "Jane",
            LastName = "Doe",
            BirthDay = DateOnly.Parse("1990-01-01"),
            PassportId = "1234 567890",
            BirthPlace = "Somewhere",
            RegistrationAddress = "Some address"
        };

        var response = await CreateUserAsync(client, user);

        response.EnsureSuccessStatusCode();
    }

    [Fact]
    public async Task CreateUser_WebDevice_MissingRequiredFields_ReturnsBadRequest()
    {
        var client = CreateClientWithDevice("web");

        var user = new User { FirstName = "Jane", LastName = "Doe" };

        var response = await CreateUserAsync(client, user);

        Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
    }

    [Fact]
    public async Task CreateUser_WebDevice_InvalidPassportNumberFormat_ReturnsBadRequest()
    {
        var client = CreateClientWithDevice("web");

        var user = new User
        {
            FirstName = "Jane",
            LastName = "Doe",
            BirthDay = DateOnly.Parse("1990-01-01"),
            PassportId = "invalid-passport",
            BirthPlace = "Somewhere",

            RegistrationAddress = "Some address"
        };

        var response = await CreateUserAsync(client, user);

        Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
    }

    [Fact]
    public async Task GetUser_ReturnsUser()
    {
        var client = CreateClientWithDevice("mail");

        var user = new User { FirstName = "John", Email = "john@example.com" };

        var createResponse = await CreateUserAsync(client, user);
        createResponse.EnsureSuccessStatusCode();

        var createdUserId = JsonConvert.DeserializeObject<Guid>(
            await createResponse.Content.ReadAsStringAsync()
        );

        var getResponse = await client.GetAsync($"/api/user/get?{createdUserId}");
        var createdUser = JsonConvert.DeserializeObject<UserDetailsVm>(
            await getResponse.Content.ReadAsStringAsync()
        );
        getResponse.EnsureSuccessStatusCode();

        var retrievedUser = JsonConvert.DeserializeObject<User>(
            await getResponse.Content.ReadAsStringAsync()
        );

        Assert.Equal(createdUser.Id, retrievedUser.Id);
        Assert.Equal(createdUser.FirstName, retrievedUser.FirstName);
        Assert.Equal(createdUser.Email, retrievedUser.Email);
    }
}
