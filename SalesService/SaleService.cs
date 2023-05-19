using Microsoft.EntityFrameworkCore;
using SalesData.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalesService
{
    public class SaleService
    {
        private readonly SalesDBContext _ctx;
        public SaleService(SalesDBContext ctx)
        {
            _ctx = ctx;
        }

        //Create Data
        public async Task<SalesModel> NewTransaction(Object data)
        {
            try
            {
                SalesModel tempData = (SalesModel)data;
                tempData.TransactionDate = DateTime.Now.ToUniversalTime();

                _ctx.SalesData.Add(tempData);
                await _ctx.SaveChangesAsync();
                return tempData;
            }
            catch (Exception ex)
            {
                throw new ArgumentException("Unable to create new data, " + ex.Message);
            }
        }

        //Update Data
        public async Task UpdateTransaction(SalesModel newData, SalesModel sales)
        {
            try
            {
                bool hasChanges = _ctx.Entry(sales).Properties.Any(p => p.IsModified);
                if(hasChanges)
                {
                    if (newData.BranchId != 0)
                    {
                        sales.BranchId = newData.BranchId;
                    }

                    sales.Amount = newData.Amount;
                    sales.LoyaltyCardNumber = newData.LoyaltyCardNumber;
                    sales.TransactionDate = DateTime.Now.ToUniversalTime();

                    _ctx.SalesData.Update(sales);
                    await _ctx.SaveChangesAsync();
                }
                else
                {
                    Console.WriteLine("There is no changes in Transaction Data.");
                }
            }
            catch (Exception ex)
            {
                throw new ArgumentException("Unable to update data, " + ex.Message);
            }
        }

        //Get data by TransacID
        public async Task<SalesModel> GetDataById(string transacId)
        {
            try
            {
                SalesModel data = await _ctx.SalesData.SingleOrDefaultAsync(v => v.TransactionId == transacId);
                return data;
            }
            catch (Exception ex)
            {
                throw new ArgumentException("Unable to retrieve data, " + ex.Message);
            }
        }

        //Checking if update happen
        public async Task<bool> CheckTransactionChanges(SalesModel newData, SalesModel existingData)
        {
            // Compare the properties of newData and existingData to check for changes
            bool hasChanges = newData.BranchId != existingData.BranchId ||
            newData.TransactionId != existingData.TransactionId ||
            newData.Amount != existingData.Amount ||
            newData.LoyaltyCardNumber != existingData.LoyaltyCardNumber;
            
            return hasChanges;
        }
    }
}
