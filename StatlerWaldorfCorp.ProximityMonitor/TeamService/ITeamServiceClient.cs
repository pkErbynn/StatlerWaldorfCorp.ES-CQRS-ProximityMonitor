using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StatlerWaldorfCorp.ES_CQRS_ProximityMonitor.TeamService
{
    public interface ITeamServiceClient
    {
        Team GetTeam(Guid teamId);
        Member GetMember(Guid teamId, Guid memberId);
    }
}