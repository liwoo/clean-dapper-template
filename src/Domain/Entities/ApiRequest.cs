using System;
using System.Linq;
using System.Collections.Generic;
using Newtonsoft.Json;
using Domain.ValueObjects;
using Domain.Common.Interfaces;

namespace Domain.Entities
{
    public class ApiRequest : CouchbaseEntity<ApiRequest>, IAuditableEntity
    {
        public static readonly string ENTITY_NAME = "api-requests";

        public string OrganizationUnits { get; set; }
        public string Verb { get; set; }
        public string Headers { get; set; }
        public string Endpoint { get; set; }
        public string AccessToken { get; set; }
        public string ClientEmail { get; set; }
        public string RequestData { get; set; }

        [JsonProperty]
        public IList<DocumentState> DocumentStates { get; private set; } = null;

        public IDictionary<string, IList<string>> InvalidDocumentFields { get; set; } = null;
        public int? ResponseStatusCode { get; set; }
        public string ResponseData { get; set; }
        public bool? Acknowledged { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public DateTime? ClientUpdatedAt { get; set; }
        public string ClientMetadata { get; set; }


        [JsonIgnore]
        public bool IsProcessable => LastState != DocumentStatuses.PROCESSED;

        [JsonIgnore]
        public bool IsProcessing => LastState == DocumentStatuses.PROCESSING;

        public void AddState(DocumentStatuses status)
        {
            DocumentStates ??= new List<DocumentState>();
            DocumentStates.Add(new DocumentState(status));
        }

        [JsonIgnore]
        public DocumentStatuses? LastState
        {
            get
            {
                if (DocumentStates == null || !DocumentStates.Any())
                {
                    return null;
                }

                return DocumentStates.Last().Status;
            }
        }

        public bool CanBeProcessed()
        {
            return !IsProcessing && IsProcessable;
        }
    }
}
