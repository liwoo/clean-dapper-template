using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Drawing;
using Domain.Enums;

namespace Domain.Entities
{
    [Table("teams")]
    public record Team
    {
        public int Id { get; init; }
        public string Name { get; init; }
        public int Position { get; init; }
        [Column("home_kit_color")]
        public KnownColor HomeKitColor { get; init; }
        public string Stadium { get; init; }
        public Cities City { get; init; }
    }
}