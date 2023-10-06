// See https://aka.ms/new-console-template for more information

using BuilderGenerator.ClassesForCreateBuilder;

string generatorClass = BuilderGenerator.BuilderGenerator.Generate(typeof(MyObject),false);
Console.Write(generatorClass);

Console.ReadLine();