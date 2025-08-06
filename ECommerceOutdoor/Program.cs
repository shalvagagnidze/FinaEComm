using Amazon;
using Amazon.S3;
using InfrastructureLayer;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Options;
using ServiceLayer;
using ServiceLayer.Models;
using ServiceLayer.Services;
using System.Runtime;
using Toycloud.AspNetCore.Mvc.ModelBinding;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddControllers();
builder.Services.AddInfrastructure(builder.Configuration);
builder.Services.AddApplication(builder.Configuration);
builder.Services.AddMvc(options =>
{
    options.ModelBinderProviders.InsertBodyOrDefaultBinding();
});

builder.Services.Configure<S3Settings>(builder.Configuration.GetSection("S3Settings"));

var awsCredentials = new Amazon.Runtime.BasicAWSCredentials(
    Environment.GetEnvironmentVariable("AWS_ACCESS_KEY_ID"),
    Environment.GetEnvironmentVariable("AWS_SECRET_ACCESS_KEY")
);


builder.Services.AddSingleton<IAmazonS3>(sp =>
{
    var s3Settings = sp.GetRequiredService<IOptions<S3Settings>>().Value;
    var config = new AmazonS3Config
    {
        RegionEndpoint = RegionEndpoint.GetBySystemName(s3Settings.Region)
    };

    return new AmazonS3Client(awsCredentials,config);
});

builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(
        policy =>
        {
            policy.WithOrigins("*").AllowAnyMethod().AllowAnyHeader(); ;
        });
});


builder.Services.AddControllers().AddJsonOptions(options =>
{
    options.JsonSerializerOptions.ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.IgnoreCycles;
});

var app = builder.Build();

await SeedData.SeedRoleAsync(app);
await SeedData.SeedUserAsync(app);

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

var imagesPath = Path.Combine(builder.Environment.ContentRootPath, "Images");

if (!Directory.Exists(imagesPath))
{
    Directory.CreateDirectory(imagesPath);
}

app.UseStaticFiles(new StaticFileOptions
{
    FileProvider = new PhysicalFileProvider(
           Path.Combine(builder.Environment.ContentRootPath, "Images")),
    RequestPath = "/Resources"
});

app.UseStaticFiles();

app.UseCors();

app.UseAuthorization();

app.MapControllers();

app.Run();
