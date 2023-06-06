using QuakeReport.Domain.Models;
using System.Text.Json;
using System.Text;

namespace QuakeReport.App.Parser
{
    public static class GameKillByPlayerJson
    {
        public static string Write(Tournament tournament)
        {
            using(MemoryStream ms = new MemoryStream())
            {
                using(Utf8JsonWriter writer = new Utf8JsonWriter(ms, new JsonWriterOptions(){ Indented = true }))
                {
                    writer.WriteStartObject();

                    foreach(Game game in tournament.Games)
                    {
                        writer.WriteStartObject(String.Concat("game_", game.Number));
                        writer.WriteNumber("total_kills", game.KillScore);
                        
                        writer.WriteStartArray("players");
                        foreach(Player player in game.PlayersById.Values)
                        {
                            if(!player.Hidden)
                            {
                                writer.WriteStringValue(player.NickName);
                            }
                        }
                        writer.WriteEndArray();
                        
                        writer.WriteStartObject("kills");
                        foreach(Player player in game.PlayersById.Values)
                        {
                            if(!player.Hidden)
                            {
                                writer.WriteNumber(player.NickName, player.KillScore);
                            }
                        }
                        writer.WriteEndObject();
                        writer.WriteEndObject();
                    }
                    writer.WriteEndObject();
                    writer.Flush();
                    return Encoding.UTF8.GetString(ms.ToArray());
                }
            }
        }
    }
}