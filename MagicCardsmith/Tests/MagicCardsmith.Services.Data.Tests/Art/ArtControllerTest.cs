using MagicCardsmith.Web.Controllers;
using MagicCardsmith.Web.ViewModels.Art;
using MyTested.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MagicCardsmith.Services.Data.Tests.Art
{
    public class ArtControllerTest
    {
        [Test]
        public void GetCreateArtShouldReturnCorrectView()
        => MyPipeline
            .Configuration()
            .ShouldMap(request => request
            .WithPath("/Art/Create")
            .WithMethod(HttpMethod.Get))
            .To<ArtController>(c => c.Create())
            .Which()
            .ShouldReturn()
            .View();
  
    }
}
