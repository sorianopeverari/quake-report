using QuakeReport.Domain.Models;
using System.Text.Json;
using System.Text;
using QuakeReport.Domain.Models.Enums;

namespace QuakeReport.App.Parser
{
    public static class MatchDeathCauseJson
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
                        writer.WriteStartObject(String.Concat("game-", m.Number));
                        writer.WriteStartObject("kills_by_means");

                        foreach(KeyValuePair<int, int> kv in  m.DeathCause)
                        {
                            DeathCause deathCause = (DeathCause)kv.Key;
                            writer.WriteNumber(deathCause.ToString(), kv.Value);
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