using Microsoft.AspNetCore.Authentication;
using Microsoft.EntityFrameworkCore;
using Registration;
using Registration.RegistrationServices;
using Swashbuckle.AspNetCore.SwaggerUI;

var builder = WebApplication.CreateBuilder(args);



builder.Services.AddControllers();

builder.Services.AddScoped<IRegistrationService, RegistrationService>();

builder.Services.AddDbContext<RegistrationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("connection")));

builder.Services.AddEndpointsApiExplorer();

builder.Services.AddCors(options =>
{
    options.AddPolicy(name: "allow",
        builder =>
            builder
                .AllowAnyMethod()
                .AllowAnyHeader()
                .SetIsOriginAllowed(_ => true)
                .AllowCredentials());
});
builder.Services.AddSwaggerGen();



var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseCors("allow");
app.UseHttpsRedirection();
app.UseRouting();
app.UseAuthorization();

app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "WebApi v1"));

app.MapControllers();

app.Run();
