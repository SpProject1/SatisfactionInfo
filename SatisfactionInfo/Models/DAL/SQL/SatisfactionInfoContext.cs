using System;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace SatisfactionInfo.Models.DAL.SQL
{
    public partial class SatisfactionInfoContext : IdentityDbContext
    {      
        public SatisfactionInfoContext(DbContextOptions<SatisfactionInfoContext> options)
            : base(options)
        {
        }
        public virtual DbSet<Answers> Answers { get; set; }
        public virtual DbSet<AnswerTypes> AnswerTypes { get; set; }  
        public virtual DbSet<Questionnaries> Questionnaries { get; set; }
        public virtual DbSet<QuestionnariesQuestion> QuestionnariesQuestion { get; set; }
        public virtual DbSet<Questions> Questions { get; set; }
        public virtual DbSet<QuestionsAnswer> QuestionsAnswer { get; set; }
        public virtual DbSet<UserQuestionnarieAnswers> UserQuestionnarieAnswers { get; set; }
        public virtual DbSet<UserQuestionnaries> UserQuestionnaries { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.HasAnnotation("ProductVersion", "2.2.0-preview3-35497");

            modelBuilder.Entity<Answers>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Answer)
                    .IsRequired()
                    .HasMaxLength(250);
            });

            modelBuilder.Entity<AnswerTypes>(entity =>
            {
                entity.HasKey(e => e.AnswerType)
                    .HasName("PK__AnswerTy__2AADA07E48179128");

                entity.Property(e => e.AnswerType)
                    .HasMaxLength(20)
                    .ValueGeneratedNever();
            });
                        
            modelBuilder.Entity<Questionnaries>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Code).HasMaxLength(5);

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

                entity.Property(e => e.AddWhyName).HasMaxLength(250);

                entity.Property(e => e.AnswerType).HasMaxLength(20);

                entity.Property(e => e.Question)
                    .IsRequired()
                    .HasMaxLength(250);

                entity.HasOne(d => d.AnswerTypeNavigation)
                    .WithMany(p => p.Questions)
                    .HasForeignKey(d => d.AnswerType)
                    .HasConstraintName("FK_Questions_AnswerTypes");
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

            modelBuilder.Entity<UserQuestionnarieAnswers>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.AddWhyName).HasMaxLength(250);

                entity.Property(e => e.AnswerType).HasMaxLength(20);

                entity.Property(e => e.Code).HasMaxLength(5);

                entity.Property(e => e.Question).HasMaxLength(250);

                entity.Property(e => e.UserQuestionnarieId).HasColumnName("UserQuestionnarieID");

                entity.HasOne(d => d.UserQuestionnarie)
                    .WithMany(p => p.UserQuestionnarieAnswers)
                    .HasForeignKey(d => d.UserQuestionnarieId)
                    .HasConstraintName("FK_UserQuestionnarieAnswers_UserQuestionnaries");
            });

            modelBuilder.Entity<UserQuestionnaries>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Code).HasMaxLength(5);

                entity.Property(e => e.Date).HasColumnType("datetime");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50);
            });
            base.OnModelCreating(modelBuilder);
        }
    }
}
