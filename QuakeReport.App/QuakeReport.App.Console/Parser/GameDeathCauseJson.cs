using QuakeReport.Domain.Models;
using System.Text.Json;
using System.Text;
using QuakeReport.Domain.Models.Enums;

namespace QuakeReport.App.Parser
{
     public static class GameDeathCauseJson
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
                         writer.WriteStartObject(String.Concat("game-", game.Number));
                         writer.WriteStartObject("kills_by_means");

                         foreach(KeyValuePair<DeathCause, int> kv in  game.DeathCauseScore)
                         {
                              writer.WriteNumber(kv.Key.ToString(), kv.Value);
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