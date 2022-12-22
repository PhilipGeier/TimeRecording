using System;
using System.Reflection.Metadata;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using NUnit.Framework;
using TimeTracking.Data;
using TimeTracking.Domain;
using TimeTracking.Service;
using TimeTracking.Service.Interfaces;

namespace TimeTracking.Test;

[TestFixture]
public class UserServiceTests
{
    private Mock<UserService> _mock;
    private IUserService _userService;

    [SetUp]
    public void UserServiceTestSetup()
    {
        _mock = new Mock<UserService>(MockBehavior.Default);
    }
}