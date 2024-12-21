
using KS.Domain.Common.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
namespace Hotlis.Infrastructure.Interceptors;
public class PublishDomainEventsInterceptor(IMediator _mediator):SaveChangesInterceptor
{
    private readonly IPublisher mediatr=_mediator;
    public override InterceptionResult<int> SavingChanges(DbContextEventData eventData, InterceptionResult<int> result)
    {
        //Audit logging
        var context=eventData.Context;
        if(context==null)
        {
            return base.SavingChanges(eventData, result);
            
        }
        
        PublishDomainEvents(eventData.Context).GetAwaiter().GetResult();
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
       
        await PublishDomainEvents(eventData.Context);


        return await base.SavingChangesAsync(eventData,result,cancellationToken);
    }

    private async Task PublishDomainEvents(DbContext? dbContext)
    {
        if(dbContext is null) return;
        //get hold of all various entitys
        var entitiesWithDomainEvents=dbContext.ChangeTracker.Entries<IHasDomainEvents>()
            .Where(entry=>entry.Entity.DomainEvents.Any())
            .Select(entry=>entry.Entity)
            .ToList();
        //get hold of various domain events
        var domainEvents=entitiesWithDomainEvents.SelectMany(entry=>entry.DomainEvents).ToList();
        

        //clear domain events
        entitiesWithDomainEvents.ForEach(entity=>entity.ClearDomainEvents());
        //publish domain events
        foreach(var domainEvent in domainEvents)
        {
            await mediatr.Publish(domainEvent);
        }      

    }

    
}