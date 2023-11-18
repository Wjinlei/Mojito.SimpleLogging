## Usage:

```csharp
LogHelper.Debug("Hello World");
LogHelper.Info("Hello World");
LogHelper.Warn("Hello World");
LogHelper.Error("Hello World");
LogHelper.Fatal("Hello World");
```

## Config

App.config

```xml
<?xml version="1.0" encoding="utf-8" ?>
<configuration>
  <logging>
    <target value="file" file="log/Mojito.log" maxRollBackups="10" maxRollTime="1d" />
    <level value="Debug" />
    <pattern value="%date [%level] %message%newline" />
  </logging>
</configuration>
```


### Options

`target`: // Logger target `Console` | `file` 
- `file` // Log path
- `maxRollBackups="10"` // The maximum retention is 10 copies  
- `maxRollSize="512kb"` // The log is larger than or equal to 512kb, Supported units is `b`, `kb`, `mb`, `gb`  
- `maxRollTime="1d"` // The log is rolled every 1 day, Supported units is `s`, `m`, `h`, `d`

`level`: // Log level
- `Debug`
- `Info`
- `Warn`
- `Error`
- `Fatal`

`pattern` // Log pattern
- `%date` // Date time format is `yyyy-MM-dd HH:mm:ss`
- `%level` // Log level
- `%message` // Your message
- `%newline` // Environment.NewLine