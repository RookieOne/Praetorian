module Praetorian
{
    type Event
    {
        Id : Guid;
        Event : Text;
        EventDate : DateTime;
    } where identity Id;
    
    EventsTable : Event*;        
    
    type AggregateEvent
    {
        Id : Guid;
        EventId : Guid;
        AggregateId : Guid;
    } where identity Id;
    
    AggregateEventsTable : AggregateEvent*;
    
    type Message
    {
        Id : Guid;
        DateRecieved : DateTime;
        DateSent : DateTime;
        Message : Text;
    } where identity Id;
    
    MessagesTable : Message*;
    
    type Expense
    {
        Id : Guid;
        Title : Text;
        Author : Text;
        ApprovalDate : DateTime;
    } where identity Id;
    
    ExpensesTable : Expense*;
}