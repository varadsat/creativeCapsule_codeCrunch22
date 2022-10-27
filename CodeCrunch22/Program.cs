using CodeCrunch22.Services.Github;
using CodeCrunch22.Services.StackOverflow;
using CodeCrunch22.Services.Youtube;
using Microsoft.AspNetCore.Mvc;
using System.Net;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddHttpClient();
builder.Services.AddHttpClient("SOClient").ConfigurePrimaryHttpMessageHandler(() => new HttpClientHandler
        {
            AutomaticDecompression = DecompressionMethods.GZip
        });
builder.Services.AddSingleton<IYoutubeService, YoutubeService>();
builder.Services.AddSingleton<IGithubService, GithubService>();
builder.Services.AddSingleton<IStackOverflowService, StackOverflowService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.MapGet("/youtube",async ([FromServices]IYoutubeService service,[FromQuery] string searchString, [FromQuery] string? pageToken) =>
{
    //TODO: add that channel thumbnail, viewCount
    var response = await service.GetSearchData(searchString, pageToken);
    var returnData = response.items.Select(v => new
    {
        VideoId = v.id.videoId,
        PublishedAt = v.snippet.publishedAt,
        Title = v.snippet.title,
        Description = v.snippet.description,
        Thumbnails = v.snippet.thumbnails,
        ChannelTitle = v.snippet.channelTitle
    });
    return returnData;
});

app.MapGet("/stackoverflow", async ([FromServices] IStackOverflowService service, [FromQuery] string searchString) =>
{
    var response = await service.GetSearchDataByStringAsync(searchString);
    var returnData = response.items.Select(x => new
    {
        Title = x.title,
        ProfileImage = x.owner.profile_image,
        DisplayName = x.owner.display_name,
        ViewCount = x.view_count,
        AnswerCount = x.answer_count
    });
    return returnData;
});

app.MapGet("/github", async ([FromServices] IGithubService service, [FromQuery] string searchString) =>
{
    var response = await service.GetSearchData2Async(searchString);
    var returnData = response.items.Select(x => new
    {
        Name = x.name,
        Desciption = x.description,
        Topics = x.topics,
        Stars = x.stargazers_count,
        Updated = x.updated_at

    });
    return returnData;
});

app.Run();