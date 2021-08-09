using System;

namespace Domain.ValueObjects
{
    public enum DocumentStatuses
    {
        PROCESSING = 1,
        VALID,
        INVALID,
        PROCESSED
    }

    public sealed class DocumentState
    {
        public DocumentState(DocumentStatuses status)
        {
            Time = DateTime.UtcNow;
            Status = status;
        }

        public DateTime Time { get; }
        public DocumentStatuses Status { get; }
    }
}
