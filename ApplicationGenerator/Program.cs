// See https://aka.ms/new-console-template for more information
using ApplicationGenerator;
using ApplicationGenerator.Business;

Console.WriteLine("Hello, World!");
Class1.CreateHandlerCreated();
var generator = new Generator();
//generator.CreateFeatureCsFiles();
// generator.CreateRequestsCsFiles();
generator.CreateControllersFiles();