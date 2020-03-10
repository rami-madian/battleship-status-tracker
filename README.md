# battleship-status-tracker

# Running the BattleshipStateTracker API [Tested using VS on Mac]
- Open the **BattleshipStateTracker** solution file using Visual Studio
- Rebuild the **BattleshipStateTracker** project
- A browser window will be launced by VS displaying the string "Battleship State Tracker API!"

## API
### **POST** /api/battleship/board/{size}
- Request parameter: size (an integer > 0)
- Response: The newley created board ID

### **POST** /api/battleship/{board_id}/ship
- Request parameter: board ID and the ship attributes in JSON (Layout = 0 means horizontal and 1 means vertical)
```
{
    "Layout": 0,
    "ShipLength": 2,
    "ShipSymbol": "Ship1"
}
```

- Response: true for successful placement on the board and false otherwise.

### **POST** /api/battleship/{board_id}/fire
- Request parameter: board ID and the coordinates of the target board panel in JSON
```
{
    "X": 1,
    "Y": 1
}
```

- Response: true for a hit and false otherwise.



