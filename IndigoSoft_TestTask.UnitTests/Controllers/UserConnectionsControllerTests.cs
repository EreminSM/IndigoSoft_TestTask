using FakeItEasy;
using IndigoSoft_TestTask.API.Controllers;
using IndigoSoft_TestTask.API.Models;
using IndigoSoft_TestTask_EFCore.DataAccess.PostgresSQL.Interfaces;
using Microsoft.AspNetCore.Http;
using Xunit;

namespace IndigoSoft_TestTask.UnitTests.Controllers;

public class UserConnectionsControllerTests
{
    private readonly IDataReposity _fakeDataRepository;
    private readonly UserConnectionsController _controller;
    public UserConnectionsControllerTests()
    {
        _fakeDataRepository = A.Fake<IDataReposity>();
        _controller = new UserConnectionsController(_fakeDataRepository);
    }

    #region AddUser
    [Fact]
    public async Task When_AddUserCallsWithValidRequest_ExpectedDataReposityMethodCallShouldHappen()
    {
        // Arrange
        var accountNumber = 111222333;

        // Act
        await _controller.AddUser(accountNumber);

        // Asset
        A.CallTo(() => _fakeDataRepository.AddUser(A<long>._)).MustHaveHappened();
    }

    [Fact]
    public async Task When_AddUserCallsWithNegativeAccountNumberInRequest_ExpectedDataReposityMethodCallShouldNotHappen()
    {
        // Arrange
        var accountNumber = -1;

        // Act & assert
        await Assert.ThrowsAsync<BadHttpRequestException>(async () => await _controller.AddUser(accountNumber));
        A.CallTo(() => _fakeDataRepository.AddUser(A<long>._)).MustNotHaveHappened();
    }

    [Fact]
    public async Task When_AddUserCallsWithAzeroAccountNumberInRequest_ExpectedDataReposityMethodCallShouldNotHappen()
    {
        // Arrange
        var accountNumber = 0;

        // Act & assert
        await Assert.ThrowsAsync<BadHttpRequestException>(async () => await _controller.AddUser(accountNumber));
        A.CallTo(() => _fakeDataRepository.AddUser(A<long>._)).MustNotHaveHappened();
    }
    #endregion

    #region GetUsersByPartOfIpAddress
    [Fact]
    public async Task When_GetUsersByPartOfIpAddressCallsWithValidRequest_ExpectedDataReposityMethodCallShouldHappen()
    {
        // Arrange
        var partOfIpAddress = "176.123.";

        // Act
        await _controller.GetUsersByPartOfIpAddress(partOfIpAddress);

        // Asset
        A.CallTo(() => _fakeDataRepository.GetUsersByPartOfIp(A<string>._)).MustHaveHappened();
    }

    [Fact]
    public async Task When_GetUsersByPartOfIpAddressCallsWithInvalidRequest_ExpectedDataReposityMethodCallShouldNotHappen()
    {
        // Arrange
        var partOfIpAddress = "ase3ff";

        // Act & assert
        await Assert.ThrowsAsync<BadHttpRequestException>(async () => await _controller.GetUsersByPartOfIpAddress(partOfIpAddress));
        A.CallTo(() => _fakeDataRepository.GetUsersByPartOfIp(A<string>._)).MustNotHaveHappened();
    }

    [Fact]
    public async Task When_GetUsersByPartOfIpAddressCallsWithEmptyPartOfIpInRequest_ExpectedDataReposityMethodCallShouldNotHappen()
    {
        // Arrange
        var partOfIpAddress = "";

        // Act & assert
        await Assert.ThrowsAsync<BadHttpRequestException>(async () => await _controller.GetUsersByPartOfIpAddress(partOfIpAddress));
        A.CallTo(() => _fakeDataRepository.GetUsersByPartOfIp(A<string>._)).MustNotHaveHappened();
    }
    #endregion

    #region GetAllIpAddressesForAccount
    [Fact]
    public async Task When_GetAllIpAddressesForAccountCallsWithValidRequest_ExpectedDataReposityMethodCallShouldHappen()
    {
        // Arrange
        var accountNumber = 111222333;

        // Act
        await _controller.GetAllIpAddressesForAccount(accountNumber);

        // Asset
        A.CallTo(() => _fakeDataRepository.GetIpAddressesByAccountId(A<long>._)).MustHaveHappened();
    }

    [Fact]
    public async Task When_GetAllIpAddressesForAccountCallsWithNegativeAccountNumberInRequest_ExpectedDataReposityMethodCallShouldNotHappen()
    {
        // Arrange
        var accountNumber = -1;

        // Act & assert
        await Assert.ThrowsAsync<BadHttpRequestException>(async () => await _controller.GetAllIpAddressesForAccount(accountNumber));
        A.CallTo(() => _fakeDataRepository.GetIpAddressesByAccountId(A<long>._)).MustNotHaveHappened();
    }

