using Microsoft.Extensions.Configuration;

namespace Mojito.SimpleLogging.Test;

public class LogHelperTest
{
    private IConfiguration configuration;

    [SetUp]
    public void SetUp()
    {
        configuration = new ConfigurationBuilder()
            .AddXmlFile("App.config", optional: true, reloadOnChange: true)
            .Build();
    }

    [Test]
    public void TestLogHelper()
    {
        var file = configuration["logging:target:file"];

        LogHelper.Debug("Test Message"); // Do not write
        LogHelper.Info("Test Message");

        var text = File.ReadAllText(file ?? "");

        Assert.Multiple(() =>
        {
            Assert.That(File.Exists(file), Is.True);
            Assert.That(text.IndexOf("[Debug]"), Is.EqualTo(-1));
            Assert.That(text.IndexOf("[Info]"), Is.Not.EqualTo(-1));
        });
    }
}