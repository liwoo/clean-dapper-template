using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Drawing;
using Domain.Enums;

namespace Domain.Entities
{
    [Table("teams")]
    public record Team
    {
        [Key]
        [Column("id")]
        public int Id { get; init; }

        [Column("name")]
        public string Name { get; init; }

        [Column("position")]
        public int Position { get; init; }

        [Column("stadium")]
        public string Stadium { get; init; }

        [Column("home_kit_color")]
        public KnownColor HomeKitColor { get; init; }

        [Column("city")]
        public Cities City { get; init; }
    }
}