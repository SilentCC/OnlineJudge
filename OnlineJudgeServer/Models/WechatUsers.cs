using System;
using System.Collections.Generic;

namespace OnlineJudgeServer.Models
{
    public partial class WechatUsers
    {
        public int Id { get; set; }
        public string Phone { get; set; }
        public string Openid { get; set; }
        public int Status { get; set; }
        public string ReviewMsg { get; set; }
        public string Nickname { get; set; }
        public string Avatar { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public DateTime? Birthday { get; set; }
        public string IdNumber { get; set; }
        public string IdCardImg { get; set; }
        public string Postcode { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public DateTime? DeletedAt { get; set; }
    }
}
