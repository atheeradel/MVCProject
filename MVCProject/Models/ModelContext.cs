using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using MVCProject.Models;

namespace MVCProject.Models;

public partial class ModelContext : DbContext
{
    public ModelContext()
    {
    }

    public ModelContext(DbContextOptions<ModelContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Aboutu> Aboutus { get; set; }

    public virtual DbSet<Category> Categories { get; set; }

    public virtual DbSet<Contactu> Contactus { get; set; }

    public virtual DbSet<Homepage> Homepages { get; set; }

    public virtual DbSet<Login> Logins { get; set; }

    public virtual DbSet<Recipe> Recipes { get; set; }

    public virtual DbSet<Role> Roles { get; set; }

    public virtual DbSet<Testimonal> Testimonals { get; set; }

    public virtual DbSet<Userinfo> Userinfos { get; set; }

    public virtual DbSet<Usermsg> Usermsgs { get; set; }

    public virtual DbSet<Userrecipe> Userrecipes { get; set; }

    public virtual DbSet<Visa> Visas { get; set; }

    public virtual DbSet<Wishlist> Wishlists { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseOracle("USER ID=C##MVCPRO;PASSWORD=Test321;DATA SOURCE=localhost:1521/xe");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder
            .HasDefaultSchema("C##MVCPRO")
            .UseCollation("USING_NLS_COMP");

        modelBuilder.Entity<Aboutu>(entity =>
        {
            entity.HasKey(e => e.AboutId).HasName("SYS_C008528");

            entity.ToTable("ABOUTUS");

            entity.Property(e => e.AboutId)
                .ValueGeneratedOnAdd()
                .HasColumnType("NUMBER")
                .HasColumnName("ABOUT_ID");
            entity.Property(e => e.Aboutusdescription)
                .HasMaxLength(240)
                .IsUnicode(false)
                .HasColumnName("ABOUTUSDESCRIPTION");
            entity.Property(e => e.Aboutusimgbanner)
                .HasMaxLength(150)
                .IsUnicode(false)
                .HasColumnName("ABOUTUSIMGBANNER");
            entity.Property(e => e.Aboutusmainimg)
                .HasMaxLength(150)
                .IsUnicode(false)
                .HasColumnName("ABOUTUSMAINIMG");
            entity.Property(e => e.Expertinfo)
                .HasMaxLength(200)
                .IsUnicode(false)
                .HasColumnName("EXPERTINFO");
            entity.Property(e => e.Profinfo)
                .HasMaxLength(200)
                .IsUnicode(false)
                .HasColumnName("PROFINFO");
            entity.Property(e => e.Trustedinfo)
                .HasMaxLength(200)
                .IsUnicode(false)
                .HasColumnName("TRUSTEDINFO");
        });

        modelBuilder.Entity<Category>(entity =>
        {
            entity.HasKey(e => e.CatId).HasName("SYS_C008500");

            entity.ToTable("CATEGORY");

            entity.Property(e => e.CatId)
                .ValueGeneratedOnAdd()
                .HasColumnType("NUMBER")
                .HasColumnName("CAT_ID");
            entity.Property(e => e.CatDes)
                .HasMaxLength(230)
                .IsUnicode(false)
                .HasColumnName("CAT_DES");
            entity.Property(e => e.CatImg)
                .HasMaxLength(150)
                .IsUnicode(false)
                .HasColumnName("CAT_IMG");
            entity.Property(e => e.CatName)
                .HasMaxLength(55)
                .IsUnicode(false)
                .HasColumnName("CAT_NAME");
        });

        modelBuilder.Entity<Contactu>(entity =>
        {
            entity.HasKey(e => e.ContId).HasName("SYS_C008530");

            entity.ToTable("CONTACTUS");

            entity.Property(e => e.ContId)
                .ValueGeneratedOnAdd()
                .HasColumnType("NUMBER")
                .HasColumnName("CONT_ID");
            entity.Property(e => e.Contactinfo)
                .HasMaxLength(240)
                .IsUnicode(false)
                .HasColumnName("CONTACTINFO");
            entity.Property(e => e.Contactusbannerimg)
                .HasMaxLength(150)
                .IsUnicode(false)
                .HasColumnName("CONTACTUSBANNERIMG");
        });

        modelBuilder.Entity<Homepage>(entity =>
        {
            entity.HasKey(e => e.PageId).HasName("SYS_C008526");

            entity.ToTable("HOMEPAGE");

            entity.Property(e => e.PageId)
                .ValueGeneratedOnAdd()
                .HasColumnType("NUMBER")
                .HasColumnName("PAGE_ID");
            entity.Property(e => e.Bannerdescription)
                .HasMaxLength(230)
                .IsUnicode(false)
                .HasColumnName("BANNERDESCRIPTION");
            entity.Property(e => e.Imgbannerone)
                .HasMaxLength(150)
                .IsUnicode(false)
                .HasColumnName("IMGBANNERONE");
            entity.Property(e => e.Imgbannerthree)
                .HasMaxLength(150)
                .IsUnicode(false)
                .HasColumnName("IMGBANNERTHREE");
            entity.Property(e => e.Imgbannertwo)
                .HasMaxLength(150)
                .IsUnicode(false)
                .HasColumnName("IMGBANNERTWO");
            entity.Property(e => e.Imglogo)
                .HasMaxLength(150)
                .IsUnicode(false)
                .HasColumnName("IMGLOGO");
            entity.Property(e => e.Imgonedesign)
                .HasMaxLength(150)
                .IsUnicode(false)
                .HasColumnName("IMGONEDESIGN");
            entity.Property(e => e.Imgtwodesign)
                .HasMaxLength(150)
                .IsUnicode(false)
                .HasColumnName("IMGTWODESIGN");
            entity.Property(e => e.Phonenum)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("PHONENUM");
        });

        modelBuilder.Entity<Login>(entity =>
        {
            entity.HasKey(e => e.LogId).HasName("SYS_C008568");

            entity.ToTable("LOGIN");

            entity.Property(e => e.LogId)
                .ValueGeneratedOnAdd()
                .HasColumnType("NUMBER")
                .HasColumnName("LOG_ID");
            entity.Property(e => e.Password)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("PASSWORD");
            entity.Property(e => e.UserId)
                .HasColumnType("NUMBER")
                .HasColumnName("USER_ID");
            entity.Property(e => e.Username)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("USERNAME");

            entity.HasOne(d => d.User).WithMany(p => p.Logins)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("SYS_C008569");
        });

        modelBuilder.Entity<Recipe>(entity =>
        {
            entity.HasKey(e => e.RecId).HasName("SYS_C008571");

            entity.ToTable("RECIPE");

            entity.Property(e => e.RecId)
                .ValueGeneratedOnAdd()
                .HasColumnType("NUMBER")
                .HasColumnName("REC_ID");
            entity.Property(e => e.CatId)
                .HasColumnType("NUMBER")
                .HasColumnName("CAT_ID");
            entity.Property(e => e.Dateadd)
                .HasColumnType("DATE")
                .HasColumnName("DATEADD");
            entity.Property(e => e.Image)
                .HasMaxLength(200)
                .IsUnicode(false)
                .HasColumnName("IMAGE");
            entity.Property(e => e.Ingrediants)
                .HasMaxLength(200)
                .IsUnicode(false)
                .HasColumnName("INGREDIANTS");
            entity.Property(e => e.Instruction)
                .HasMaxLength(240)
                .IsUnicode(false)
                .HasColumnName("INSTRUCTION");
            entity.Property(e => e.Name)
                .HasMaxLength(55)
                .IsUnicode(false)
                .HasColumnName("NAME");
            entity.Property(e => e.Price)
                .HasColumnType("NUMBER(38)")
                .HasColumnName("PRICE");
            entity.Property(e => e.Status)
                .HasDefaultValueSql("2")
                .HasColumnType("NUMBER")
                .HasColumnName("STATUS");
            entity.Property(e => e.UserId)
                .HasColumnType("NUMBER")
                .HasColumnName("USER_ID");

            entity.HasOne(d => d.Cat).WithMany(p => p.Recipes)
                .HasForeignKey(d => d.CatId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("SYS_C008573");

            entity.HasOne(d => d.User).WithMany(p => p.Recipes)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("SYS_C008572");
        });

        modelBuilder.Entity<Role>(entity =>
        {
            entity.HasKey(e => e.RoleId).HasName("SYS_C008492");

            entity.ToTable("ROLE");

            entity.Property(e => e.RoleId)
                .ValueGeneratedOnAdd()
                .HasColumnType("NUMBER")
                .HasColumnName("ROLE_ID");
            entity.Property(e => e.RoleName)
                .HasMaxLength(55)
                .IsUnicode(false)
                .HasColumnName("ROLE_NAME");
        });

        modelBuilder.Entity<Testimonal>(entity =>
        {
            entity.HasKey(e => e.TestId).HasName("SYS_C008579");

            entity.ToTable("TESTIMONAL");

            entity.Property(e => e.TestId)
                .ValueGeneratedOnAdd()
                .HasColumnType("NUMBER")
                .HasColumnName("TEST_ID");
            entity.Property(e => e.DateAdd)
                .HasColumnType("DATE")
                .HasColumnName("DATE_ADD");
            entity.Property(e => e.Msg)
                .HasMaxLength(230)
                .IsUnicode(false)
                .HasColumnName("MSG");
            entity.Property(e => e.Status)
                .HasDefaultValueSql("2")
                .HasColumnType("NUMBER")
                .HasColumnName("STATUS");
            entity.Property(e => e.UserId)
                .HasColumnType("NUMBER")
                .HasColumnName("USER_ID");

            entity.HasOne(d => d.User).WithMany(p => p.Testimonals)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("SYS_C008580");
        });

        modelBuilder.Entity<Userinfo>(entity =>
        {
            entity.HasKey(e => e.UserId).HasName("SYS_C008563");

            entity.ToTable("USERINFO");

            entity.Property(e => e.UserId)
                .ValueGeneratedOnAdd()
                .HasColumnType("NUMBER")
                .HasColumnName("USER_ID");
            entity.Property(e => e.Address)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("ADDRESS");
            entity.Property(e => e.Age)
                .HasColumnType("NUMBER")
                .HasColumnName("AGE");
            entity.Property(e => e.Email)
                .HasMaxLength(55)
                .IsUnicode(false)
                .HasColumnName("EMAIL");
            entity.Property(e => e.Firstname)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("FIRSTNAME");
            entity.Property(e => e.ImagePath)
                .HasMaxLength(200)
                .IsUnicode(false)
                .HasColumnName("IMAGE_PATH");
            entity.Property(e => e.Lastname)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("LASTNAME");
            entity.Property(e => e.Phonenum)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("PHONENUM");
            entity.Property(e => e.RoleId)
                .HasColumnType("NUMBER")
                .HasColumnName("ROLE_ID");

            entity.HasOne(d => d.Role).WithMany(p => p.Userinfos)
                .HasForeignKey(d => d.RoleId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("SYS_C008564");
        });

        modelBuilder.Entity<Usermsg>(entity =>
        {
            entity.HasKey(e => e.MsgId).HasName("SYS_C008586");

            entity.ToTable("USERMSG");

            entity.Property(e => e.MsgId)
                .ValueGeneratedOnAdd()
                .HasColumnType("NUMBER")
                .HasColumnName("MSG_ID");
            entity.Property(e => e.Email)
                .HasMaxLength(55)
                .IsUnicode(false)
                .HasColumnName("EMAIL");
            entity.Property(e => e.Msg)
                .HasMaxLength(240)
                .IsUnicode(false)
                .HasColumnName("MSG");
            entity.Property(e => e.Subject)
                .HasMaxLength(55)
                .IsUnicode(false)
                .HasColumnName("SUBJECT");
            entity.Property(e => e.UserId)
                .HasColumnType("NUMBER")
                .HasColumnName("USER_ID");
            entity.Property(e => e.Username)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("USERNAME");

            entity.HasOne(d => d.User).WithMany(p => p.Usermsgs)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("SYS_C008587");
        });

        modelBuilder.Entity<Userrecipe>(entity =>
        {
            entity.HasKey(e => e.ReqId).HasName("SYS_C008575");

            entity.ToTable("USERRECIPE");

            entity.Property(e => e.ReqId)
                .ValueGeneratedOnAdd()
                .HasColumnType("NUMBER")
                .HasColumnName("REQ_ID");
            entity.Property(e => e.RecId)
                .HasColumnType("NUMBER")
                .HasColumnName("REC_ID");
            entity.Property(e => e.ReqDate)
                .HasColumnType("DATE")
                .HasColumnName("REQ_DATE");
            entity.Property(e => e.Status)
                .HasDefaultValueSql("2")
                .HasColumnType("NUMBER")
                .HasColumnName("STATUS");
            entity.Property(e => e.UserId)
                .HasColumnType("NUMBER")
                .HasColumnName("USER_ID");

            entity.HasOne(d => d.Rec).WithMany(p => p.Userrecipes)
                .HasForeignKey(d => d.RecId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("SYS_C008577");

            entity.HasOne(d => d.User).WithMany(p => p.Userrecipes)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("SYS_C008576");
        });

        modelBuilder.Entity<Visa>(entity =>
        {
            entity.HasKey(e => e.VisaId).HasName("SYS_C008514");

            entity.ToTable("VISA");

            entity.Property(e => e.VisaId)
                .ValueGeneratedOnAdd()
                .HasColumnType("NUMBER")
                .HasColumnName("VISA_ID");
            entity.Property(e => e.Amountofmoney)
                .HasColumnType("NUMBER(38)")
                .HasColumnName("AMOUNTOFMONEY");
            entity.Property(e => e.Cardname)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("CARDNAME");
            entity.Property(e => e.Cardnum)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("CARDNUM");
            entity.Property(e => e.Cvc)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("CVC");
            entity.Property(e => e.Expiredate)
                .HasColumnType("DATE")
                .HasColumnName("EXPIREDATE");
        });

        modelBuilder.Entity<Wishlist>(entity =>
        {
            entity.HasKey(e => e.WishId).HasName("SYS_C008582");

            entity.ToTable("WISHLIST");

            entity.Property(e => e.WishId)
                .ValueGeneratedOnAdd()
                .HasColumnType("NUMBER")
                .HasColumnName("WISH_ID");
            entity.Property(e => e.RecId)
                .HasColumnType("NUMBER")
                .HasColumnName("REC_ID");
            entity.Property(e => e.UserId)
                .HasColumnType("NUMBER")
                .HasColumnName("USER_ID");

            entity.HasOne(d => d.Rec).WithMany(p => p.Wishlists)
                .HasForeignKey(d => d.RecId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("SYS_C008584");

            entity.HasOne(d => d.User).WithMany(p => p.Wishlists)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("SYS_C008583");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);

    public DbSet<MVCProject.Models.Profilephoto>? Profilephoto { get; set; }
}
