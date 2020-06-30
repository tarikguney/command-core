# What is CommandCore?

A simple command line parsing library that helps creating MVC like command line applications.

There are many command line parsing libraries out there but most of them are unnecessarily complicated. They require so much boilerplate code; whereas, Command Core library uses an already well-understood design pattern: MVC. It is easy to start with and reason about it.

### Under active development!
> This library has not been published yet. The readme.md file explains the vision of this library. It may change in the future. However, the idea of this library will always be the same: Make CLI development easier and simpler.


## How to use it?

Let's start with a simple command:

```bash
helloworld.exe add --name tarik --lastname guney
```

You can write represent and parse the command with the following classes:

```c#
[VerbName("add")]
public class Add : Verb<AddOptions>
{
    public VerbView Run(){
        return AddView(Options);
    }
}

public class AddView : VerbView
{
    public override StringBuilder Write(){
        var outputBuilder = new StringBuilder();
        builder.Append($"Name: {Model.FirstName} {Environment.NewLine}");
        builder.Append($"Name: {Model.LastName} {Environment.NewLine}");
        return outputBuilder;
    }
}

public class AddOptions : VerbOptions
{
    [InputName("firstname")]
    public string? FirstName {get;set;}
    
    [InputName("lastname")]
    public string? LastName {get;set;}
}
```

To activate this library, you need to add the following code to the `Main` function in `Program.cs` file:

```c#
public static int Main(string[] args)
{
    return CommandCore.Parse(args);
}
```

That's all, and it will be all. The whole idea of this library is to simplify the console application command line argument parsing.

## Developed

by Tarik Guney with .NET Core 3.x.
