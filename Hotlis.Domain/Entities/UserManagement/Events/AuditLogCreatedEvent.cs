using KS.Domain.Entities.UserManagement;
using MediatR;

namespace Hotlis.Domain.Entities.UserManagement.Events;
public class AuditLogCreatedEvent(AuditLog auditLog):INotification
{
    public AuditLog AuditLog { get; } = auditLog;
}