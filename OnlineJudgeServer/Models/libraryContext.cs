using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace OnlineJudgeServer.Models
{
    public partial class LibraryContext : DbContext
    {
        public LibraryContext()
        {
        }

        public LibraryContext(DbContextOptions<LibraryContext> options)
            : base(options)
        {
        }

        public virtual DbSet<BookBooklist> BookBooklist { get; set; }
        public virtual DbSet<BooklistWechatUser> BooklistWechatUser { get; set; }
        public virtual DbSet<Booklists> Booklists { get; set; }
        public virtual DbSet<Books> Books { get; set; }
        public virtual DbSet<Classifications> Classifications { get; set; }
        public virtual DbSet<Codes> Codes { get; set; }
        public virtual DbSet<Collections> Collections { get; set; }
        public virtual DbSet<Libraries> Libraries { get; set; }
        public virtual DbSet<Orders> Orders { get; set; }
        public virtual DbSet<RecommendedBook> RecommendedBook { get; set; }
        public virtual DbSet<RecommendedBooklist> RecommendedBooklist { get; set; }
        public virtual DbSet<ReviewLikes> ReviewLikes { get; set; }
        public virtual DbSet<Reviews> Reviews { get; set; }
        public virtual DbSet<Tokens> Tokens { get; set; }
        public virtual DbSet<WechatUsers> WechatUsers { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseMySql("Server=47.240.2.193;Database=library;port=3306;user=root;password=dage123;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<BookBooklist>(entity =>
            {
                entity.HasKey(e => new { e.BooklistId, e.BookId })
                    .HasName("PRIMARY");

                entity.ToTable("book_booklist");

                entity.Property(e => e.BooklistId)
                    .HasColumnName("booklist_id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.BookId)
                    .HasColumnName("book_id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.Comment)
                    .HasColumnName("comment")
                    .HasColumnType("text");

                entity.Property(e => e.CreatedAt)
                    .HasColumnName("created_at")
                    .HasColumnType("timestamp")
                    .HasDefaultValueSql("'CURRENT_TIMESTAMP'");

                entity.Property(e => e.UpdatedAt)
                    .HasColumnName("updated_at")
                    .HasColumnType("timestamp");
            });

            modelBuilder.Entity<BooklistWechatUser>(entity =>
            {
                entity.HasKey(e => new { e.BooklistId, e.WechatUserId })
                    .HasName("PRIMARY");

                entity.ToTable("booklist_wechat_user");

                entity.Property(e => e.BooklistId)
                    .HasColumnName("booklist_id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.WechatUserId)
                    .HasColumnName("wechat_user_id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.CreatedAt)
                    .HasColumnName("created_at")
                    .HasColumnType("timestamp")
                    .HasDefaultValueSql("'CURRENT_TIMESTAMP'");

                entity.Property(e => e.UpdatedAt)
                    .HasColumnName("updated_at")
                    .HasColumnType("timestamp");
            });

            modelBuilder.Entity<Booklists>(entity =>
            {
                entity.ToTable("booklists");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.CreatedAt)
                    .HasColumnName("created_at")
                    .HasColumnType("timestamp")
                    .HasDefaultValueSql("'CURRENT_TIMESTAMP'");

                entity.Property(e => e.Description)
                    .IsRequired()
                    .HasColumnName("description")
                    .HasColumnType("varchar(300)");

                entity.Property(e => e.Title)
                    .IsRequired()
                    .HasColumnName("title")
                    .HasColumnType("varchar(100)");

                entity.Property(e => e.UpdatedAt)
                    .HasColumnName("updated_at")
                    .HasColumnType("timestamp");

                entity.Property(e => e.WechatUserId)
                    .HasColumnName("wechat_user_id")
                    .HasColumnType("int(11)");
            });

            modelBuilder.Entity<Books>(entity =>
            {
                entity.ToTable("books");

                entity.HasIndex(e => e.ClassNum)
                    .HasName("class_num");

                entity.HasIndex(e => e.Isbn)
                    .HasName("isbn_2")
                    .IsUnique();

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.AltTitle)
                    .IsRequired()
                    .HasColumnName("alt_title")
                    .HasColumnType("varchar(100)");

                entity.Property(e => e.Author)
                    .IsRequired()
                    .HasColumnName("author")
                    .HasColumnType("text");

                entity.Property(e => e.AuthorIntroduction)
                    .IsRequired()
                    .HasColumnName("author_introduction")
                    .HasColumnType("text");

                entity.Property(e => e.Binding)
                    .IsRequired()
                    .HasColumnName("binding")
                    .HasColumnType("varchar(11)");

                entity.Property(e => e.CallNumber)
                    .IsRequired()
                    .HasColumnName("call_number")
                    .HasColumnType("varchar(20)");

                entity.Property(e => e.Catalog)
                    .IsRequired()
                    .HasColumnName("catalog")
                    .HasColumnType("text");

                entity.Property(e => e.ClassNum)
                    .IsRequired()
                    .HasColumnName("class_num")
                    .HasColumnType("varchar(50)");

                entity.Property(e => e.CreatedAt)
                    .HasColumnName("created_at")
                    .HasColumnType("timestamp")
                    .HasDefaultValueSql("'CURRENT_TIMESTAMP'");

                entity.Property(e => e.DeletedAt)
                    .HasColumnName("deleted_at")
                    .HasColumnType("timestamp");

                entity.Property(e => e.Description)
                    .IsRequired()
                    .HasColumnName("description")
                    .HasColumnType("text");

                entity.Property(e => e.Imgs)
                    .IsRequired()
                    .HasColumnName("imgs")
                    .HasColumnType("text");

                entity.Property(e => e.Isbn)
                    .IsRequired()
                    .HasColumnName("isbn")
                    .HasColumnType("varchar(20)");

                entity.Property(e => e.Language)
                    .IsRequired()
                    .HasColumnName("language")
                    .HasColumnType("varchar(10)");

                entity.Property(e => e.OriginTitle)
                    .IsRequired()
                    .HasColumnName("origin_title")
                    .HasColumnType("varchar(100)");

                entity.Property(e => e.Page)
                    .HasColumnName("page")
                    .HasColumnType("int(11)");

                entity.Property(e => e.Preview)
                    .IsRequired()
                    .HasColumnName("preview")
                    .HasColumnType("text");

                entity.Property(e => e.Price).HasColumnName("price");

                entity.Property(e => e.Pubdate)
                    .HasColumnName("pubdate")
                    .HasColumnType("date");

                entity.Property(e => e.Publisher)
                    .IsRequired()
                    .HasColumnName("publisher")
                    .HasColumnType("varchar(50)");

                entity.Property(e => e.Subtitle)
                    .IsRequired()
                    .HasColumnName("subtitle")
                    .HasColumnType("varchar(100)");

                entity.Property(e => e.Title)
                    .IsRequired()
                    .HasColumnName("title")
                    .HasColumnType("varchar(100)");

                entity.Property(e => e.Translator)
                    .IsRequired()
                    .HasColumnName("translator")
                    .HasColumnType("text");

                entity.Property(e => e.TranslatorIntroduction)
                    .IsRequired()
                    .HasColumnName("translator_introduction")
                    .HasColumnType("text");

                entity.Property(e => e.UpdatedAt)
                    .HasColumnName("updated_at")
                    .HasColumnType("timestamp");

                entity.Property(e => e.Word)
                    .HasColumnName("word")
                    .HasColumnType("int(11)");
            });

            modelBuilder.Entity<Classifications>(entity =>
            {
                entity.HasKey(e => e.Number)
                    .HasName("PRIMARY");

                entity.ToTable("classifications");

                entity.HasIndex(e => e.ParentNumber)
                    .HasName("parent_number");

                entity.Property(e => e.Number)
                    .HasColumnName("number")
                    .HasColumnType("varchar(10)");

                entity.Property(e => e.CreatedTime)
                    .HasColumnName("created_time")
                    .HasColumnType("timestamp")
                    .HasDefaultValueSql("'CURRENT_TIMESTAMP'");

                entity.Property(e => e.Name)
                    .HasColumnName("name")
                    .HasColumnType("varchar(50)");

                entity.Property(e => e.NextNumber)
                    .HasColumnName("next_number")
                    .HasColumnType("varchar(10)");

                entity.Property(e => e.ParentNumber)
                    .HasColumnName("parent_number")
                    .HasColumnType("varchar(10)");

                entity.Property(e => e.SonNumber)
                    .HasColumnName("son_number")
                    .HasColumnType("varchar(10)");
            });

            modelBuilder.Entity<Codes>(entity =>
            {
                entity.ToTable("codes");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.Code)
                    .IsRequired()
                    .HasColumnName("code")
                    .HasColumnType("varchar(10)");

                entity.Property(e => e.CreatedAt)
                    .HasColumnName("created_at")
                    .HasColumnType("timestamp")
                    .HasDefaultValueSql("'CURRENT_TIMESTAMP'");

                entity.Property(e => e.Expiry)
                    .HasColumnName("expiry")
                    .HasColumnType("int(5)")
                    .HasDefaultValueSql("'300'");

                entity.Property(e => e.Phone)
                    .IsRequired()
                    .HasColumnName("phone")
                    .HasColumnType("varchar(15)");

                entity.Property(e => e.Type)
                    .IsRequired()
                    .HasColumnName("type")
                    .HasColumnType("varchar(10)");

                entity.Property(e => e.UpdatedAt)
                    .HasColumnName("updated_at")
                    .HasColumnType("timestamp");
            });

            modelBuilder.Entity<Collections>(entity =>
            {
                entity.ToTable("collections");

                entity.HasIndex(e => e.LibraryId)
                    .HasName("lib_library_id_2");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.AvailableNum)
                    .HasColumnName("available_num")
                    .HasColumnType("int(11)");

                entity.Property(e => e.BookId)
                    .HasColumnName("book_id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.CreatedAt)
                    .HasColumnName("created_at")
                    .HasColumnType("timestamp")
                    .HasDefaultValueSql("'CURRENT_TIMESTAMP'");

                entity.Property(e => e.IsAvailable)
                    .HasColumnName("is_available")
                    .HasColumnType("int(11)")
                    .HasDefaultValueSql("'1'");

                entity.Property(e => e.LibraryId)
                    .HasColumnName("library_id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.TotalNum)
                    .HasColumnName("total_num")
                    .HasColumnType("int(11)");

                entity.Property(e => e.UpdatedAt)
                    .HasColumnName("updated_at")
                    .HasColumnType("timestamp");
            });

            modelBuilder.Entity<Libraries>(entity =>
            {
                entity.ToTable("libraries");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.Address)
                    .IsRequired()
                    .HasColumnName("address")
                    .HasColumnType("varchar(200)");

                entity.Property(e => e.AdminName)
                    .IsRequired()
                    .HasColumnName("admin_name")
                    .HasColumnType("varchar(20)");

                entity.Property(e => e.AdminPassword)
                    .IsRequired()
                    .HasColumnName("admin_password")
                    .HasColumnType("varchar(20)");

                entity.Property(e => e.AdminPhone)
                    .IsRequired()
                    .HasColumnName("admin_phone")
                    .HasColumnType("varchar(20)");

                entity.Property(e => e.CreatedAt)
                    .HasColumnName("created_at")
                    .HasColumnType("timestamp")
                    .HasDefaultValueSql("'CURRENT_TIMESTAMP'");

                entity.Property(e => e.Introduction)
                    .IsRequired()
                    .HasColumnName("introduction")
                    .HasColumnType("text");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnName("name")
                    .HasColumnType("varchar(50)");

                entity.Property(e => e.Phone)
                    .IsRequired()
                    .HasColumnName("phone")
                    .HasColumnType("varchar(20)");

                entity.Property(e => e.Photos)
                    .IsRequired()
                    .HasColumnName("photos")
                    .HasColumnType("text");

                entity.Property(e => e.Qualifications)
                    .IsRequired()
                    .HasColumnName("qualifications")
                    .HasColumnType("text");

                entity.Property(e => e.ReviewMsg)
                    .IsRequired()
                    .HasColumnName("review_msg")
                    .HasColumnType("varchar(500)");

                entity.Property(e => e.Status)
                    .HasColumnName("status")
                    .HasColumnType("int(11)")
                    .HasDefaultValueSql("'0'");

                entity.Property(e => e.UpdatedAt)
                    .HasColumnName("updated_at")
                    .HasColumnType("timestamp");
            });

            modelBuilder.Entity<Orders>(entity =>
            {
                entity.ToTable("orders");

                entity.HasIndex(e => e.CreatedAt)
                    .HasName("create_time");

                entity.HasIndex(e => e.Isbn)
                    .HasName("bk_book_isbn");

                entity.HasIndex(e => e.Status)
                    .HasName("state");

                entity.HasIndex(e => e.WechatUserId)
                    .HasName("user_user_phone");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.ActualReturnTime)
                    .HasColumnName("actual_return_time")
                    .HasColumnType("varchar(20)");

                entity.Property(e => e.ActualTakeTime)
                    .HasColumnName("actual_take_time")
                    .HasColumnType("varchar(20)");

                entity.Property(e => e.CreatedAt)
                    .HasColumnName("created_at")
                    .HasColumnType("timestamp")
                    .HasDefaultValueSql("'CURRENT_TIMESTAMP'");

                entity.Property(e => e.DeletedAt)
                    .HasColumnName("deleted_at")
                    .HasColumnType("timestamp");

                entity.Property(e => e.Fine)
                    .HasColumnName("fine")
                    .HasDefaultValueSql("'0'");

                entity.Property(e => e.IsFinePaied)
                    .HasColumnName("is_fine_paied")
                    .HasColumnType("int(11)")
                    .HasDefaultValueSql("'0'");

                entity.Property(e => e.Isbn)
                    .IsRequired()
                    .HasColumnName("isbn")
                    .HasColumnType("varchar(50)");

                entity.Property(e => e.LibraryId)
                    .HasColumnName("library_id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.RenewCount)
                    .HasColumnName("renew_count")
                    .HasColumnType("int(11)")
                    .HasDefaultValueSql("'0'");

                entity.Property(e => e.ShouldReturnTime)
                    .HasColumnName("should_return_time")
                    .HasColumnType("varchar(20)");

                entity.Property(e => e.ShouldTakeTime)
                    .HasColumnName("should_take_time")
                    .HasColumnType("date");

                entity.Property(e => e.Status)
                    .HasColumnName("status")
                    .HasColumnType("int(11)");

                entity.Property(e => e.UpdatedAt)
                    .HasColumnName("updated_at")
                    .HasColumnType("timestamp");

                entity.Property(e => e.WechatUserId)
                    .HasColumnName("wechat_user_id")
                    .HasColumnType("int(11)");
            });

            modelBuilder.Entity<RecommendedBook>(entity =>
            {
                entity.ToTable("recommended_book");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.BookId)
                    .HasColumnName("book_id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.Comment)
                    .IsRequired()
                    .HasColumnName("comment")
                    .HasColumnType("varchar(300)");

                entity.Property(e => e.CreatedAt)
                    .HasColumnName("created_at")
                    .HasColumnType("timestamp")
                    .HasDefaultValueSql("'CURRENT_TIMESTAMP'");

                entity.Property(e => e.DeletedAt)
                    .HasColumnName("deleted_at")
                    .HasColumnType("timestamp");

                entity.Property(e => e.UpdatedAt)
                    .HasColumnName("updated_at")
                    .HasColumnType("timestamp");

                entity.Property(e => e.WechatUserId)
                    .HasColumnName("wechat_user_id")
                    .HasColumnType("int(11)");
            });

            modelBuilder.Entity<RecommendedBooklist>(entity =>
            {
                entity.ToTable("recommended_booklist");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.BooklistId)
                    .HasColumnName("booklist_id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.CreatedAt)
                    .HasColumnName("created_at")
                    .HasColumnType("timestamp")
                    .HasDefaultValueSql("'CURRENT_TIMESTAMP'");

                entity.Property(e => e.UpdatedAt)
                    .HasColumnName("updated_at")
                    .HasColumnType("timestamp");

                entity.Property(e => e.WechatUserId)
                    .HasColumnName("wechat_user_id")
                    .HasColumnType("int(11)");
            });

            modelBuilder.Entity<ReviewLikes>(entity =>
            {
                entity.ToTable("review_likes");

                entity.HasIndex(e => e.Phone)
                    .HasName("user_user_id");

                entity.HasIndex(e => e.ReviewId)
                    .HasName("bk_review_id");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.CreatedAt)
                    .HasColumnName("created_at")
                    .HasColumnType("timestamp")
                    .HasDefaultValueSql("'CURRENT_TIMESTAMP'");

                entity.Property(e => e.Phone)
                    .HasColumnName("phone")
                    .HasColumnType("int(15)");

                entity.Property(e => e.ReviewId)
                    .HasColumnName("review_id")
                    .HasColumnType("int(11)");
            });

            modelBuilder.Entity<Reviews>(entity =>
            {
                entity.ToTable("reviews");

                entity.HasIndex(e => e.BookId)
                    .HasName("bk_book_id");

                entity.HasIndex(e => e.WechatUserId)
                    .HasName("user_user_id");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.BookId)
                    .HasColumnName("book_id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.Content)
                    .IsRequired()
                    .HasColumnName("content")
                    .HasColumnType("text");

                entity.Property(e => e.CreatedAt)
                    .HasColumnName("created_at")
                    .HasColumnType("timestamp")
                    .HasDefaultValueSql("'CURRENT_TIMESTAMP'");

                entity.Property(e => e.DeletedAt)
                    .HasColumnName("deleted_at")
                    .HasColumnType("timestamp");

                entity.Property(e => e.Score)
                    .HasColumnName("score")
                    .HasColumnType("int(11)");

                entity.Property(e => e.UpdatedAt)
                    .HasColumnName("updated_at")
                    .HasColumnType("timestamp");

                entity.Property(e => e.WechatUserId)
                    .HasColumnName("wechat_user_id")
                    .HasColumnType("int(11)");
            });

            modelBuilder.Entity<Tokens>(entity =>
            {
                entity.ToTable("tokens");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.CreatedAt)
                    .HasColumnName("created_at")
                    .HasColumnType("timestamp")
                    .HasDefaultValueSql("'CURRENT_TIMESTAMP'");

                entity.Property(e => e.IsUsed)
                    .HasColumnName("is_used")
                    .HasColumnType("int(11)")
                    .HasDefaultValueSql("'0'");

                entity.Property(e => e.Token)
                    .IsRequired()
                    .HasColumnName("token")
                    .HasColumnType("text");
            });

            modelBuilder.Entity<WechatUsers>(entity =>
            {
                entity.ToTable("wechat_users");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.Address)
                    .IsRequired()
                    .HasColumnName("address")
                    .HasColumnType("varchar(200)");

                entity.Property(e => e.Avatar)
                    .IsRequired()
                    .HasColumnName("avatar")
                    .HasColumnType("varchar(200)");

                entity.Property(e => e.Birthday)
                    .HasColumnName("birthday")
                    .HasColumnType("date");

                entity.Property(e => e.CreatedAt)
                    .HasColumnName("created_at")
                    .HasColumnType("timestamp")
                    .HasDefaultValueSql("'CURRENT_TIMESTAMP'");

                entity.Property(e => e.DeletedAt)
                    .HasColumnName("deleted_at")
                    .HasColumnType("timestamp");

                entity.Property(e => e.IdCardImg)
                    .IsRequired()
                    .HasColumnName("id_card_img")
                    .HasColumnType("text");

                entity.Property(e => e.IdNumber)
                    .IsRequired()
                    .HasColumnName("id_number")
                    .HasColumnType("varchar(20)");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnName("name")
                    .HasColumnType("varchar(100)");

                entity.Property(e => e.Nickname)
                    .IsRequired()
                    .HasColumnName("nickname")
                    .HasColumnType("varchar(50)");

                entity.Property(e => e.Openid)
                    .IsRequired()
                    .HasColumnName("openid")
                    .HasColumnType("varchar(50)");

                entity.Property(e => e.Phone)
                    .IsRequired()
                    .HasColumnName("phone")
                    .HasColumnType("varchar(50)");

                entity.Property(e => e.Postcode)
                    .IsRequired()
                    .HasColumnName("postcode")
                    .HasColumnType("varchar(10)");

                entity.Property(e => e.ReviewMsg)
                    .IsRequired()
                    .HasColumnName("review_msg")
                    .HasColumnType("varchar(200)");

                entity.Property(e => e.Status)
                    .HasColumnName("status")
                    .HasColumnType("int(11)")
                    .HasDefaultValueSql("'0'");

                entity.Property(e => e.UpdatedAt)
                    .HasColumnName("updated_at")
                    .HasColumnType("timestamp");
            });
        }
    }
}
