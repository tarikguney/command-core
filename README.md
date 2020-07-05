![Build Project](https://github.com/tarikguney/command-core/workflows/Build%20Project/badge.svg?branch=master)

A simple command line parsing library that helps creating CLI apps using MVC pattern.

There are many command line parsing libraries out there, but most of them are unnecessarily complicated. Command Core library is built using a well-understood desing pattern: MVC. It cleanly separates the building blocks and makes the CLI development a scalable, extensible, and more importantly simpler endeavor. 

Each verb and their arguments are represented with three simple classes inheriting from the following base classes: `VerbBase`, `VerbViewBase`, and `VerbOptionsBase`. The verbs are parsed into these classes and the clases are populated with the necessary properties for the consumers to access. For instance, your Verb class will include a property called `Options` through which you can easily access the CLI arguments passed as part of the verb.

### Under active development!
> This library has not been published yet. The readme.md file explains the vision of this library. It may change in the future. However, the idea of this library will always be the same: Make CLI development easier and simpler. Once published, it can be downloaded from Nuget as a package.


## How to use it?

Let's start with a simple command:

```bash
helloworld.exe add --firstname tarik --lastname guney --haslicense -a 33
```
If we dissect the command call above, these are the pieces of it:
- `add`: The verb of the command. It is also known as `subcommand`.
- `--name` and `--lastname`: These are the parameter names, which are also known as options. 
- `tarik` and `guney`: These are the arguments or parameter values.
- `-a` is an alias for `--age`, so you can either specify the age with `--age 33` or `-a 33`
- `--haslicense` is a flag, which by default has value of `true`. You don't need to specify its values explicitly.

When it comes to parameter names, CommandCore follows a strict format. You must use `--` suffix for the parameter names or `-` for the parameter aliases as shown in the sample call above. 

You can represent and parse the command above with the following classes:

```c#
[VerbName("add")]
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
    [ParameterName("firstname")]
    public string FirstName {get;set;}
    
    [ParameterName("lastname")]
    public string LastName {get;set;}

    // CommandCore supports various types like Boolean, and it automatically
    // converts them to their corresponding types specified with the Options properties.
    [ParameterName("haslicense")]
    public bool? HasLicense {get;set;}
    
    [ParameterName("age", Alias="a")]
    public int Age {get;set;}
}
```

To activate this library, you need to add the following code to the `Main` function in `Program.cs` file:

```c#
public static int Main(string[] args)
{
    var commandCoreApp = new CommandCoreApp();
    return commandCoreApp.Parse(args);
}
```

That's all, and it will be all. The whole idea of this library is to simplify the console application command line argument parsing.

## Generate help automatically

Command Core prints out a small documentation for the verbs and their associated options in an organized way to the console.You don't need to anything special as it works out of the box when people pass `--help` flag to the command. Check out the `CommandCore.TestConsole` project for an example.

`VerbName` and `ParameterName` attributes accepts an optional `Description` value which is used when printing out the documentation. You can see an example of the documentation output below:

```bash
VERBS:
------
    add: Adds a new person to the system.
    Options:
    --firstname: First name of the person provided.
    --lastname: Last name of the person provided.
    --haslicense (-hs): Indicates whether the person has a driver license
    --age (-a)
```

## Roadmap

1. Add routing mechanism, similar to Asp.NET Core MVC. Routing mechanism will help verbs to be routed to a desired class pattern for more complicated scenarios.
2. Add a middleware, which is similar to Asp.NET Core MVC request/response middleware features, which allows the developers to inject logic between a command is routed to its handling verb (controller).
3. Caching for faster type lookup. Currently, the entire assembly needs to be scanned for the types that match the verb. A caching mechanism can store away the detected types for faster retrival next time.
## Developed

by Tarik Guney with .NET Core 3.x.
