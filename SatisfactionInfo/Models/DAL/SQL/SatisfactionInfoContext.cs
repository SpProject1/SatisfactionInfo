using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace SatisfactionInfo.Models.DAL.SQL
{
    public partial class SatisfactionInfoContext : DbContext
    {
        public SatisfactionInfoContext(DbContextOptions<SatisfactionInfoContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Answers> Answers { get; set; }
        public virtual DbSet<AnswerTypes> AnswerTypes { get; set; }   
        public virtual DbSet<AspNetUsers> AspNetUsers { get; set; }
        public virtual DbSet<Questionnaries> Questionnaries { get; set; }
        public virtual DbSet<QuestionnariesQuestion> QuestionnariesQuestion { get; set; }
        public virtual DbSet<Questions> Questions { get; set; }
        public virtual DbSet<QuestionsAnswer> QuestionsAnswer { get; set; }
        public virtual DbSet<UserAnswers> UserAnswers { get; set; }
        public virtual DbSet<VUserQuestionnarie> VUserQuestionnarie { get; set; }

     
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("ProductVersion", "2.2.0-preview3-35497");

            modelBuilder.Entity<Answers>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Answer)
                    .IsRequired()
                    .HasMaxLength(250);

                entity.Property(e => e.Weight).HasColumnType("decimal(18, 2)");                               
            });

            modelBuilder.Entity<AnswerTypes>(entity =>
            {
                entity.HasKey(e => e.AnswerType)
                    .HasName("PK__AnswerTy__2AADA07E48179128");

                entity.Property(e => e.AnswerType)
                    .HasMaxLength(20)
                    .ValueGeneratedNever();
            });

            modelBuilder.Entity<AspNetUsers>(entity =>
            {
                entity.HasIndex(e => e.NormalizedEmail)
                    .HasName("EmailIndex");

                entity.HasIndex(e => e.NormalizedUserName)
                    .HasName("UserNameIndex")
                    .IsUnique()
                    .HasFilter("([NormalizedUserName] IS NOT NULL)");

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.Email).HasMaxLength(256);

                entity.Property(e => e.NormalizedEmail).HasMaxLength(256);

                entity.Property(e => e.NormalizedUserName).HasMaxLength(256);

                entity.Property(e => e.UserName).HasMaxLength(256);
            });           

            modelBuilder.Entity<Questionnaries>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<QuestionnariesQuestion>(entity =>
            {
                entity.HasKey(e => new { e.QuestionId, e.QuestionnarieId });

                entity.Property(e => e.QuestionId).HasColumnName("QuestionID");

                entity.Property(e => e.QuestionnarieId).HasColumnName("QuestionnarieID");

                entity.HasOne(d => d.Question)
                    .WithMany(p => p.QuestionnariesQuestion)
                    .HasForeignKey(d => d.QuestionId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_QuestionID");

                entity.HasOne(d => d.Questionnarie)
                    .WithMany(p => p.QuestionnariesQuestion)
                    .HasForeignKey(d => d.QuestionnarieId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_QuestionnarieID");
            });

            modelBuilder.Entity<Questions>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Question)
                    .IsRequired()
                    .HasMaxLength(250);
            });

            modelBuilder.Entity<QuestionsAnswer>(entity =>
            {
                entity.HasKey(e => new { e.AnswerId, e.QuestionId })
                    .HasName("PK_QuestionAnswer");

                entity.Property(e => e.AnswerId).HasColumnName("AnswerID");

                entity.Property(e => e.QuestionId).HasColumnName("QuestionID");

                entity.HasOne(d => d.Answer)
                    .WithMany(p => p.QuestionsAnswer)
                    .HasForeignKey(d => d.AnswerId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_QuestionsIDAnswer");

                entity.HasOne(d => d.Question)
                    .WithMany(p => p.QuestionsAnswer)
                    .HasForeignKey(d => d.QuestionId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_AnswerIDQuestion");
            });

            modelBuilder.Entity<UserAnswers>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Date).HasColumnType("datetime");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50);
            });
        }
    }
}
