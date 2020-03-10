using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Threading.Tasks;
using BattleshipStateTracker.model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;

namespace BattleshipStateTracker.Controllers
{
    

    [Route("api/[controller]")]
    [ApiController]
    public class BattleshipController : ControllerBase
    {
        private IMemoryCache cache;

        public BattleshipController(IMemoryCache cache)
        {
            this.cache = cache;
        }

        [HttpPut("board")]
        public int CreateBoard([FromForm] int size)
        {
            Board board = new Board(size);
            cache.Set(board.Id, board);
            return board.Id;
        }

        [HttpGet("boards/{id}")]
        public Board GetBoard([FromRoute] int id)
        {
            return cache.Get<Board>(id);
        }

        [HttpPost("{boardId}/ship")]
        public bool PlaceShip([FromRoute] int boardId, [FromBody] Ship ship)
        {
            Board board = cache.Get<Board>(boardId);
            bool result = board.PlaceShip(ship);
            if (result)
            {
                cache.Set(boardId, board);
            }
            return result;
        }

        [HttpPost("{boardId}/fire")]
        public bool PlaceShip([FromRoute] int boardId,
            [FromBody] BoardPanel boardPanel)
        {
            Board board = cache.Get<Board>(boardId);
            bool result = board.Fire(boardPanel);
            if (result)
            {
                cache.Set(boardId, board);
            }
            return result;
        }

        [HttpGet]
        public string Get()
        {
            return "Battleship State Tracker API!";
        }

    }
}