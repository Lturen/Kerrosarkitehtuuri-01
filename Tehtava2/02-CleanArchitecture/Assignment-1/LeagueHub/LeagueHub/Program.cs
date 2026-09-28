using LeagueHub.Repository;
using LeagueHub.Services;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<ITeamRepository, InMemoryTeamRepository>();
builder.Services.AddScoped<ITeamService, TeamService>();

builder.Services.AddScoped<IPlayerRepository, InMemoryPlayerRepository>();
builder.Services.AddScoped<IPlayerService, PlayerService>();

builder.Services.AddScoped<IMatchRepository, InMemoryMatchRepository>();
builder.Services.AddScoped<IMatchService, MatchService>();

builder.Services.AddScoped<IMatchScoreRepository, InMemoryMatchScoreRepository>();
builder.Services.AddScoped<IMatchScoreService, MatchScoreService>();

builder.Services.AddScoped<ILeagueTableRepository, InMemoryLeagueTableRepository>();
builder.Services.AddScoped<ILeagueTableService, LeagueTableService>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
