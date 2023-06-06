using QuakeReport.Domain.Models;
using System.Text.Json;
using System.Text;

namespace QuakeReport.App.Parser
{
    public static class MatchKillJson
    {
        public static string Write(Tournament tournament)
        {
            using(MemoryStream ms = new MemoryStream())
            {
                using(Utf8JsonWriter writer = new Utf8JsonWriter(ms, new JsonWriterOptions(){ Indented = true }))
                {
                    writer.WriteStartObject();

                    foreach(Match m in tournament.Matches.Values)
                    {
                        writer.WriteStartObject(String.Concat("game_", m.Number));
                        writer.WriteNumber("total_kills", m.Kills.Count);
                        
                        writer.WriteStartArray("players");
                        foreach(Player p in m.Players.Values)
                        {
                            writer.WriteStringValue(p.NickName);
                        }
                        writer.WriteEndArray();
                        
                        writer.WriteStartObject("kills");
                        foreach(KeyValuePair<int,int> kv in m.PlayerKiller)
                        {
                            writer.WriteNumber(m.Players[kv.Key].NickName, kv.Value);
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