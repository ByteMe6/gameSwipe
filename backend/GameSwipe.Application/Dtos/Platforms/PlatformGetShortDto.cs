using GameSwipe.Application.Dtos.General;
using GameSwipe.DataAccess.Entities.Other;

namespace GameSwipe.Application.Dtos.Platforms;

public class PlatformGetShortDto(long id, string name) : GetShortDto(id, name)
{
	public static explicit operator PlatformGetShortDto(Platform ent) => new(ent.Id, ent.Name);
}
