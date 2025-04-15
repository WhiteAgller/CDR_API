using Application.CallRecords.Commands;
using Domain.CallRecordAggregate;
using FastEndpoints;
using FastEndpoints.Swagger;
using Infrastructure;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddMediatR(options =>
{
    options.RegisterServicesFromAssemblies(typeof(UploadCallRecordsCommand).Assembly);
    options.RegisterServicesFromAssemblies(typeof(CallRecord).Assembly);
});

builder.Services.AddInfrastructureService(builder.Configuration);

builder.Services.AddFastEndpoints()
    .SwaggerDocument(o =>
    {
        o.DocumentSettings = s =>
        {
            s.Title = "CDR API";
            s.Version = "v1";
        };
        o.EnableJWTBearerAuth = false;
    });

builder.Services.AddOpenApi();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseFastEndpoints()
    .UseSwaggerGen();

app.UseHttpsRedirection();

app.Run();