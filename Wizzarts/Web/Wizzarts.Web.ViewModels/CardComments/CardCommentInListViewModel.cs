using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wizzarts.Data.Models;
using Wizzarts.Services.Mapping;

namespace Wizzarts.Web.ViewModels.CardComments
{
    public class CardCommentInListViewModel : IMapFrom<CommentCard>
    {
        public int Id { get; set; }

        public string Title { get; set; } = string.Empty;

        public string Description { get; set; } = string.Empty;

        public string Review { get; set; } = string.Empty;

        public string Suggestions { get; set; } = string.Empty;

        public string CardId { get; set; }

        public string PostedByUserId { get; set; } = string.Empty;

        public bool IsPostedByAdmin { get; set; }
    }
}
