using Microsoft.AspNetCore.Mvc;
using System.Reflection.Metadata.Ecma335;
using WebApi_LS1.Dtos;
using WebApi_LS1.Entities;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebApi_LS1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PlayerController : ControllerBase
    {



        public static List<Player> Players { get; set; } = new List<Player>
        {
            new Player
            {
                Id = 1,
                City="NewYork",
                PlayerName="Jack",
                Score=99
            },
             new Player
            {
                Id = 2,
                City="Paris",
                PlayerName="Jeanne",
                Score=96
            }, new Player
            {
                Id = 3,
                City="Madrid",
                PlayerName="Antonio",
                Score=72
            },
        };


        // GET: api/<PlayerController>
        //[HttpGet]
        //public IEnumerable<PlayerDto> Get()
        //{
        //    var result = Players.Select(p => new PlayerDto
        //    { 
        //        PlayerName = p.PlayerName, 
        //        City = p.City, 
        //        Score = p.Score });
        //    return result;
        //}

        [HttpGet("BestPlayers")]
        public IEnumerable<PlayerDto> GetBestPlayers()
        {
            var result = Players.Where(p => p.Score >= 80).Select(p => new PlayerDto
            {
                PlayerName = p.PlayerName,
                City = p.City,
                Score = p.Score
            });
            return result;
        }


        [HttpGet]
        public IEnumerable<PlayerExtendedDto> Search(string key = "")
        {
            var result = Players.Where(p => p.PlayerName.ToLower().Contains(key.ToLower())).Select(p => new PlayerExtendedDto
            {
                Id = p.Id,
                PlayerName = p.PlayerName,
                City = p.City,
                Score = p.Score
            });
            return result;
        }
        // GET api/<PlayerController>/5
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            try
            {
                var player = Players.FirstOrDefault(p => p.Id == id);
                if (player != null)
                {
                    var dto = new PlayerDto
                    {
                        City = player.City,
                        PlayerName = player.PlayerName,
                        Score = player.Score
                    };
                    return Ok(dto);
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            //return NotFound("Player tapilmadi !");
            return NotFound();
        }

        [HttpPost]
        // POST api/<PlayerController>
        public IActionResult Post([FromBody] PlayerDto dto)
        {
            if (dto.Score > 0)
            {
                var player = new Player
                {
                    City = dto.City,
                    PlayerName = dto.PlayerName,
                    Score = dto.Score,
                    Id = (new Random()).Next(10, 1000)
                };
                Players.Add(player);
                return Ok(player);
            }
            return BadRequest("Score is not valid !");
        }


        // PUT api/<PlayerController>/5
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] PlayerDto dto)
        {
            var player = Players.FirstOrDefault(p => p.Id == id);
            if (player != null)
            {
                player.PlayerName = dto.PlayerName;
                player.Score = dto.Score;
                player.City = dto.City;
                return Ok(player);
            }
            return NotFound();
        }

        // DELETE api/<PlayerController>/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var player = Players.FirstOrDefault(p => p.Id == id);
            if (player == null) return NotFound();
            Players.Remove(player);
            return NoContent();
        }
    }
}
