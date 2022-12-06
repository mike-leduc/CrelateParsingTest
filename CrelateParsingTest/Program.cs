using CrelateParsingTest.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace CrelateParsingTest;

public class Program
{
    //Coding exercise:The contact data below is coming into the company from a vendor.This information needs to be validated and for correctness before further processing.
    //Can you build a tool to validate the records inbound according to the following rules?
    // Rules:
    // Valid records must have the following fields: Id, FirstName, and LastName
    // No duplicates and duplicates are based on phone number
    // Ids are GUIDs

private readonly static string _contacts = @"Id,FirstName,LastName,PhoneNumber
72F740AD-D949-41FE-945E-8801AAD04748,Jessica,Avery,202-555-0149
48EDF105-6610-44E6-A680-5D3503EAF4A2,Benjamin,Hemmings,2025550129
2A97E6D8-D0D7-4B2E-9B54-1908C96A9FE4,Grace,,202-555-0130
9F937783-DC28-421C-A937-A0C201643AAE,Oliver,Wendell,202-555-3484
4463CE30-041E-4EBF-8231-63443E36E281,Emily,Smith,   
2CE36345-65A3-401A-B504-A3290162041,Joseph,Lummings,202-555-9351
A203D9EA-79EF-4274-435F-C1CDABADD908,Robert,Ebert,(202) 555-0149
0AF60567-DD6E-40D6-1FBE-417A25D1D908,,,202-555-2173
7745FA91-E7F6-47D5-AB87-0349445D5F0F,Tim,Payne,
834EF63A-DA43-4721-9DCA-15468ABC129E, ,Johnson,202-555-3484
57F9A815-6044-4768-943D-8F7BD1D9CAE2,Elyse,Jenkins,202-555-0129
B8E6C4AD-EC28-42FD-53FB-1C233F80DA08,Jennifer,Coolidge,Kirkland,202-555-7654";

    static void Main(string[] args)
    {
        var services = new ServiceCollection();
        ConfigureServices(services);
        var serviceProvider = services.BuildServiceProvider();

        var executor = serviceProvider.GetService<Executor>();

        executor!.Execute(_contacts);
    }

    private static void ConfigureServices(IServiceCollection services)
    {
        services
            .AddSingleton<Executor>()
            .AddScoped<IContactParser, ContactParser>()
            .AddScoped<IContactValidator, ContactValidator>();
    }
}
