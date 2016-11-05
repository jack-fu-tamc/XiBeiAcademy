using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace HC.Data.Models.Mapping
{
    public class NewsMap : EntityTypeConfiguration<News>
    {
        public NewsMap()
        {
            // Primary Key
            this.HasKey(t => t.NewsID);

            // Properties
            this.Property(t => t.NewsTitle)
                .IsRequired()
                .HasMaxLength(100);

            this.Property(t => t.Author)
                .HasMaxLength(50);

            this.Property(t => t.PicURL)
                .HasMaxLength(150);

            // Table & Column Mappings
            this.ToTable("News");
            this.Property(t => t.NewsID).HasColumnName("NewsID");
            this.Property(t => t.NewsTitle).HasColumnName("NewsTitle");
            this.Property(t => t.ClassID).HasColumnName("ClassID");
            this.Property(t => t.NewsOrder).HasColumnName("NewsOrder");
            this.Property(t => t.Author).HasColumnName("Author");
            this.Property(t => t.NewsContent).HasColumnName("NewsContent");
            this.Property(t => t.PicURL).HasColumnName("PicURL");
            this.Property(t => t.NewsType).HasColumnName("NewsType");
            this.Property(t => t.NewsTitleCount).HasColumnName("NewsTitleCount");
            this.Property(t => t.contentCount).HasColumnName("contentCount");
            this.Property(t => t.isDelete).HasColumnName("isDelete");
            this.Property(t => t.CreatTime).HasColumnName("CreatTime");
            this.Property(t => t.ClickNum).HasColumnName("ClickNum");
            this.Property(t => t.IsHot).HasColumnName("IsHot");
            this.Property(t => t.IsRec).HasColumnName("IsRec");
            this.Property(t => t.ArtistLevel).HasColumnName("ArtistLevel");
            this.Property(t => t.ArtistTypeID).HasColumnName("ArtistTypeID");
            this.Property(t => t.DeputyTitle).HasColumnName("DeputyTitle");
            this.Property(t => t.DeleteTime).HasColumnName("DeleteTime");
            // Relationships
            this.HasOptional(t => t.ArtistType)
                .WithMany(t => t.News)
                .HasForeignKey(d => d.ArtistTypeID);
            this.HasRequired(t => t.NewsClass)
                .WithMany(t => t.News)
                .HasForeignKey(d => d.ClassID);

        }
    }
}
