using AutoFixture;
using Moq;
using TemplateProject.Services;
using TemplateProject.Repositories;
using TemplateProject.Repositories.Models;
using TemplateProject.Models;
using Microsoft.AspNetCore.Http;

namespace Api.Tests.Services;

public class UserServiceTests
{
    private readonly UserService _userService;
    private readonly Mock<IUserRepository> _mockUserRepository;
    private readonly Fixture _fixture;

    public UserServiceTests()
    {
        // fixture for creating test data
        _fixture = new Fixture();
        _fixture.Behaviors.Add(new OmitOnRecursionBehavior());

        // mock user repo dependency
        _mockUserRepository = new Mock<IUserRepository>();

        // service under test
        _userService = new UserService(_mockUserRepository.Object);
    }

    [Fact]
    public async Task GetAll_ReturnsAllUsers()
    {
        // Arrange
        var usersFixture = _fixture.CreateMany<User>(3);
        _mockUserRepository.Setup(x => x.GetAll()).ReturnsAsync(usersFixture);

        // Act
        var users = await _userService.GetAll();

        // Assert
        Assert.True(users.Count() == 3);
        Assert.Equal(usersFixture.First().FirstName, users.First().FirstName);
    }

    [Fact]
    public async Task GetById_ValidId_ReturnsUser()
    {
        // Arrange
        var userFixture = _fixture.Create<User>();
        var id = userFixture.Id;
        _mockUserRepository.Setup(x => x.GetById(id)).ReturnsAsync(userFixture);

        // Act
        var user = await _userService.GetById(id);

        // Assert
        Assert.NotNull(user);
        Assert.Equal(userFixture.FirstName, user.FirstName);
    }

    [Fact]
    public async Task GetById_InvalidId_ReturnsNull()
    {
        // Act & Assert
        var user = await _userService.GetById(100);

        Assert.Null(user);
    }

    [Fact]
    public async Task RegisterUser_WithoutConfirmPassword_ThrowsBadRequestException()
    {
        // Arrange
        var registerUser = new RegisterUser
        {
            ConfirmPassword = "",
            Email = "test@user.com",
            FirstName = "Test",
            LastName = "User",
            Mobile = "0412345678",
            Password = "Password",
            Phone = "0712345678",
            Username = "tester"
        };

        // Act & Assert
        await Assert.ThrowsAsync<BadHttpRequestException>(() => _userService.Register(registerUser));
    }

    [Fact]
    public async Task RegisterUser_WithoutEmail_ThrowsBadRequestException()
    {
        // Arrange
        var registerUser = new RegisterUser
        {
            ConfirmPassword = "Password",
            Email = "",
            FirstName = "Test",
            LastName = "User",
            Mobile = "0412345678",
            Password = "Password",
            Phone = "0712345678",
            Username = "tester"
        };

        // Act & Assert
        await Assert.ThrowsAsync<BadHttpRequestException>(() => _userService.Register(registerUser));
    }

    [Fact]
    public async Task RegisterUser_WithoutFirstName_ThrowsBadRequestException()
    {
        // Arrange
        var registerUser = new RegisterUser
        {
            ConfirmPassword = "Password",
            Email = "test@user.com",
            FirstName = "",
            LastName = "User",
            Mobile = "0412345678",
            Password = "Password",
            Phone = "0712345678",
            Username = "tester"
        };

        // Act & Assert
        await Assert.ThrowsAsync<BadHttpRequestException>(() => _userService.Register(registerUser));
    }

    [Fact]
    public async Task RegisterUser_WithoutLastName_ThrowsBadRequestException()
    {
        // Arrange
        var registerUser = new RegisterUser
        {
            ConfirmPassword = "Password",
            Email = "test@user.com",
            FirstName = "Test",
            LastName = "",
            Mobile = "0412345678",
            Password = "Password",
            Phone = "0712345678",
            Username = "tester"
        };

        // Act & Assert
        await Assert.ThrowsAsync<BadHttpRequestException>(() => _userService.Register(registerUser));
    }

    [Fact]
    public async Task RegisterUser_WithMismatchedPasswords_ThrowsBadRequestException()
    {
        // Arrange
        var registerUser = new RegisterUser
        {
            ConfirmPassword = "Password2",
            Email = "test@user.com",
            FirstName = "Test",
            LastName = "User",
            Mobile = "0412345678",
            Password = "Password1",
            Phone = "0712345678",
            Username = "tester"
        };

        // Act & Assert
        await Assert.ThrowsAsync<BadHttpRequestException>(() => _userService.Register(registerUser));
    }

    [Fact]
    public async Task RegisterUser_CreatesUser()
    {
        // Arrange
        _mockUserRepository.Setup(x => x.Create(It.IsAny<User>())).ReturnsAsync(new User
        {
            Id = 1,
            Email = "test@user.com",
            FirstName = "Test",
            LastName = "User",
            Mobile = "0412345678",
            Password = [],
            Phone = "0712345678",
            Username = "tester"
        });

        var registerUser = new RegisterUser
        {
            ConfirmPassword = "Password",
            Email = "test@user.com",
            FirstName = "Test",
            LastName = "User",
            Mobile = "0412345678",
            Password = "Password",
            Phone = "0712345678",
            Username = "tester"
        };

        // Act
        var user = await _userService.Register(registerUser);

        // Assert
        Assert.NotNull(user);
        Assert.NotEqual(0, user.Id);
        Assert.Equal("test@user.com", user.Email);
        Assert.Equal("Test", user.FirstName);
        Assert.Equal("User", user.LastName);
        Assert.Equal("tester", user.Username);
    }
}