using System.Xml.XPath;

namespace Mojito.SimpleLogging.Test;

public class LogManagerTest
{
    private XPathNavigator xPathNavigator;

    [SetUp]
    public void SetUp()
    {
        xPathNavigator = new XPathDocument("App.config").CreateNavigator();
    }

    [Test]
    public void TestLogManager()
    {
        var logger = LogManager.GetLogger(typeof(LogManagerTest));

        logger.Debug("Test Debug message"); // Do not write
        logger.Info("Test Info message");

        var target = xPathNavigator.SelectSingleNode("/configuration/logging/target");

        Assert.That(target, Is.Not.Null);

        if (target.MoveToAttribute("file", ""))
        {
            var text = File.ReadAllText(target.Value ?? "");

            Assert.Multiple(() =>
            {
                Assert.That(File.Exists(target.Value), Is.True);
                Assert.That(text.IndexOf("DEBUG"), Is.EqualTo(-1));
                Assert.That(text.IndexOf("INFO"), Is.Not.EqualTo(-1));
            });
        }
    }
}