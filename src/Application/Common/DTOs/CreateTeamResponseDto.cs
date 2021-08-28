namespace Application.Common.DTOs
{
    public record CreateTeamResponseDto(int Id, string Name, int Position, string HomeKitColor, string Stadium, string City);
}