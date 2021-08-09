using System.ComponentModel.DataAnnotations;
using System.Drawing;
using Domain.Enums;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Application.Common.DTOs
{
    public record TeamDto
    {
        [Required]
        public string Name { get; init; }
        [Required]
        public int Position { get; init; }
        [Required]
        [JsonConverter(typeof(StringEnumConverter))]
        public KnownColor HomeKitColor { get; init; }
        [Required]
        public string Stadium { get; init; }
        [Required]
        public Cities City { get; init; }
    }
}