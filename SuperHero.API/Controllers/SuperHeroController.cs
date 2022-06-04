using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SuperHero.API.Models;

namespace SuperHero.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SuperHeroController : ControllerBase
    {

       

        private readonly DataContext _dataContext;

        public SuperHeroController(DataContext dataContext)
        {
            

            _dataContext = dataContext;

        }

        [HttpGet("{id}")]
        public async Task<ActionResult<SuperHeroModel>> Get([FromRoute]int id)
        {


            #region In-Memory
            // var data=model.Where(model=>model.Id == id).FirstOrDefault(); 
            #endregion

            var data =await _dataContext.tbl_superhero.FindAsync(id);

            return data switch
            {
                null => NotFound(new { data="id not exist", code = "99", description = "fail" }),
              
                _ => Ok(new { data, code = "00", description = "succes" })
            };

            
            
        }
        [HttpGet]
        public async Task<ActionResult<List<SuperHeroModel>>> GetAllHero()
        {


            #region In-Memory
            //var data = model.OrderBy(x=>x.Name)
            //                .ThenBy(x => x.FirstName)
            //                .ThenBy(x => x.LastName)
            //                .ToList(); 
            #endregion

            var data = await _dataContext.tbl_superhero
                            .OrderBy(x => x.Name)
                            .ThenBy(x => x.FirstName)
                            .ThenBy(x => x.LastName)
                            .Select(x => new 
                            { 
                               fullname=x.Name + " " + x.FirstName +" "+x.LastName,
                               x.Place
                            })
                            .ToListAsync();  //if you are using TolistAsync() will not use ASEnumberable()
            return data switch
            {
                null => NotFound(new { data="record null", code = "99", description = "fail" }),

                _ => Ok(new { data, code = "00", description = "succes" })
            };



        }
        [HttpPost]
        public async Task<ActionResult<List<SuperHeroModel>>> POST([FromBody]SuperHeroModel superHeroModel)
        {


            #region In-Memory
            //model.Add(superHeroModel); 
            #endregion
            _dataContext.tbl_superhero.Add(superHeroModel);
            await _dataContext.SaveChangesAsync();
            return NoContent();

        }
        [HttpPut]
        public async Task<ActionResult<List<SuperHeroModel>>> PuT([FromBody] SuperHeroModel superHeroModel)
        {
            //var m = model.Find(model => model.Id == superHeroModel.Id);

            var m = _dataContext.tbl_superhero.Find(superHeroModel.Id);

            if (m == null)return NotFound(new { data = "user doesnt exist", code = "99", description = "fail" });

            m.Name= superHeroModel.Name;
            m.FirstName = superHeroModel.FirstName;
            m.LastName = superHeroModel.LastName;
            m.Place = superHeroModel.Place;

            await _dataContext.SaveChangesAsync();

            return NoContent();

        }

        [HttpDelete]
        public async Task<ActionResult<List<SuperHeroModel>>> Delete([FromQuery]int id)
        {
            #region In-Memory
            //var m = model.Find(model => model.Id == id); 
            #endregion


            var m = _dataContext.tbl_superhero.Find(id);


            if (m == null) return NotFound(new { data = "user doesnt exist", code = "99", description = "fail" });

            _dataContext.tbl_superhero.Remove(m);
            await _dataContext.SaveChangesAsync();
            return NoContent();

        }
    }
}
