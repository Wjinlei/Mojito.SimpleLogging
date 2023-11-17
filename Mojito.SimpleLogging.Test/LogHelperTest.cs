using System.Xml.XPath;

namespace Mojito.SimpleLogging.Test;

public class LogHelperTest
{
    private XPathNavigator xPathNavigator;

    [SetUp]
    public void SetUp()
    {
        xPathNavigator = new XPathDocument("App.config").CreateNavigator();
    }

    [Test]
    public void TestLogHelper()
    {
        LogHelper.Debug("Test Message"); // Do not write
        LogHelper.Info("Test Message");

        var target = xPathNavigator.SelectSingleNode("/configuration/logging/target");

        Assert.That(target, Is.Not.Null);

        if (target.MoveToAttribute("file", ""))
        {
            var text = File.ReadAllText(target.Value ?? "");

            Assert.Multiple(() =>
            {
                Assert.That(File.Exists(target.Value), Is.True);
                Assert.That(text.IndexOf("[Debug]"), Is.EqualTo(-1));
                Assert.That(text.IndexOf("[Info]"), Is.Not.EqualTo(-1));
            });
        }
    }
}