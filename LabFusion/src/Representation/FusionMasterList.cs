using LabFusion.Extensions;
using LabFusion.Network;

using System.Text.RegularExpressions;

namespace LabFusion.Representation;

public enum FusionMasterResult
{
    NORMAL = 1 << 0,
    IMPERSONATOR = 1 << 1,
    MASTER = 1 << 2,
}

public static class FusionMasterList
{
    public struct MasterPlayer
    {
        public ulong id;
        public string name;
        public bool unique;

        public MasterPlayer(ulong id, string name, bool unique = true)
        {
            this.id = id;
            this.name = name;
            this.unique = unique;
        }
    }

    private static readonly MasterPlayer[] _steamPlayers = new MasterPlayer[] {

    };

    public static FusionMasterResult VerifyPlayer(ulong id, string name)
    {
        if (NetworkLayerManager.Layer is SteamNetworkLayer)
        {
            return VerifyPlayer(_steamPlayers, id, name);
        }

        return FusionMasterResult.NORMAL;
    }

    private static FusionMasterResult VerifyPlayer(MasterPlayer[] players, ulong id, string name)
    {
        for (var i = 0; i < players.Length; i++)
        {
            var player = players[i];

            // Our id matches, and this is a master user
            if (player.id == id)
            {
                return FusionMasterResult.MASTER;
            }

            // Convert names to have no whitespace and in lowercase
            string masterName = Regex.Replace(player.name, @"\s+", "").ToLower().RemoveRichText();

            string otherName = Regex.Replace(name, @"\s+", "").ToLower().RemoveRichText();

            // The name matches, but the id didn't
            if (otherName.Contains(masterName) && player.unique)
            {
                return FusionMasterResult.IMPERSONATOR;
            }
        }

        // Neither name nor id matched, this is a regular joe
        return FusionMasterResult.NORMAL;
    }
}
