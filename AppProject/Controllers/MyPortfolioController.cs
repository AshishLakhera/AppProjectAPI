using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AppProject.Repositories;
using CompsContext;
using CompsModel;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AppProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MyPortfolioController : ControllerBase
    {
        private readonly CompsEntities _compsDb;
       
        public MyPortfolioController(CompsEntities Db)
        {
            _compsDb = Db;
        }
       
        [Route("GetPortfolio"),HttpGet]
        public IActionResult GetPortfolio() {
            MyPortfolioRepositories repo = new MyPortfolioRepositories(_compsDb);
            var GetData=  repo.GetPortFolioList("sherry.ittycheria@evalueserve.com");
            return Ok(GetData);
        }
        [Route("EditPortfolio"), HttpPost]
        public IActionResult EditPortfolio(CustomPortfolioModel model)
        {
            var CustomPortfolio = _compsDb.tm_CustomPortfolio.Where(x => x.CustomId == model.CustomId).FirstOrDefault();
            CustomPortfolio.PortfolioName = model.PortfolioName;
           // CustomPortfolio.UpdatedDate = model.UpdatedDate;
            _compsDb.Update(CustomPortfolio);
            _compsDb.SaveChanges();
         //   var CustomPortfolioCompsList = _compsDb.tm_CustomPortfolioCompsList.Where(x => x.CustomId == model.CustomId).FirstOrDefault();
         //   CustomPortfolioCompsList.CustomPortfolioName = model.PortfolioName;
         ////   CustomPortfolioCompsList.UpdatedDate = model.UpdatedDate;
         //   _compsDb.Update(CustomPortfolioCompsList);
         //   _compsDb.SaveChanges();

            return Ok();
        }
        [Route("GetEmaiList"), HttpGet]
        public IActionResult GetEmaiList() {
            MyPortfolioRepositories repo = new MyPortfolioRepositories(_compsDb);
            var GetList = repo.GetUserEmail();
            return Ok(GetList);
        }
        [Route("GetRegionList"), HttpGet]
        public IActionResult GetRegionList()
        {
            MyPortfolioRepositories repo = new MyPortfolioRepositories(_compsDb);
            var GetList = repo.GetRegionList();
            return Ok(GetList);
        }
        [Route("GetLocationByRegionId"), HttpPost]
        public IActionResult GetLocationByRegionId(int regionId)
        {
            MyPortfolioRepositories repo = new MyPortfolioRepositories(_compsDb);
            var GetList = repo.GetLocationBasedOnRegion(regionId);
            return Ok(GetList);
        }
    }
}