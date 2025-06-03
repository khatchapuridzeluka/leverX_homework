    using leverX.Application.Interfaces.Repositories;
    using leverX.Application.Interfaces.Services;
    using leverX.Infrastructure.Repositories;
    using leverX.Application.Services;
    using System.Data;
    using Microsoft.Data.SqlClient;
    using LeverX.Application.Mappings;
    using leverX.Application.Mappings;
    using FluentValidation.AspNetCore;
    using FluentValidation;
    using leverX.Application.Validators;

var builder = WebApplication.CreateBuilder(args);

    // Add services to the container.

    builder.Services.AddControllers();
    // Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
    builder.Services.AddOpenApi();

    builder.Services.AddScoped<IDbConnection>(sp =>
    {
        var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
        return new SqlConnection(connectionString);
    });

    builder.Services.AddAutoMapper(typeof(OpeningProfile).Assembly);
    builder.Services.AddAutoMapper(typeof(GameProfile).Assembly);
    builder.Services.AddAutoMapper(typeof(PlayerProfile).Assembly);
    builder.Services.AddAutoMapper(typeof(TournamentProfile).Assembly); 
    builder.Services.AddAutoMapper(typeof(TournamentPlayerProfile).Assembly);

    builder.Services.AddFluentValidationAutoValidation();
    builder.Services.AddValidatorsFromAssemblyContaining<CreatePlayerDtoValidator>();


    builder.Services.AddScoped<IPlayerRepository, PlayerRepository>();
    builder.Services.AddScoped<IGameRepository, GameRepository>();
    builder.Services.AddScoped<ITournamentRepository, TournamentRepository>();
    builder.Services.AddScoped<IOpeningRepository, OpeningRepository>();
    builder.Services.AddScoped<ITournamentPlayerRepository, TournamentPlayerRepository>();
    builder.Services.AddScoped<IPlayerService, PlayerService>();
    builder.Services.AddScoped<IGameService, GameService>();
    builder.Services.AddScoped<ITournamentService, TournamentService>();
    builder.Services.AddScoped<IOpeningService, OpeningService>();
    builder.Services.AddScoped<ITournamentPlayerService, TournamentPlayerService>();



builder.Services.AddSwaggerGen(c =>
    {
        var xmlFile = $"{System.Reflection.Assembly.GetExecutingAssembly().GetName().Name}.xml";
        var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
        c.IncludeXmlComments(xmlPath);
    });

    var app = builder.Build();

    // Configure the HTTP request pipeline.
    if (app.Environment.IsDevelopment())
    {
        app.UseSwagger();
        app.UseSwaggerUI( c =>
        {
            c.SwaggerEndpoint("/swagger/v1/swagger.json", "leverX API V1");
            c.RoutePrefix = string.Empty; // Set Swagger UI at the app's root
        });
    }

    app.UseHttpsRedirection();

    app.UseAuthorization();

    app.MapControllers();

    app.Run();
