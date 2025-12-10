module Tests

open Xunit
open CinemaLogic

[<Fact>]
let ``Booking empty seat should return true`` () =
    let seats = Array2D.create 5 6 false
    let result = CinemaLogic.bookSeat seats 2 3
    Assert.True(result)

[<Fact>]
let ``Booking already taken seat should return false`` () =
    let seats = Array2D.create 5 6 false
    CinemaLogic.bookSeat seats 1 1 |> ignore
    let result = CinemaLogic.bookSeat seats 1 1
    Assert.False(result)
