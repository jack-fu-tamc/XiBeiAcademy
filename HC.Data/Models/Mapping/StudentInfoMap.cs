using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace HC.Data.Models.Mapping
{
    public class StudentInfoMap : EntityTypeConfiguration<StudentInfo>
    {
        public StudentInfoMap()
        {
            // Primary Key
            this.HasKey(t => t.StudentID);

            // Properties
            this.Property(t => t.Sex)
                .IsRequired()
                .HasMaxLength(10);

            this.Property(t => t.Stype)
                .IsRequired()
                .HasMaxLength(50);

            this.Property(t => t.Scategory)
                .IsRequired()
                .HasMaxLength(50);

            this.Property(t => t.Sfrom)
                .IsRequired()
                .HasMaxLength(50);

            this.Property(t => t.SchoolName)
                .IsRequired()
                .HasMaxLength(100);

            this.Property(t => t.SelfCardNo)
                .IsRequired()
                .HasMaxLength(50);

            this.Property(t => t.SeflPhone)
                .IsRequired()
                .HasMaxLength(50);

            this.Property(t => t.FatherPhone)
                .IsRequired()
                .HasMaxLength(50);

            this.Property(t => t.MotherPhone)
                .IsRequired()
                .HasMaxLength(50);

            this.Property(t => t.TelNum)
                .IsRequired()
                .HasMaxLength(50);

            this.Property(t => t.major)
                .IsRequired()
                .HasMaxLength(50);

            this.Property(t => t.SendAddress)
                .IsRequired()
                .HasMaxLength(150);

            this.Property(t => t.ZipCode)
                .IsRequired()
                .HasMaxLength(10);

            this.Property(t => t.ReceiveName)
                .IsRequired()
                .HasMaxLength(50);

            this.Property(t => t.ReceivePhone)
                .IsRequired()
                .HasMaxLength(50);

            this.Property(t => t.Sphoto)
                .IsRequired()
                .HasMaxLength(150);

            this.Property(t => t.registerNo)
                .IsRequired()
                .HasMaxLength(50);

            this.Property(t => t.StudentName)
                .IsRequired()
                .HasMaxLength(50);

            // Table & Column Mappings 
            this.ToTable("StudentInfo");
            this.Property(t => t.StudentID).HasColumnName("StudentID");
            this.Property(t => t.Sex).HasColumnName("Sex");
            this.Property(t => t.Stype).HasColumnName("Stype");
            this.Property(t => t.Scategory).HasColumnName("Scategory");
            this.Property(t => t.Sfrom).HasColumnName("Sfrom");
            this.Property(t => t.SchoolName).HasColumnName("SchoolName");
            this.Property(t => t.SelfCardNo).HasColumnName("SelfCardNo");
            this.Property(t => t.SeflPhone).HasColumnName("SeflPhone");
            this.Property(t => t.FatherPhone).HasColumnName("FatherPhone");
            this.Property(t => t.MotherPhone).HasColumnName("MotherPhone");
            this.Property(t => t.TelNum).HasColumnName("TelNum");
            this.Property(t => t.major).HasColumnName("major");
            this.Property(t => t.SendAddress).HasColumnName("SendAddress");
            this.Property(t => t.ZipCode).HasColumnName("ZipCode");
            this.Property(t => t.ReceiveName).HasColumnName("ReceiveName");
            this.Property(t => t.ReceivePhone).HasColumnName("ReceivePhone");
            this.Property(t => t.Sphoto).HasColumnName("Sphoto");
            this.Property(t => t.registerNo).HasColumnName("registerNo");
            this.Property(t => t.RegisterTime).HasColumnName("RegisterTime");
            this.Property(t => t.StudentName).HasColumnName("StudentName");
        } 
    }
}
