[Test]
public void TestSQLInjectionPrevention()
{
    var response = _controller.Login(new LoginRequest { Email = "' OR '1'='1"; Password = "password" });
    Assert.IsInstanceOf<UnauthorizedResult>(response);
}
