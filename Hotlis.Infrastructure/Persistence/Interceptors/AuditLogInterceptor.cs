using System.Text.Json;
using Hotlis.Application.Common.Interfaces;
using Hotlis.Domain.Entities.UserManagement.Events;
using Hotlis.Infrastructure.Persistence;
using KS.Domain.Entities.UserManagement;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Diagnostics;


namespace Hotlis.Infrastructure.Interceptors;
public class AuditLogInterceptor(IUserContextService _userContextService,IDbContextFactory<AppDbContext> _dbContextFactory):SaveChangesInterceptor
{
    private readonly IUserContextService userContextService=_userContextService;
    private readonly IDbContextFactory<AppDbContext> dbContextFactory=_dbContextFactory;
    
    public override InterceptionResult<int> SavingChanges(DbContextEventData eventData, InterceptionResult<int> result)
    {
        //Audit logging
        var context=eventData.Context;
        if(context==null)
        {
            return base.SavingChanges(eventData, result);
            
        }
        var auditEntries=context.ChangeTracker.Entries()
        .Where(e => e.State == EntityState.Added || e.State == EntityState.Modified || e.State == EntityState.Deleted)
            .Select(e => CreateAuditLog(e))
            .ToList();
        SaveAuditLogs(auditEntries);
        
        return base.SavingChanges(eventData,result);
    }
    public async override ValueTask<InterceptionResult<int>> SavingChangesAsync(DbContextEventData eventData, InterceptionResult<int> result, CancellationToken cancellationToken = default(CancellationToken))
    {
        //Audit logging
        var context=eventData.Context;
        if(context==null)
        {
            return await base.SavingChangesAsync(eventData, result, cancellationToken);
            
        }
        // Add audit log entries
       var auditEntries=context.ChangeTracker.Entries()
        .Where(e => e.State == EntityState.Added || e.State == EntityState.Modified || e.State == EntityState.Deleted)
            .Select(e => CreateAuditLog(e))
            .ToList();
        await SaveAuditLogsAsync(auditEntries, cancellationToken);
        return await base.SavingChangesAsync(eventData,result,cancellationToken);
    }


    private AuditLog CreateAuditLog(EntityEntry entry)
    {
        
        var auditLog=new AuditLog{
            Action=entry.State.ToString(),
            TableName=entry.Entity.GetType().Name,
            UserId=userContextService.UserId,
            UserName=userContextService.UserName,
            KeyValues=entry.Properties.FirstOrDefault(p=>p.Metadata.IsPrimaryKey())?.CurrentValue?.ToString()??string.Empty,
           
        };
        switch(entry.State)
        {
            case EntityState.Added:
                auditLog.OriginalValues=JsonSerializer.Serialize(entry.CurrentValues.ToObject());
                break;
            case EntityState.Modified:
                auditLog.NewValues=JsonSerializer.Serialize(entry.CurrentValues.ToObject());
                auditLog.OriginalValues=JsonSerializer.Serialize(entry.OriginalValues.ToObject());
                break;
            case EntityState.Deleted:
                auditLog.OriginalValues=JsonSerializer.Serialize(entry.OriginalValues.ToObject());
                break;
        }
        
        return auditLog;
    }

    private void SaveAuditLogs(List<AuditLog> auditLogs) 
    { 
        using var context = dbContextFactory.CreateDbContext(); 
        context.AuditLog.AddRange(auditLogs); context.SaveChanges();
     }
    private async Task SaveAuditLogsAsync(List<AuditLog> auditLogs, CancellationToken cancellationToken) 
    { 
        await using var context = dbContextFactory.CreateDbContext(); 
        context.AuditLog.AddRange(auditLogs); 
        await context.SaveChangesAsync(cancellationToken); 
    }
}