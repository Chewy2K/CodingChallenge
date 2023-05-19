using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Attributes;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Enums;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using SalesAzure.DTO;
using SalesAzure.Validator;
using SalesData.Models;
using SalesService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace SalesAzure.Function
{
    public class SalesHTTPTrigger
    {
        private readonly ILogger<SalesHTTPTrigger> _logger;
        private readonly SaleService _saleService;
        private readonly IMapper _mapper;

        public SalesHTTPTrigger(ILogger<SalesHTTPTrigger> log, SaleService saleService, IMapper mapper)
        {
            _logger = log;
            _saleService = saleService;
            _mapper = mapper;
        }

        [FunctionName("SalesData")]
        [OpenApiOperation(operationId: "create/updateSalesData", tags: new[] { "Sales" })]
        [OpenApiSecurity("function_key", SecuritySchemeType.ApiKey, Name = "code", In = OpenApiSecurityLocationType.Query)]
        [OpenApiRequestBody(contentType: "application/json", bodyType: typeof(SalesDTO), Description = "Parameters", Required = true)]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "application/json", bodyType: typeof(SalesDTO), Description = "The OK response")]
        public async Task<IActionResult> EditSalesData(
           [HttpTrigger(AuthorizationLevel.Function, "post", Route = "sales-data")] SalesDTO editData)
        {
            _logger.LogInformation("Function requested to update Activity data:" + editData.ToString());


            try
            {
                var dataSelected = await _saleService.GetDataById(editData.TransactionId);

                if (dataSelected == null)
                {
                    var validator = new SalesValidator();
                    var validatorResult = await validator.ValidateAsync(editData);
                    if (!validatorResult.IsValid)
                    {
                        return new BadRequestObjectResult("Error \n" + validatorResult);
                    }
                    else
                    {
                        var newData = await _saleService.NewTransaction(_mapper.Map<SalesModel>(editData));
                        return new OkObjectResult("New transaction data is successfully created.");
                    }
                }
                else
                {
                    var salesData = await _saleService.GetDataById((string)dataSelected.TransactionId);
                    var data = _mapper.Map<SalesModel>(editData);

                    // Check if there are changes in the transaction data
                    bool hasChanges = await _saleService.CheckTransactionChanges(data, salesData);
                    if (hasChanges)
                    {
                        await _saleService.UpdateTransaction(data, salesData);
                        return new OkObjectResult("Transaction data is successfully updated.");
                    }
                    else
                    {
                        return new OkObjectResult("There is no changes in Transaction Data.");
                    }
                }
            }
            catch (System.Exception ex)
            {
                _logger.LogError(ex, "Unable to update data"); 
                return new BadRequestObjectResult("Unable to create or access data. " + ex.Message);
            }
        }
    }
}
