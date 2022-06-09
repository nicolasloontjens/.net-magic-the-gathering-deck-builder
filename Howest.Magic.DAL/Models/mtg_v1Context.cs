using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace Howest.MagicCards.DAL.Models
{
    public partial class mtg_v1Context : DbContext
    {
        public mtg_v1Context()
        {
        }

        public mtg_v1Context(DbContextOptions<mtg_v1Context> options)
            : base(options)
        {
        }

        public virtual DbSet<Artist> Artists { get; set; }
        public virtual DbSet<Card> Cards { get; set; }
        public virtual DbSet<CardColor> CardColors { get; set; }
        public virtual DbSet<CardType> CardTypes { get; set; }
        public virtual DbSet<Color> Colors { get; set; }
        public virtual DbSet<Deck> Decks { get; set; }
        public virtual DbSet<Migration> Migrations { get; set; }
        public virtual DbSet<PersonalAccessToken> PersonalAccessTokens { get; set; }
        public virtual DbSet<Rarity> Rarities { get; set; }
        public virtual DbSet<Set> Sets { get; set; }
        public virtual DbSet<Type> Types { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Server=PC\\SQLEXPRESS;Initial Catalog=mtg_v1;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "SQL_Latin1_General_CP1_CI_AS");

            modelBuilder.Entity<Artist>(entity =>
            {
                entity.ToTable("artists");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.CreatedAt)
                    .HasColumnType("datetime")
                    .HasColumnName("created_at");

                entity.Property(e => e.FullName)
                    .IsRequired()
                    .HasMaxLength(255)
                    .HasColumnName("full_name");

                entity.Property(e => e.UpdatedAt)
                    .HasColumnType("datetime")
                    .HasColumnName("updated_at");
            });

            modelBuilder.Entity<Card>(entity =>
            {
                entity.ToTable("cards");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.ArtistId).HasColumnName("artist_id");

                entity.Property(e => e.ConvertedManaCost)
                    .IsRequired()
                    .HasMaxLength(255)
                    .HasColumnName("converted_mana_cost");

                entity.Property(e => e.CreatedAt)
                    .HasColumnType("datetime")
                    .HasColumnName("created_at");

                entity.Property(e => e.Flavor).HasColumnName("flavor");

                entity.Property(e => e.Image)
                    .IsRequired()
                    .HasMaxLength(255)
                    .HasColumnName("image");

                entity.Property(e => e.Layout)
                    .IsRequired()
                    .HasMaxLength(255)
                    .HasColumnName("layout");

                entity.Property(e => e.ManaCost)
                    .HasMaxLength(255)
                    .HasColumnName("mana_cost");

                entity.Property(e => e.MtgId)
                    .IsRequired()
                    .HasMaxLength(255)
                    .HasColumnName("mtg_id");

                entity.Property(e => e.MultiverseId).HasColumnName("multiverse_id");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(255)
                    .HasColumnName("name");

                entity.Property(e => e.Number)
                    .IsRequired()
                    .HasMaxLength(255)
                    .HasColumnName("number");

                entity.Property(e => e.OriginalImageUrl)
                    .HasMaxLength(255)
                    .HasColumnName("original_image_url");

                entity.Property(e => e.OriginalText).HasColumnName("original_text");

                entity.Property(e => e.OriginalType)
                    .HasMaxLength(255)
                    .HasColumnName("original_type");

                entity.Property(e => e.Power)
                    .HasMaxLength(255)
                    .HasColumnName("power");

                entity.Property(e => e.RarityCode)
                    .HasMaxLength(255)
                    .HasColumnName("rarity_code");

                entity.Property(e => e.SetCode)
                    .IsRequired()
                    .HasMaxLength(255)
                    .HasColumnName("set_code");

                entity.Property(e => e.Text).HasColumnName("text");

                entity.Property(e => e.Toughness)
                    .HasMaxLength(255)
                    .HasColumnName("toughness");

                entity.Property(e => e.Type)
                    .IsRequired()
                    .HasMaxLength(255)
                    .HasColumnName("type");

                entity.Property(e => e.UpdatedAt)
                    .HasColumnType("datetime")
                    .HasColumnName("updated_at");

                entity.Property(e => e.Variations).HasColumnName("variations");

                entity.HasOne(d => d.Artist)
                    .WithMany(p => p.Cards)
                    .HasForeignKey(d => d.ArtistId)
                    .HasConstraintName("FK_cards_artists");

                entity.HasOne(d => d.RarityCodeNavigation)
                    .WithMany(p => p.Cards)
                    .HasPrincipalKey(p => p.Code)
                    .HasForeignKey(d => d.RarityCode)
                    .HasConstraintName("FK_cards_rarities");

                entity.HasOne(d => d.SetCodeNavigation)
                    .WithMany(p => p.Cards)
                    .HasPrincipalKey(p => p.Code)
                    .HasForeignKey(d => d.SetCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_cards_sets");
            });

            modelBuilder.Entity<CardColor>(entity =>
            {
                entity.HasKey(e => new { e.CardId, e.ColorId })
                    .HasName("card_colors_card_id_color_id_primary");

                entity.ToTable("card_colors");

                entity.HasIndex(e => new { e.CardId, e.ColorId }, "card_colors_card_id_color_id_unique")
                    .IsUnique();

                entity.Property(e => e.CardId).HasColumnName("card_id");

                entity.Property(e => e.ColorId).HasColumnName("color_id");

                entity.Property(e => e.CreatedAt)
                    .HasColumnType("datetime")
                    .HasColumnName("created_at");

                entity.Property(e => e.UpdatedAt)
                    .HasColumnType("datetime")
                    .HasColumnName("updated_at");

                entity.HasOne(d => d.Card)
                    .WithMany(p => p.CardColors)
                    .HasForeignKey(d => d.CardId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_card_colors_cards");

                entity.HasOne(d => d.Color)
                    .WithMany(p => p.CardColors)
                    .HasForeignKey(d => d.ColorId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_card_colors_colors");
            });

            modelBuilder.Entity<CardType>(entity =>
            {
                entity.HasKey(e => new { e.CardId, e.TypeId })
                    .HasName("card_types_card_id_type_id_primary");

                entity.ToTable("card_types");

                entity.HasIndex(e => new { e.CardId, e.TypeId }, "card_types_card_id_type_id_unique")
                    .IsUnique();

                entity.Property(e => e.CardId).HasColumnName("card_id");

                entity.Property(e => e.TypeId).HasColumnName("type_id");

                entity.Property(e => e.CreatedAt)
                    .HasColumnType("datetime")
                    .HasColumnName("created_at");

                entity.Property(e => e.UpdatedAt)
                    .HasColumnType("datetime")
                    .HasColumnName("updated_at");

                entity.HasOne(d => d.Card)
                    .WithMany(p => p.CardTypes)
                    .HasForeignKey(d => d.CardId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_card_types_cards");

                entity.HasOne(d => d.Type)
                    .WithMany(p => p.CardTypes)
                    .HasForeignKey(d => d.TypeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_card_types_types");
            });

            modelBuilder.Entity<Color>(entity =>
            {
                entity.ToTable("colors");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Code)
                    .IsRequired()
                    .HasMaxLength(255)
                    .HasColumnName("code");

                entity.Property(e => e.CreatedAt)
                    .HasColumnType("datetime")
                    .HasColumnName("created_at");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(255)
                    .HasColumnName("name");

                entity.Property(e => e.UpdatedAt)
                    .HasColumnType("datetime")
                    .HasColumnName("updated_at");
            });

            modelBuilder.Entity<Deck>(entity =>
            {
                entity.ToTable("decks");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Cards).HasColumnName("cards");

                entity.Property(e => e.Password).HasColumnName("password");
            });

            modelBuilder.Entity<Migration>(entity =>
            {
                entity.ToTable("migrations");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Batch).HasColumnName("batch");

                entity.Property(e => e.Migration1)
                    .IsRequired()
                    .HasMaxLength(255)
                    .HasColumnName("migration");
            });

            modelBuilder.Entity<PersonalAccessToken>(entity =>
            {
                entity.ToTable("personal_access_tokens");

                entity.HasIndex(e => e.Token, "personal_access_tokens_token_unique")
                    .IsUnique();

                entity.HasIndex(e => new { e.TokenableType, e.TokenableId }, "personal_access_tokens_tokenable_type_tokenable_id_index");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Abilities).HasColumnName("abilities");

                entity.Property(e => e.CreatedAt)
                    .HasColumnType("datetime")
                    .HasColumnName("created_at");

                entity.Property(e => e.LastUsedAt)
                    .HasColumnType("datetime")
                    .HasColumnName("last_used_at");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(255)
                    .HasColumnName("name");

                entity.Property(e => e.Token)
                    .IsRequired()
                    .HasMaxLength(64)
                    .HasColumnName("token");

                entity.Property(e => e.TokenableId).HasColumnName("tokenable_id");

                entity.Property(e => e.TokenableType)
                    .IsRequired()
                    .HasMaxLength(255)
                    .HasColumnName("tokenable_type");

                entity.Property(e => e.UpdatedAt)
                    .HasColumnType("datetime")
                    .HasColumnName("updated_at");
            });

            modelBuilder.Entity<Rarity>(entity =>
            {
                entity.ToTable("rarities");

                entity.HasIndex(e => e.Code, "rarities_code_unique")
                    .IsUnique();

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Code)
                    .IsRequired()
                    .HasMaxLength(255)
                    .HasColumnName("code");

                entity.Property(e => e.CreatedAt)
                    .HasColumnType("datetime")
                    .HasColumnName("created_at");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(255)
                    .HasColumnName("name");

                entity.Property(e => e.UpdatedAt)
                    .HasColumnType("datetime")
                    .HasColumnName("updated_at");
            });

            modelBuilder.Entity<Set>(entity =>
            {
                entity.ToTable("sets");

                entity.HasIndex(e => e.Code, "sets_code_unique")
                    .IsUnique();

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Code)
                    .IsRequired()
                    .HasMaxLength(255)
                    .HasColumnName("code");

                entity.Property(e => e.CreatedAt)
                    .HasColumnType("datetime")
                    .HasColumnName("created_at");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(255)
                    .HasColumnName("name");

                entity.Property(e => e.UpdatedAt)
                    .HasColumnType("datetime")
                    .HasColumnName("updated_at");
            });

            modelBuilder.Entity<Type>(entity =>
            {
                entity.ToTable("types");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.CreatedAt)
                    .HasColumnType("datetime")
                    .HasColumnName("created_at");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(255)
                    .HasColumnName("name");

                entity.Property(e => e.Type1)
                    .IsRequired()
                    .HasMaxLength(255)
                    .HasColumnName("type");

                entity.Property(e => e.UpdatedAt)
                    .HasColumnType("datetime")
                    .HasColumnName("updated_at");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
