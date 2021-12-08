using Newtonsoft.Json.Linq;
using Paypal.API.CustomException;
using Paypal.API.Dto;
using Paypal.API.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Paypal.API.DataAdapter
{
    public class OrderDataAdapter : IDataAdapter
    {
        public JObject RepackOrderToJObject(CreateOrderDto dto)
        {
            if (dto.Items.Count == 0)
                throw new InvalidOrderDataException("Cannot place order with zero items.");
            
            Dictionary<string, List<OrderItemDto>> merchantPurchaseUnitDict = new Dictionary<string, List<OrderItemDto>>();
            JArray purchaseUnits = new JArray();
            string currencyCode = dto.Items[0].Currency;

            //Map merchant to ordered items
            foreach (OrderItemDto item in dto.Items)
            {
                if (item.Currency != currencyCode)
                {
                    throw new InvalidOrderDataException("Currency codes in purchase order must match.");
                }

                if (!merchantPurchaseUnitDict.ContainsKey(item.MerchantID))
                {
                    merchantPurchaseUnitDict.Add(item.MerchantID, new List<OrderItemDto>());
                }
                merchantPurchaseUnitDict[item.MerchantID].Add(item);
            }

            //Create purchase units
            foreach(KeyValuePair<string, List<OrderItemDto> > kvp in merchantPurchaseUnitDict)
            {
                double purchaseUnitTotal = 0;
                JArray itemsArray = new JArray();
                foreach(OrderItemDto item in kvp.Value)
                {
                    purchaseUnitTotal += item.Value * item.Quantity;
                    itemsArray.Add(JObject.FromObject( 
                        new
                        {
                            name = item.Name,
                            description = item.Description,
                            unit_amount = new
                            {
                                currency_code = item.Currency,
                                value = item.Value,
                                
                            },
                            
                            quantity = item.Quantity
                        }));

                }

                purchaseUnits.Add(JObject.FromObject(
                new
                {
                    reference_id = kvp.Key,
                    amount = new
                    {
                        value = purchaseUnitTotal,
                        currency_code = currencyCode,
                        breakdown = new
                        {
                            item_total = new
                            {
                                currency_code = currencyCode,
                                value = purchaseUnitTotal,
                            }
                        }

                    },

                    payee = new 
                    {
                        merchant_id = kvp.Key
                    },

                    items = itemsArray
                }));
            }

            //Create order form purchase units
            JObject order = JObject.FromObject(
            new
            {
                purchase_units = purchaseUnits,
                intent = "CAPTURE"
            });
            return order;
        }
    }
}
