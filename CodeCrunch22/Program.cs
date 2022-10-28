using CodeCrunch22.Services.Github;
using CodeCrunch22.Services.Google;
using CodeCrunch22.Services.StackOverflow;
using CodeCrunch22.Services.Twitter;
using CodeCrunch22.Services.Youtube;
using Microsoft.AspNetCore.Mvc;
using System.Net;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
//TODO : Renove this if twitter gets fixed
builder.Services.AddSwaggerGen(options =>
{
    options.CustomSchemaIds(type => type.ToString());
});
builder.Services.AddHttpClient();
builder.Services.AddHttpClient("SOClient").ConfigurePrimaryHttpMessageHandler(() => new HttpClientHandler
{
    AutomaticDecompression = DecompressionMethods.GZip
});
builder.Services.AddSingleton<IYoutubeService, YoutubeService>();
builder.Services.AddSingleton<IGithubService, GithubService>();
builder.Services.AddSingleton<IStackOverflowService, StackOverflowService>();
builder.Services.AddSingleton<IGoogleService, GoogleService>();
builder.Services.AddSingleton<ITwitterService, TwitterService>();

var isDevelopment = builder.Environment.IsDevelopment();
builder.Services.AddCors(options =>
                options.AddDefaultPolicy(
                    builder =>
                    {
                        builder.AllowAnyHeader().AllowAnyMethod().WithOrigins(isDevelopment ? "http://127.0.0.1:5173" : "https://codecrunch22.netlify.app").AllowCredentials();
                    }));
var app = builder.Build();
app.UseCors();
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.MapGet("/youtube", async ([FromServices] IYoutubeService service, [FromQuery] string searchString, [FromQuery] string? pageToken) =>
{
    //TODO: add that channel thumbnail, viewCount
    var response = await service.GetSearchData(searchString, pageToken);
    var data = response.items.Select(v => new
    {
        VideoId = v.id.videoId,
        PublishedAt = v.snippet.publishedAt,
        Title = v.snippet.title,
        Description = v.snippet.description,
        Thumbnails = v.snippet.thumbnails,
        ChannelTitle = v.snippet.channelTitle
    });
    var returnData = new
    {
        response.nextPageToken,
        Items = data
    };
    return returnData;
});

app.MapGet("/stackoverflow", async ([FromServices] IStackOverflowService service, [FromQuery] string searchString) =>
{
    var response = await service.GetSearchDataByStringAsync(searchString);
    var returnData = response.items.Select(x => new
    {
        Title = x.title,
        Id = x.question_id,
        ProfileImage = x.owner.profile_image,
        DisplayName = x.owner.display_name,
        ViewCount = x.view_count,
        AnswerCount = x.answer_count,
        UpVotes = x.up_vote_count,
        BodyMarkdown = x.body_markdown.Substring(0, Math.Min(x.body_markdown.Length, 150)),
        Tags = x.tags,
        Link = x.link,
        LastActivity = x.last_activity_date
    });
    return returnData;
});

app.MapGet("/github", async ([FromServices] IGithubService service, [FromQuery] string searchString) =>
{
    //TODO : try to remove octokit
    //TODO : stars and date on frontend 
    var response = await service.GetSearchData2Async(searchString);
    var returnData = response.items.Select(x => new
    {
        Id = x.id,
        Name = x.name,
        Description = x.description,
        Topics = x.topics,
        Stars = x.stargazers_count,
        Updated = x.updated_at,
        Link = x.html_url
    });
    return returnData;
});

app.MapGet("/google", async ([FromServices] IGoogleService service, [FromQuery] string searchString) =>
{
    var response = await service.GetGoogleSearchDataAsync(searchString);
    var returnData = response.items.Select(x => new
    {
        Id = x.cacheId,
        Title = x.title,
        Snippet = x.snippet,
        DisplayLink = x.displayLink,
        Link = x.link
    });
    return returnData;
});
app.MapGet("/twitter", async ([FromServices] ITwitterService service, [FromQuery] string searchString) =>
{
    var response = await service.GetTwitterSearchDataV2Async(searchString);
    var returnData = response.data.Join(response.includes.users,t=> t.author_id,u => u.id,
        (t,u) => new
    {
        Id = t.id,
        Text = t.text,
        //Image = t.entities.urls.FirstOrDefault() ?? null,
        AuthorImg = u.profile_image_url,
        AuthorUserName = u.username,    
        AuthorName = u.name,
        Link = $"https://twitter.com/i/web/status/{t.id}",
        AuthorProfileLink = $"https://twitter.com/{u.username}",
        RetweetCount = t.public_metrics.retweet_count,
        ReplyCount = t.public_metrics.reply_count,
        LikeCount = t.public_metrics.like_count,
        QuoteCount = t.public_metrics.quote_count,
        CreatedAt = t.created_at
    });
    return returnData;
});

app.Run();