using System;
using System.Collections.Generic;

namespace OnlineJudgeServer.Models
{
    public partial class Books
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string OriginTitle { get; set; }
        public string AltTitle { get; set; }
        public string Subtitle { get; set; }
        public string Isbn { get; set; }
        public string Language { get; set; }
        public string Publisher { get; set; }
        public DateTime? Pubdate { get; set; }
        public string ClassNum { get; set; }
        public string CallNumber { get; set; }
        public string Author { get; set; }
        public string Translator { get; set; }
        public string AuthorIntroduction { get; set; }
        public string TranslatorIntroduction { get; set; }
        public string Binding { get; set; }
        public float Price { get; set; }
        public int Page { get; set; }
        public int Word { get; set; }
        public string Description { get; set; }
        public string Catalog { get; set; }
        public string Preview { get; set; }
        public string Imgs { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public DateTime? DeletedAt { get; set; }
    }
}
