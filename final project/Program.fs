open System
open System.Drawing
open System.Windows.Forms
open System.IO
open CinemaLogic  

let rows = 5
let cols = 6
let seats = Array2D.create rows cols false
let ticketFile = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "tickets.txt")

let form = new Form(Text = "Cinema Seat Reservation", Size = Size(600, 450))

let seatButtons = Array2D.init rows cols (fun r c ->
    let btn = new Button(Text = "", Size = Size(50, 50))
    btn.Location <- Point(c * 55 + 20, r * 55 + 20)
    btn.BackColor <- Color.LightGreen
    btn.Click.Add(fun _ ->
        if seats.[r,c] then
            MessageBox.Show("Seat already booked!") |> ignore
        else
            CinemaLogic.bookSeat seats r c |> ignore
            btn.BackColor <- Color.Red
            let ticketInfo = CinemaLogic.saveTicket r c ticketFile
            MessageBox.Show(sprintf "Seat booked!\n%s" ticketInfo) |> ignore
    )
    btn
)

for r in 0 .. rows - 1 do
    for c in 0 .. cols - 1 do
        form.Controls.Add(seatButtons.[r,c])

let resetBtn = new Button(Text = "Reset All", Size = Size(100, 40))
resetBtn.Location <- Point(20, rows * 55 + 30)
resetBtn.BackColor <- Color.LightBlue
resetBtn.Click.Add(fun _ ->
    for r in 0 .. rows - 1 do
        for c in 0 .. cols - 1 do
            seats.[r,c] <- false
            seatButtons.[r,c].BackColor <- Color.LightGreen
    File.WriteAllText(ticketFile, "")
    MessageBox.Show("All seats reset!") |> ignore
)
form.Controls.Add(resetBtn)

[<STAThread>]
Application.EnableVisualStyles()
Application.Run(form)