    [Fact]
    public async Task When_GetAllIpAddressesForAccountCallsWithAzeroAccountNumberInRequest_ExpectedDataReposityMethodCallShouldNotHappen()
    {
        // Arrange
        var accountNumber = 0;

        // Act & assert
        await Assert.ThrowsAsync<BadHttpRequestException>(async () => await _controller.GetAllIpAddressesForAccount(accountNumber));
        A.CallTo(() => _fakeDataRepository.GetIpAddressesByAccountId(A<long>._)).MustNotHaveHappened();
    }
    #endregion

    #region AddUserConnection
    [Fact]
    public async Task When_AddUserConnectionCallsWithValidRequest_ExpectedDataReposityMethodCallShouldHappen()
    {
        // Arrange
        var request = new UserConnectionEvent()
        {
            IpAddress = "176.15.167.59",
            UserAccountNumber = 111222333
        };

        // Act
        await _controller.AddUserConnection(request);

        // Asset
        A.CallTo(() => _fakeDataRepository.AddConnectionEvent(A<long>._, A<string>._)).MustHaveHappened();
    }

    [Fact]
    public async Task When_AddUserConnectionCallsWithNegativeAccountNumberInRequest_ExpectedDataReposityMethodCallShouldNotHappen()
    {
        // Arrange
        var request = new UserConnectionEvent()
        {
            IpAddress = "176.15.167.59",
            UserAccountNumber = -1
        };

        // Act & assert
        await Assert.ThrowsAsync<BadHttpRequestException>(async () => await _controller.AddUserConnection(request));
        A.CallTo(() => _fakeDataRepository.AddConnectionEvent(A<long>._, A<string>._)).MustNotHaveHappened();
    }

    [Fact]
    public async Task When_AddUserConnectionCallsWithAzeroAccountNumberInRequest_ExpectedDataReposityMethodCallShouldNotHappen()
    {
        // Arrange
        var accountNumber = new UserConnectionEvent()
        {
            IpAddress = "176.15.167.59",
            UserAccountNumber = 0
        };

        // Act & assert
        await Assert.ThrowsAsync<BadHttpRequestException>(async () => await _controller.AddUserConnection(accountNumber));
        A.CallTo(() => _fakeDataRepository.AddConnectionEvent(A<long>._, A<string>._)).MustNotHaveHappened();
    }


    [Fact]
    public async Task When_AddUserConnectionCallsWithInvalidIpInRequest_ExpectedDataReposityMethodCallShouldNotHappen()
    {
        // Arrange
        var request = new UserConnectionEvent()
        {
            IpAddress = "sddsd@24",
            UserAccountNumber = 111222333
        };

        // Act & assert
        await Assert.ThrowsAsync<BadHttpRequestException>(async () => await _controller.AddUserConnection(request));
        A.CallTo(() => _fakeDataRepository.AddConnectionEvent(A<long>._, A<string>._)).MustNotHaveHappened();
    }

    [Fact]
    public async Task When_AddUserConnectionCallsWithEmptyIpInRequest_ExpectedDataReposityMethodCallShouldNotHappen()
    {
        // Arrange
        var request = new UserConnectionEvent()
        {
            IpAddress = "",
            UserAccountNumber = 111222333
        };

        // Act & assert
        await Assert.ThrowsAsync<BadHttpRequestException>(async () => await _controller.AddUserConnection(request));
        A.CallTo(() => _fakeDataRepository.AddConnectionEvent(A<long>._, A<string>._)).MustNotHaveHappened();
    }
    #endregion

    #region GetLatestConnectionInfoForAccount
    [Fact]
    public async Task When_GetLatestConnectionInfoForAccountCallsWithValidRequest_ExpectedDataReposityMethodCallShouldHappen()
    {
        // Arrange
        var accountNumber = 111222333;

        // Act
        await _controller.GetLatestConnectionInfoForAccount(accountNumber);

        // Asset
        A.CallTo(() => _fakeDataRepository.GetIpAddressAndTimeOfLatestConnection(A<long>._)).MustHaveHappened();
    }

    [Fact]
    public async Task When_GetLatestConnectionInfoForAccountCallsWithNegativeAccountNumberInRequest_ExpectedDataReposityMethodCallShouldNotHappen()
    {
        // Arrange
        var accountNumber = -1;

        // Act & assert
        await Assert.ThrowsAsync<BadHttpRequestException>(async () => await _controller.GetLatestConnectionInfoForAccount(accountNumber));
        A.CallTo(() => _fakeDataRepository.GetIpAddressAndTimeOfLatestConnection(A<long>._)).MustNotHaveHappened();
    }

    [Fact]
    public async Task When_GetLatestConnectionInfoForAccountCallsWithAzeroAccountNumberInRequest_ExpectedDataReposityMethodCallShouldNotHappen()
    {
        // Arrange
        var accountNumber = 0;

        // Act & assert
        await Assert.ThrowsAsync<BadHttpRequestException>(async () => await _controller.GetLatestConnectionInfoForAccount(accountNumber));
        A.CallTo(() => _fakeDataRepository.GetIpAddressAndTimeOfLatestConnection(A<long>._)).MustNotHaveHappened();
    }
    #endregion
}
