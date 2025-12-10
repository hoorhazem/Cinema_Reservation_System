module CinemaLogic

open System
open System.IO

let bookSeat (seats: bool[,]) row col =
    if seats.[row, col] then
        false
    else
        seats.[row, col] <- true
        true

let generateTicketId () = Guid.NewGuid().ToString()

let saveTicket row col ticketFile =
    let ticketId = generateTicketId()
    let ticketInfo = sprintf "Ticket ID: %s | Row: %d, Column: %d" ticketId row col
    File.AppendAllText(ticketFile, ticketInfo + Environment.NewLine)
    ticketInfo
