using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WebServer.GameStoreApplication.Services;
using WebServer.Server.Http.Contracts;

namespace WebServer.GameStoreApplication.Controllers
{
    public class GameController : BaseController
    {
        private GameService gameService;

        public GameController(IHttpRequest request)
            : base(request)
        {
            this.gameService = new GameService();
        }

        public IHttpResponse View(int id) 
        {
            // Get Game from db
            var game = this.gameService.Find(id);
                        
            // Incorporate the result in the html file:
            var result = new StringBuilder();
            result.AppendLine($"<h1>{game.Title}</h1>")
                 .AppendLine($@"<iframe width=""560"" height=""315"" src=""https://www.youtube.com/embed/{game.ImageUrl}"" frameborder=""0"" allowfullscreen> </iframe>")
                 .AppendLine("<br/>")
                 .AppendLine($"<div>{game.Description};</div>").AppendLine("<br/>")
                 .AppendLine($"<div><b>Price</b> - {game.Price:F2} &euro;</div>").AppendLine("<br/>")
                 .AppendLine($"<div><b>Size</b> {game.Size:F1} GB</div>").AppendLine("<br/>")
                 .AppendLine($"<div><b>Release Date</b> {game.ReleaseDate.ToShortDateString()}</div>").AppendLine("<br/>")
                 .AppendLine($"<div>")                   
                 .AppendLine($@"<a class=""btn btn-warning"" href=""/"">Back</a>")
                 .AppendLine($@"<a class=""btn btn-danger"" href=""#"">Buy</a>")
                 .AppendLine($"</div>")
                 .ToString();

            var resultAsHtml = string.Join(Environment.NewLine, result);

            this.ViewData["result"] = resultAsHtml; // replate this with the other table!

            return this.FileViewResponse(@"game\game-details");
        }
    }
}
