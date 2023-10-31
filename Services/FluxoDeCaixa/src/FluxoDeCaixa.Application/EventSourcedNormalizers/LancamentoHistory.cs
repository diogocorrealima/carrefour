using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using FluxoDeCaixa.Domain.Core.Events;

namespace FluxoDeCaixa.Application.EventSourcedNormalizers
{
    public static class LancamentoHistory
    {
        public static IList<LancamentoHistoryData> HistoryData { get; set; }

        public static IList<LancamentoHistoryData> ToJavaScriptLancamentoHistory(IList<StoredEvent> storedEvents)
        {
            HistoryData = new List<LancamentoHistoryData>();
            LancamentoHistoryDeserializer(storedEvents);

            var sorted = HistoryData.OrderBy(c => c.Timestamp);
            var list = new List<LancamentoHistoryData>();
            var last = new LancamentoHistoryData();

            foreach (var change in sorted)
            {
                var jsSlot = new LancamentoHistoryData
                {
                    Id = change.Id == Guid.Empty.ToString() || change.Id == last.Id
                        ? ""
                        : change.Id,
                    IdUsuario = string.IsNullOrWhiteSpace(change.IdUsuario) || change.IdUsuario == last.IdUsuario
                        ? ""
                        : change.IdUsuario,
                    Valor = change.Valor == last.Valor
                        ? 0
                        : change.Valor,
                    Action = change.Action,
                    Timestamp = change.Timestamp,
                    Usuario = change.Usuario
                };

                list.Add(jsSlot);
                last = change;
            }
            return list;
        }

        private static void LancamentoHistoryDeserializer(IEnumerable<StoredEvent> storedEvents)
        {
            foreach (var e in storedEvents)
            {
                var historyData = JsonSerializer.Deserialize<LancamentoHistoryData>(e.Data);
                historyData.Timestamp = DateTime.Parse(historyData.Timestamp).ToString("yyyy'-'MM'-'dd' - 'HH':'mm':'ss");

                switch (e.MessageType)
                {
                    case "LancamentoRegisteredEvent":
                        historyData.Usuario = e.User;
                        break;
                    case "LancamentoUpdatedEvent":
                        historyData.Action = "Updated";
                        historyData.Usuario = e.User;
                        break;
                    case "LancamentoRemovedEvent":
                        historyData.Action = "Removed";
                        historyData.Usuario = e.User;
                        break;
                    default:
                        historyData.Action = "Unrecognized";
                        historyData.Usuario = e.User ?? "Anonymous";
                        break;

                }
                HistoryData.Add(historyData);
            }
        }
    }
}