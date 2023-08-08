using Stripe;
using StripPaymentPOC.Interfaces;
using StripPaymentPOC.Services;

var builder = WebApplication.CreateBuilder(args);

var key = builder.Configuration["Stripe:SecretKey"];
// Add services to the container.
StripeConfiguration.ApiKey = key;
StripeConfiguration.SetApiKey(key);

builder.Services.AddHttpContextAccessor();
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<IStripeService, StripeService>();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
	app.UseSwagger();
	app.UseSwaggerUI();
}
app.UseDefaultFiles();
app.UseStaticFiles();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
