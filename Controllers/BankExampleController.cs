using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Primitives;
using NetCoreProyectExample.Models;
using System.ComponentModel.DataAnnotations;
using System.Net;

namespace NetCoreProyectExample.Controllers
{

    public class BankExampleController : Controller
    {

        #region HomeWork (6) Controllers

        public AccountBankModel Account = new AccountBankModel()
        {
            AccountNumber = 1001,
            AccountHolderName = "Example Name",
            Balance = 5000
        };
        [Route("/Bank")]
        //welcome

        public IActionResult Index()
        {
            return Ok("Welcome to the best bank");
        }

        [HttpGet]
        [Route("AccountStatement")]
        public IActionResult GetStatement()
        {
            return File("~/Cv.pdf", "application/pdf");

        }

        [HttpGet]
        [Route("Account-Details")]
        //json
        public async Task<IActionResult> GetAccount()
        {
            StreamReader reader = new StreamReader(Request.Body);
            var bodyContent = await reader.ReadToEndAsync();
            Dictionary<string, StringValues> dictBody = QueryHelpers.ParseQuery(bodyContent);
            if (!dictBody.ContainsKey("account"))
            {
                return BadRequest();
            }
            string? account = dictBody["account"][0];


            if (account != "1001" || account != "1002")

            {
                return BadRequest("invalid Account number");
            }
            return Json(Account);
        }



        [HttpDelete]
        [Route("Delete{account:int}")]
        /*
        public IActionResult Delete()
        {
            int accountNumber = Convert.ToInt32(Request.Query.ContainsKey("account").ToString());
            if (accountNumber != 1001)
            {
                return BadRequest("invalid Account number");
            }

            //delete...


        }
        */

        [HttpPost]
        [Route("Create")]
        //recibe en body
        public async Task<IActionResult> AddAccount()
        {
            StreamReader reader = new StreamReader(Request.Body);
            var bodyContent = await reader.ReadToEndAsync();
            Dictionary<string, StringValues> dictBody = QueryHelpers.ParseQuery(bodyContent);
            if (!dictBody.ContainsKey("AccountNumber"))
            {
                return BadRequest("Account number not supplied");
            }
            if (!dictBody.ContainsKey("Balance"))
            {
                return BadRequest("Balance not supplied");

            }
            if (!dictBody.ContainsKey("HolderName"))
            {
                return BadRequest("holder name not supplied");
            }
            AccountBankModel newAccount = new AccountBankModel();
            newAccount.AccountHolderName = dictBody["HolderName"][0];
            newAccount.AccountNumber = Convert.ToInt32(dictBody["AccountNumber"][0]);
            newAccount.Balance = Convert.ToDecimal(dictBody["Balance"][0]);

            return CreatedAtAction("GetAccount", new { account = newAccount.AccountNumber }, newAccount);

        }

      
    [HttpPut]
    [Route("AddBalance")]
    public async Task<IActionResult> AddBalance()
    {

        StreamReader reader = new StreamReader(Request.Body);
        var bodycontent = await reader.ReadToEndAsync();
        Dictionary<string, StringValues> dictQuery = QueryHelpers.ParseQuery(bodycontent);
        if (!dictQuery.ContainsKey("Mount"))
        {
            return BadRequest();
        }
        Account.Balance += Convert.ToDecimal(dictQuery["Mount"][0]);

        return CreatedAtAction("GetAccount", new { account = Account.AccountNumber }, Account);
    }

    [HttpPut]
    [Route("SubtractBalance")]

    public async Task<IActionResult> LessBalance()
    {

        StreamReader reader = new StreamReader(Request.Body);
        var bodycontent = await reader.ReadToEndAsync();
        Dictionary<string, StringValues> dictQuery = QueryHelpers.ParseQuery(bodycontent);
        if (!dictQuery.ContainsKey("Mount"))
        {
            return BadRequest();
        }
        Account.Balance -= Convert.ToDecimal(dictQuery["Mount"][0]);

        return CreatedAtAction("GetAccount", new { account = Account.AccountNumber }, Account);
    }





    #endregion
}
}
