public async Task<MessageHelper> EditSalesOrder(CreateSalesOrderCommonDTO sme, bool? isReturn, string? previousCode)
{
    using (var transaction = await _contextW.Database.BeginTransactionAsync())
    {
        try
        {
            var msg = new MessageHelper();
            var getSalesOrder = await Task.FromResult(_contextW.SalesOrders
                            .Where(a => a.SalesOrderId == sme.SOHead.SalesOrderId && a.IsActive == true)
                            .AsNoTracking().FirstOrDefault());
            if (getSalesOrder == null)
            {
                msg.statuscode = 201;
                msg.Message = "Invoice not found!";
                return msg;
            }


            var getSalesOrderList = await Task.FromResult(_contextW.SalesOrderLines
                                              .Where(a => a.SalesOrderId == getSalesOrder.SalesOrderId).ToList());

            #region NewSalesOrderHeaderRow
            var orderHead = new Models.Write.SalesOrder()
            {
                SalesOrderCode = "EXC-" + getSalesOrder.SalesOrderCode,
                AccountId = getSalesOrder.AccountId,
                AccountName = getSalesOrder.AccountName,
                BranchId = getSalesOrder.BranchId,
                BranchName = getSalesOrder.BranchName,
                CustomerId = getSalesOrder.CustomerId,
                CustomerName = getSalesOrder.CustomerName,
                ChallanNo = getSalesOrder.ChallanNo,
                OrderDate = getSalesOrder.OrderDate,
                DeliveryDate = SmeTimeZone.CurrentDateTime(),

                Remarks = "",//PaymentTypeNarration + " Sales to " + sme.SOHead.CustomerName + ",Sales Items: " + narration,
                PaymentTypeId = 101,//PaymentTypeId,
                PaymentTypeName = "",//PaymentTypeName,

                TotalQuantity = sme.SOHead.TotalQuantity,
                ItemTotalAmount = sme.SOHead.ItemTotalAmount,
                NetDiscount = sme.SOHead.NetDiscount,
                OthersCost = sme.SOHead.OthersCost,
                NetAmount = sme.SOHead.NetAmount,
                TotalLineDiscount = sme.SOHead.TotalLineDiscount,
                HeaderDiscount = sme.SOHead.HeaderDiscount,
                HeaderDiscountPercentage = sme.SOHead.HeaderDiscountPercentage,
                ReceiveAmount = sme.SOHead.ReceiveAmount,
                ReturnAmount = 0,
                PendingAmount = sme.SOHead.PendingAmount,

                InterestRate = sme.SOHead.InterestRate,
                NetAmountWithInterest = sme.SOHead.NetAmountWithInterest,
                TotalNoOfInstallment = sme.SOHead.TotalNoOfInstallment,
                InstallmentStartDate = sme.SOHead.InstallmentStartDate,
                InstallmentType = sme.SOHead.InstallmentType,
                AmountPerInstallment = sme.SOHead.AmountPerInstallment,
                SalesForceId = sme.SOHead.SalesForceId,
                SalesForceName = sme.SOHead.SalesForceName,
                ActionById = sme.SOHead.ActionById,
                ActionByName = sme.SOHead.ActionByName,
                ActionTime = SmeTimeZone.CurrentDateTime(),
                SalesOrReturn = "Sales",
                IsActive = true,
                CustomerOrderId = sme.SOHead.CustomerOrderId,
                AdvanceBalanceAdjust = sme.SOHead.AdvanceBalanceAdjust,
                CustomerNetAmount = sme.SOHead.CustomerNetAmount,
                IsPosSales = getSalesOrder.IsPosSales,
                SalesTypeId = 1,
                SalesTypeName = "Sales",
                IsComplete = sme.SOHead.PendingAmount == 0 ? true : false,
                SalesOrderRefId = getSalesOrder.SalesOrderId
            };
            await _contextW.SalesOrders.AddAsync(orderHead);
            await _contextW.SaveChangesAsync();
            ///ROW
            var orderLine = new List<SalesOrderLine>();
            foreach (var itm in sme.SOLine)
            {
                var itemL = await Task.FromResult(_contextW.Items.FirstOrDefault(x => x.ItemId == itm.ItemId && x.IsActive == true));
                var PerUnitItemCost = (itemL.TotalValue / itemL.TotalQuantity);

                var ord = new Models.Write.SalesOrderLine()
                {
                    SalesOrderId = orderHead.SalesOrderId,
                    ItemId = itm.ItemId,
                    ItemName = itm.ItemName,
                    UomId = itm.UomId,
                    UomName = itm.UomName,
                    Quantity = itm.Quantity,
                    Price = itm.Price,
                    VatPercentage = (from x in _contextR.Items where x.ItemId == itm.ItemId && x.IsActive == true select x.VatPercentage).FirstOrDefault(),
                    TotalAmount = (decimal)(sme.SOHead.IsPosSales == true ? ((((from x in _contextR.Items
                                                                                where x.ItemId == itm.ItemId && x.BranchId == sme.SOHead.BranchId && x.IsActive == true
                                                                                select x.VatPercentage).FirstOrDefault()) * itm.TotalAmount) / 100) + itm.TotalAmount : itm.TotalAmount),
                    LineDiscount = itm.LineDiscount,
                    NetAmount = itm.NetAmount,
                    WarrantyDescription = itm.WarrantyDescription,
                    CostTotal = (itm.Quantity * PerUnitItemCost),
                    CostPrice = PerUnitItemCost
                };
                orderLine.Add(ord);
            }
            await _contextW.SalesOrderLines.AddRangeAsync(orderLine);
            await _contextW.SaveChangesAsync();
            #endregion

            #region PreviousInventoryAndItemTableRemove

            var getPreviousInventoryHeaderList = await Task.FromResult(_contextW.InventoryHeaders.Where(a => a.InventoryTransTypeId == 3 &&
                                                                           a.InventoryTransTypeName == "Sales" &&
                                                                           a.TransactionRefId ==
                                                                           getSalesOrder.SalesOrderId &&
                                                                           a.IsActive == true).FirstOrDefault());
            var getPreviousInventoryLineList = await Task.FromResult(_contextW.InventoryLines
                                        .Where(a => a.InventoryTransId == getPreviousInventoryHeaderList.InventoryTransId).ToList());

            var PreviousItemList = new List<Item>();
            var PreviousInvLine = new List<InventoryLine>();
            foreach (var itm in getPreviousInventoryLineList)
            {
                var itemL = await Task.FromResult(_contextW.Items.FirstOrDefault(x => x.ItemId == itm.ItemId && x.IsActive == true));

                var CurrentCost = (itemL.TotalValue / itemL.TotalQuantity) * itm.TransQuantity;
                var invL = new InventoryLine()
                {
                    InventoryTransId = itm.InventoryTransId,
                    ItemId = itm.ItemId,
                    ItemName = itm.ItemName,
                    UomId = itm.UomId,
                    UomName = itm.UomName,
                    TransQuantity = itm.TransQuantity * (-1),
                    TransValue = itm.TransValue * (-1),
                };
                PreviousInvLine.Add(invL);

                itemL.TotalValue += CurrentCost;
                itemL.TotalQuantity += itm.TransQuantity;
                PreviousItemList.Add(itemL);
            }
            await _contextW.InventoryLines.AddRangeAsync(PreviousInvLine);
            await _contextW.SaveChangesAsync();

            _contextW.Items.UpdateRange(PreviousItemList);
            await _contextW.SaveChangesAsync();
            #endregion

            #region NewInventoryHeaderRow
            var invHead = new InventoryHeader()
            {
                InventoryTransTypeId = 3,
                InventoryTransTypeName = "Sales",
                TransactionRefId = orderHead.SalesOrderId,
                TransactionRefCode = orderHead.SalesOrderCode,
                PartnerId = orderHead.CustomerId,
                PartnerName = orderHead.CustomerName,
                Remarks = "",//PaymentTypeNarration + " Sales to " + orderHead.CustomerName + ",Sales Items: " + narration,
                AccountId = orderHead.AccountId,
                AccountName = orderHead.AccountName,
                BranchId = orderHead.BranchId,
                BranchName = orderHead.BranchName,
                TransferToBranchId = 0,
                TransferToBranchName = "",
                TransactionDate = SmeTimeZone.CurrentDateTime(),
                ActionById = orderHead.ActionById,
                ActionByName = orderHead.ActionByName,
                ActionTime = SmeTimeZone.CurrentDateTime(),
                IsActive = true,
            };
            await _contextW.InventoryHeaders.AddAsync(invHead);
            await _contextW.SaveChangesAsync();

            var newInvLine = new List<InventoryLine>();
            var itemList = new List<Item>();
            decimal TotalCostOfSales = 0;
            foreach (var itm in orderLine)
            {
                var itemL = await Task.FromResult(_contextW.Items.FirstOrDefault(x => x.ItemId == itm.ItemId && x.IsActive == true));

                var CurrentCost = (itemL.TotalValue / itemL.TotalQuantity) * itm.Quantity;
                TotalCostOfSales += CurrentCost;
                var invL = new InventoryLine()
                {
                    InventoryTransId = invHead.InventoryTransId,
                    ItemId = itm.ItemId,
                    ItemName = itm.ItemName,
                    UomId = itm.UomId,
                    UomName = itm.UomName,
                    TransQuantity = itm.Quantity * (-1),
                    TransValue = CurrentCost * (-1),
                };
                newInvLine.Add(invL);

                itemL.TotalValue -= CurrentCost;
                itemL.TotalQuantity -= itm.Quantity;
                itemList.Add(itemL);

            }
            await _contextW.InventoryLines.AddRangeAsync(newInvLine);
            await _contextW.SaveChangesAsync();

            _contextW.Items.UpdateRange(itemList);
            await _contextW.SaveChangesAsync();

            #endregion


            #region PreviousSubLedgerDataReverse
            var getLedgerList = await Task.FromResult(_contextW.SubLedgerHeaders.Where(a => ((a.TransactionTypeId == 2 && a.TransactionTypeName == "Sales") ||
                                                    (a.TransactionTypeId == 4 && a.TransactionTypeName == "Due Receive"))
                                                   && a.TransactionId == getSalesOrder.SalesOrderId
                                                   && a.AccountId == getSalesOrder.AccountId
                                                   && a.BranchId == getSalesOrder.BranchId).ToList());

            var getPreviousSalesLedgerHeader = getLedgerList.Where(x => x.TransactionTypeId == 2).FirstOrDefault();

            var dueList = new List<DueReceive>();
            foreach (var item in getLedgerList)
            {
                var dueReceiveList = await Task.FromResult(_contextW.DueReceives.Where(c => c.SubLedgerId == 0 ?
                                     c.SalesOrderId == item.TransactionId : c.SubLedgerId == item.SubLedgerHeaderId
                                     && c.IsActive == true
                                     && c.AccountId == item.AccountId
                                     && c.BranchId == item.BranchId).FirstOrDefault());
                if (dueReceiveList != null)
                {
                    dueList.Add(dueReceiveList);
                }
            }
            var subHeadList = new List<SubLedgerHeader>();
            foreach (var a in getLedgerList)
            {
                var preViousSubRows = new List<SME.Models.Write.SubLedgerRow>();

                var previousRowList = _contextW.SubLedgerRows
                                   .Where(c => c.SubLedgerHeaderId == a.SubLedgerHeaderId).ToList();

                foreach (var itm in previousRowList)
                {
                    var subRowNew = new SME.Models.Write.SubLedgerRow()
                    {
                        SubLedgerHeaderId = itm.SubLedgerHeaderId,
                        PartnerId = getSalesOrder.CustomerId,
                        ChartOfAccId = itm.ChartOfAccId,
                        ChartOfAccName = itm.ChartOfAccName,
                        Amount = ((-1) * (itm.Amount))
                    };
                    preViousSubRows.Add(subRowNew);
                }
                await _contextW.SubLedgerRows.AddRangeAsync(preViousSubRows);
                await _contextW.SaveChangesAsync();
            }

            #endregion

            #region NewSubLedgerHeaderRow
            var subHead = new SME.Models.Write.SubLedgerHeader()
            {
                AccountId = orderHead.AccountId,
                AccountName = orderHead.AccountName,
                TransactionId = orderHead.SalesOrderId,
                TransactionCode = orderHead.SalesOrderCode,
                TransactionTypeId = 2,
                TransactionTypeName = "Sales",
                BankAccId = getPreviousSalesLedgerHeader.BankAccId,
                BankAccNumber = getPreviousSalesLedgerHeader.BankAccNumber,
                BranchId = orderHead.BranchId,
                BranchName = orderHead.BranchName,
                CheckNumber = getPreviousSalesLedgerHeader.CheckNumber,
                InstrumentNo = getPreviousSalesLedgerHeader.InstrumentNo,
                InstrumentDate = DateTime.MinValue,
                InstrumentType = getPreviousSalesLedgerHeader.InstrumentType,
                ActionById = orderHead.ActionById,
                ActionByName = orderHead.ActionByName,
                TransactionDate = orderHead.DeliveryDate,
                IsActive = true,
                Narration = "",//PaymentTypeNarration + " sales to " + orderHead.CustomerName + ",Sales Items: " + narration,
                SubLedgerCode = await _codeGen.GenerateCode(orderHead.AccountId, "SubLedger", "SL")

            };
            await _contextW.SubLedgerHeaders.AddAsync(subHead);
            await _contextW.SaveChangesAsync();

            // Sub Row
            var result = new List<ChartOfAccConfiguration>();
            if (orderHead.ReceiveAmount > 0 && orderHead.PendingAmount > 0)
            {
                result = await Task.FromResult(_contextW.ChartOfAccConfigurations
                                            .Where(x => x.Event == "Sales"
                                                        && (x.Mode == "Cash" || x.Mode == "Credit")).ToList());
            }
            else
            {
                result = await Task.FromResult(_contextW.ChartOfAccConfigurations
                                            .Where(x => x.Event == "Sales" && x.Mode == PaymentTypeName).ToList());
            }

            var subRow = new List<SME.Models.Write.SubLedgerRow>();
            long discountCnt = 0;
            long costCnt = 0;
            foreach (var itm in result)
            {
                if (itm.Portion == "Discount")
                {
                    if (discountCnt > 0) continue;
                    else discountCnt++;
                }
                if (itm.Portion == "Cost")
                {
                    if (itm.Portion == "Cost" && costCnt > 0) continue;
                    else costCnt++;
                }

                var value = itm.Portion == "Discount" ? orderHead.NetDiscount :
                            itm.Portion == "Cost" ? TotalCostOfSales :
                            (itm.Portion == "Net" && itm.Mode == "Cash") ? orderHead.ReceiveAmount :
                            (itm.Portion == "Net" && itm.Mode == "Credit") ? orderHead.PendingAmount : 0;

                if (value == 0) continue;

                var debit = new SME.Models.Write.SubLedgerRow()
                {
                    SubLedgerHeaderId = subHead.SubLedgerHeaderId,
                    PartnerId = orderHead.CustomerId,
                    ChartOfAccId = (long)itm.DebitCoaid,
                    ChartOfAccName = itm.DebitCoaname,
                    Amount = value
                };
                subRow.Add(debit);

                var credit = new SME.Models.Write.SubLedgerRow()
                {
                    SubLedgerHeaderId = subHead.SubLedgerHeaderId,
                    PartnerId = orderHead.CustomerId,
                    ChartOfAccId = (long)itm.CreditCoaid,
                    ChartOfAccName = itm.CreditCoaname,
                    Amount = value * (-1)
                };
                subRow.Add(credit);
            }
            await _contextW.SubLedgerRows.AddRangeAsync(subRow);
            await _contextW.SaveChangesAsync();
            #endregion

            #region DueReceiveTableUpdate
            if ((dueList.Count() > 0) && (sme.SOHead.PendingAmount < 0))
            {
                foreach (var due in dueList)
                {

                }
            }
            #endregion

            #region DueAdjustMent
            if (sme.SOHead.AdvanceBalanceAdjust > 0)
            {
                var partner = await Task.FromResult(_contextW.Partners.Where(x => x.PartnerId == sme.SOHead.CustomerId).FirstOrDefault());
                partner.AdvanceBalance -= sme.SOHead.AdvanceBalanceAdjust;

                _contextW.Partners.Update(partner);
                await _contextW.SaveChangesAsync();
            }
            #endregion
            #region PreviousSalesOrderHeaderRowUpdate

            #endregion

            await transaction.CommitAsync();
            return new MessageHelper()
            {
                Message = "Updated Successfully",
                statuscode = 200,
                InvoiceCode = orderHead.SalesOrderCode
            };
        }
        catch (Exception ex)
        {
            await transaction.RollbackAsync();
            if (IsUserException == true)
                throw ex;
            else
                throw new Exception("Something went wrong!");
        }
    }
}