using GameSwipe.Application.Dtos.General;

namespace GameSwipe.Application.Dtos.Platforms;

public class PlatformGetShortDto(long id, string name) : GetShortDto(id, name)
{
}
