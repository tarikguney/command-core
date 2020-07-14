![Build Project](https://github.com/tarikguney/command-core/workflows/Build%20Project/badge.svg?branch=master)
[![Coverage Status](https://coveralls.io/repos/github/tarikguney/command-core/badge.svg?branch=master)](https://coveralls.io/github/tarikguney/command-core?branch=master)
[![NuGet version (CommandCore.Library)](https://img.shields.io/nuget/v/CommandCore.Library.svg)](https://www.nuget.org/packages/CommandCore.Library/)
![GitHub](https://img.shields.io/github/license/tarikguney/command-core)

<img src="./command-core-logo.png" height="100px"/>

A simple command line parsing library that helps creating CLI apps using MVC pattern.

There are many command line parsing libraries out there, but most of them are unnecessarily complicated. Command Core library is built using a well-understood desing pattern: MVC. It cleanly separates the building blocks and makes the CLI development a scalable, extensible, and more importantly simpler endeavor. 

## Check out WIKI to get started!

Check out the [WIKI page](https://github.com/tarikguney/command-core/wiki) to learn more on how to get started with CommandCore!

## Quick Look

If you are familar with MVC pattern in any framework, you will find CommandCore very familiar with them. Look at below to see how a simple command like the one below can be represented in your .NET application:

```bash
helloworld.exe add --firstname tarik --lastname guney --haslicense -a 33
```

```c#
[VerbName("add", Description="Allows to add a new person to the database.")]
[VerbName("add-person")]
public class Add : VerbBase<AddOptions>
{
    public VerbView Run(){
        return AddView(Options);
    }
}

public class AddView : VerbViewBase<AddOptions>
{
    public override void RenderResponse(){
        Console.WriteLine(
            $"FirstName: {_options!.FirstName}\n" +
            $"Last Name: {_options!.LastName}\n" +
            $"Has License: {_options!.HasLicense}\n" +
            $"Age: {_options.Age}");
    }
}

public class AddOptions : VerbOptionsBase
{
    [OptionName("firstname")]
    [OptionName("fn"), Alias="f")]
    public string FirstName {get;set;}
    
    [OptionName("lastname")]
    public string LastName {get;set;}

    [OptionName("haslicense")]
    public bool? HasLicense {get;set;}
    
    [OptionName("age", Alias="a")]
    public int Age {get;set;}
}
```

## Integrated IoC Container for Dependency Injection

CommandCore comes with an in-house IoC container named `CommandCore.LightIoC`, and it allows you to register your own services and inject them to the verb classes. Check out this page for more information: [Dependency Injection with LightIoC Container](https://github.com/tarikguney/command-core/wiki/Dependency-Injection-with-LightIoC-Container)

## Roadmap

1. Add routing mechanism, similar to Asp.NET Core MVC. Routing mechanism will help verbs to be routed to a desired class pattern for more complicated scenarios.
2. Add a middleware, which is similar to Asp.NET Core MVC request/response middleware features, which allows the developers to inject logic between a command is routed to its handling verb (controller).
3. Caching for faster type lookup. Currently, the entire assembly needs to be scanned for the types that match the verb. A caching mechanism can store away the detected types for faster retrival next time.

## Developed

by Tarik Guney with .NET Core 3.x.
