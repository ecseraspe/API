// ---------------------------------------------------------------------------------------------------
// <copyright file="PaymentController.cs" company="Youffer">
//     Copyright (c) 2014 All Right Reserved
// </copyright>
// <author>Gaurav Barar</author>
// <date>2014-12-28</date>
// <summary>
//     The PaymentController class
// </summary>
// ---------------------------------------------------------------------------------------------------

/// <summary>
/// The Controllers namespace.
/// </summary>
namespace Youffer.API.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Security.Cryptography;
    using System.Text;
    using System.Threading.Tasks;
    using System.Web;
    using System.Web.Http;
    using Microsoft.AspNet.Identity;
    using PayPal;
    using PayPal.Api.Payments;
    using Youffer.Common.CRMService;
    using Youffer.Common.DataService;
    using Youffer.Common.Helper;
    using Youffer.Common.LogService;
    using Youffer.Common.Mapper;
    using Youffer.DataService.DBSchema;
    using Youffer.Resources.Constants;
    using Youffer.Resources.CRMModel;
    using Youffer.Resources.Enum;
    using Youffer.Resources.Models;
    using Youffer.Resources.ViewModel;

    /// <summary>
    /// The Payment Controller.
    /// </summary>
    [RoutePrefix("api/payment")]
    [Authorize(Roles = RoleName.CompanyRole)]
    public class PaymentController : BaseApiController
    {
        #region private properties

        /// <summary>
        /// The g2s gateway
        /// </summary>
        private readonly IG2SService g2SService;

        /// <summary>
        /// The mapper factory
        /// </summary>
        private readonly IMapperFactory mapperFactory;

        /// <summary>
        /// The IYoufferContactService service instance
        /// </summary>
        private readonly IYoufferContactService youfferContactService;

        /// <summary>
        /// The IUserService service instance
        /// </summary>
        private readonly IUserService userService;

        /// <summary>
        /// The common service instance
        /// </summary>
        private readonly ICommonService commonService;

        /// <summary>
        /// The IContactService service instance
        /// </summary>
        private readonly ICRMManagerService crmManagerService;

        /// <summary>
        /// The IPaymentService service instance
        /// </summary>
        private readonly IPaymentService paymentService;

        /// <summary>
        /// The char array for hexadecimal
        /// </summary>
        private char[] hexadecimal = { '0', '1', '2', '3', '4', '5', '6', '7', '8', '9', 'a', 'b', 'c', 'd', 'e', 'f' };

        /// <summary>
        /// The md5 message digest
        /// </summary>
        private string messageDigestMD5 = "MD5";

        /// <summary>
        /// The character encoding
        /// </summary>
        private string encoding = "utf-8";

        /// <summary>
        /// The date format
        /// </summary>
        private string dateFormat = "yyyy-MM-dd.HH:mm:ss";

        /// <summary>
        /// Initializes a new instance of the <see cref="PaymentController" /> class.
        /// </summary>
        /// <param name="g2SService">The g2s service.</param>
        /// <param name="loggerService">The logger service.</param>
        /// <param name="crmManagerService">The CRM manager service.</param>
        /// <param name="userService">The user service.</param>
        /// <param name="commonService">The common service.</param>
        /// <param name="youfferContactService">The youffer contact service.</param>
        /// <param name="paymentService">The youffer payment service</param>
        /// <param name="mapperFactory">The Mapper Factory</param>
        public PaymentController(IG2SService g2SService, ILoggerService loggerService, ICRMManagerService crmManagerService, IUserService userService, ICommonService commonService, IYoufferContactService youfferContactService, IPaymentService paymentService, IMapperFactory mapperFactory)
            : base(loggerService)
        {
            this.g2SService = g2SService;
            this.mapperFactory = mapperFactory;
            this.youfferContactService = youfferContactService;
            this.userService = userService;
            this.commonService = commonService;
            this.crmManagerService = crmManagerService;
            this.paymentService = paymentService;
        }

        /// <summary>
        /// Gets the ApiContext
        /// </summary>
        private APIContext Api
        {
            get
            {
                APIContext context = new APIContext(this.AccessToken);
                return context;
            }
        }

        /// <summary>
        /// Gets the Access token
        /// </summary>           
        private string AccessToken
        {
            get
            {
                Dictionary<string, string> payPalConfig = new Dictionary<string, string>();
                payPalConfig.Add("mode", AppSettings.Get<string>("Mode", "Paypal"));
                var accessToken = new OAuthTokenCredential(AppSettings.Get<string>("ClientID"), AppSettings.Get<string>("ClientSecret"), payPalConfig).GetAccessToken();
                return accessToken;
            }
        }
        #endregion

        /// <summary>
        /// Gets the G2S payment request url.
        /// </summary>
        /// <param name="g2SModel">The G2S Model.</param>
        /// <returns>IHttpActionResult object</returns>
        [Route("createrequest")]
        [HttpPost]
        public async Task<IHttpActionResult> CreateRequest(G2SModel g2SModel)
        {
            UserResultModel userResultModel = new UserResultModel();
            string crmContactId = this.userService.GetContact(g2SModel.ClientId).CRMId;
            string crmLeadId = string.Empty;

            if (!string.IsNullOrEmpty(crmContactId))
            {
                crmLeadId = this.youfferContactService.GetMappingEntryByContactId(g2SModel.ClientId).LeadId;
            }

            CountryModel countryDet = this.commonService.GetUserCountryDetails(g2SModel.ClientId);
            decimal rank = this.crmManagerService.GetUserRank(crmContactId);

            var reviews = this.crmManagerService.GetUserReviews(crmContactId);
            List<UserReviewsDto> lstReviews = this.mapperFactory.GetMapper<List<CRMUserReview>, List<UserReviewsDto>>().Map(reviews);

            if (!string.IsNullOrEmpty(crmLeadId))
            {
                LeadModel leadModel = this.crmManagerService.GetLead(crmLeadId);
                userResultModel = this.mapperFactory.GetMapper<LeadModel, UserResultModel>().Map(leadModel);
                userResultModel.PaymentDetails = new PaymentModelDto()
                {
                    PayPalId = leadModel.PaypalId,
                    Mode = (PaymentMode)leadModel.PaymentMode
                };
            }
            else
            {
                ContactModel contactModel = this.crmManagerService.GetContact(g2SModel.ClientId);
                userResultModel = this.mapperFactory.GetMapper<ContactModel, UserResultModel>().Map(contactModel);
                userResultModel.PaymentDetails = new PaymentModelDto()
                {
                    PayPalId = contactModel.PaypalId,
                    Mode = (PaymentMode)contactModel.PaymentMode
                };
            }

            userResultModel.Id = g2SModel.ClientId;
            userResultModel.Rank = rank;
            userResultModel.CountryDetails = countryDet;
            userResultModel.UserRole = Roles.Customer;
            userResultModel.UserReviews = lstReviews;

            ProductModel productModel = new ProductModel()
            {
                Id = userResultModel.Id,
                Title = userResultModel.FirstName,
                Price = AppSettings.Get<decimal>("interestPrice")
            };
            g2SModel.Product = productModel;
            g2SModel.ProcessingFee = (g2SModel.Product.Price * AppSettings.Get<decimal>("processingFeePercent")) / 100;
            g2SModel.CompanyId = User.Identity.GetUserId();
            string merchantKey = AppSettings.Get<string>("secret_key");
            string pppUrl = AppSettings.Get<string>("live_pppURL");
            ////string pppUrl = AppSettings.Get<string>("test_pppURL");
            try
            {
                g2SModel.InvoiceId = this.CreateInvoice(g2SModel).ToString();
            }
            catch (Exception ex)
            {
                this.LoggerService.LogException("PaymentController > CreateRequest(while creating invoice): " + ex.StackTrace);
            }

            Dictionary<string, string> parameters = this.ConstructParametersMap(this.encoding, g2SModel);
            string checksum = this.CalculateChecksum(parameters, merchantKey, this.encoding);
            parameters.Add("checksum", checksum);
            string encoded_params = this.EncodeParameters(parameters, this.encoding);
            try
            {
                G2SRequestModel g2SRequestModel = this.FillG2SRequest(parameters, g2SModel);
                this.paymentService.InsertG2SRequest(g2SRequestModel);
            }
            catch (Exception ex)
            {
                this.LoggerService.LogException("PaymentController > G2SSuccess(while filling or inserting request): " + ex.StackTrace);
            }

            return this.Ok(pppUrl + encoded_params);
        }

        /// <summary>
        /// Sets the G2S payment pending transaction request data.
        /// </summary>
        /// <returns>Sets the G2S payment success transaction request data.</returns>
        [Route("g2ssuccess")]
        [HttpGet]
        [AllowAnonymous]
        public async Task<IHttpActionResult> G2SSuccess()
        {
            G2SResponseModel response = new G2SResponseModel();
            try
            {
                response = this.FillG2SResponse(Request.RequestUri.Query);
                try
                {
                    this.UpdateInvoice(Convert.ToInt64(response.InvoiceId), InvoiceStatus.Success);
                }
                catch (Exception ex)
                {
                    this.LoggerService.LogException("PaymentController > G2SSuccess(while updating invoice): " + ex.StackTrace);
                }

                this.paymentService.InsertG2SResponse(response);
            }
            catch (Exception ex)
            {
                this.LoggerService.LogException("PaymentController > G2SSuccess(while filling or inserting response): " + ex.StackTrace);
                response.UserId = HttpUtility.ParseQueryString(Request.RequestUri.Query).Get("customField1");
                response.Interest = HttpUtility.ParseQueryString(Request.RequestUri.Query).Get("customField2");
                response.CompanyId = HttpUtility.ParseQueryString(Request.RequestUri.Query).Get("customField3");
            }

            try
            {
                bool isSuccess = true;
                decimal price = AppSettings.Get<decimal>(ConfigConstants.UserPrice);

                BuyUserModelDto buyUserModelDto = new BuyUserModelDto { CompanyId = response.CompanyId, UserId = response.UserId, Interest = response.Interest, Amount = price, PurchasedFromCash = false, PurchasedFromCredit = false };
                isSuccess = this.crmManagerService.AddOpportunity(buyUserModelDto);
            }
            catch (Exception ex)
            {
                this.LoggerService.LogException("PaymentController > G2SSuccess(while updating system for who purchased what): " + ex.StackTrace);
            }

            return this.Redirect(AppSettings.Get<string>("site_payment_url").ToString() + "?status=success");
        }

        /// <summary>
        /// Sets the G2S payment error transaction request data.
        /// </summary>
        /// <returns>Redirects to main site</returns>
        [Route("g2serror")]
        [HttpGet]
        [AllowAnonymous]
        public async Task<IHttpActionResult> G2SError()
        {
            G2SResponseModel response = new G2SResponseModel();
            try
            {
                response = this.FillG2SResponse(Request.RequestUri.Query);
                try
                {
                    this.UpdateInvoice(Convert.ToInt64(response.InvoiceId), InvoiceStatus.Failure);
                }
                catch (Exception ex)
                {
                    this.LoggerService.LogException("PaymentController > G2SError(while updating invoice): " + ex.StackTrace);
                }

                this.paymentService.InsertG2SResponse(response);
            }
            catch (Exception ex)
            {
                this.LoggerService.LogException("PaymentController > G2SError(while filling or inserting response): " + ex.StackTrace);
            }

            return this.Redirect(AppSettings.Get<string>("site_payment_url").ToString() + "?status=error");
        }

        /// <summary>
        /// Sets the G2S payment pending transaction request data.
        /// </summary>
        /// <returns>Redirects to main site</returns>
        [Route("g2spending")]
        [HttpGet]
        [AllowAnonymous]
        public async Task<IHttpActionResult> G2SPending()
        {
            G2SResponseModel response = new G2SResponseModel();
            try
            {
                response = this.FillG2SResponse(Request.RequestUri.Query);
                try
                {
                    this.UpdateInvoice(Convert.ToInt64(response.InvoiceId), InvoiceStatus.Pending);
                }
                catch (Exception ex)
                {
                    this.LoggerService.LogException("PaymentController > G2SPending(while updating invoice): " + ex.StackTrace);
                }

                this.paymentService.InsertG2SResponse(response);
            }
            catch (Exception ex)
            {
                this.LoggerService.LogException("PaymentController > G2SPending(while filling or inserting response): " + ex.StackTrace);
            }

            return this.Redirect(AppSettings.Get<string>("site_payment_url").ToString() + "?status=pending");
        }

        /// <summary>
        /// Sets the G2S payment DMN request data.
        /// </summary>
        /// <returns>IHttpActionResult object</returns>
        [Route("g2sdmn")]
        [HttpGet]
        [AllowAnonymous]
        public async Task<IHttpActionResult> DMN()
        {
            G2SDMNModel g2SDMNModel = new G2SDMNModel();
            InvoiceStatus invoiceStatus = InvoiceStatus.Invalid;
            try
            {
                g2SDMNModel = this.FillG2SDMN(Request.RequestUri.Query);
                if (g2SDMNModel.PPPStatus.ToLower() == "OK".ToLower())
                {
                    if (g2SDMNModel.Status.ToLower() == "Approved".ToLower() || g2SDMNModel.Status.ToLower() == "Success".ToLower())
                    {
                        invoiceStatus = InvoiceStatus.Success;
                        this.UpdateInvoice(Convert.ToInt64(g2SDMNModel.InvoiceId), InvoiceStatus.Success);
                    }
                    else if (g2SDMNModel.Status.ToLower() == "Declined".ToLower() || g2SDMNModel.Status.ToLower() == "Error".ToLower())
                    {
                        invoiceStatus = InvoiceStatus.Failure;
                        this.UpdateInvoice(Convert.ToInt64(g2SDMNModel.InvoiceId), InvoiceStatus.Failure);
                    }
                    else if (g2SDMNModel.Status.ToLower() == "Pending".ToLower())
                    {
                        invoiceStatus = InvoiceStatus.Pending;
                        this.UpdateInvoice(Convert.ToInt64(g2SDMNModel.InvoiceId), InvoiceStatus.Pending);
                    }
                }
                else if (g2SDMNModel.PPPStatus.ToLower() == "Fail".ToLower())
                {
                    invoiceStatus = InvoiceStatus.Failure;
                    this.UpdateInvoice(Convert.ToInt64(g2SDMNModel.InvoiceId), InvoiceStatus.Failure);
                }

                try
                {
                    if (invoiceStatus != InvoiceStatus.Invalid)
                    {
                        this.UpdateInvoice(Convert.ToInt64(g2SDMNModel.InvoiceId), invoiceStatus);
                    }
                    else
                    {
                        this.LoggerService.LogException("PaymentController > DMN(Manually Generated): Unrecognized PPPStatus in G2S DMN Request.");
                    }
                }
                catch (Exception ex)
                {
                    this.LoggerService.LogException("PaymentController > DMN(while updating invoice): " + ex.StackTrace);
                }

                this.paymentService.InsertG2SDMN(g2SDMNModel);
            }
            catch (Exception ex)
            {
                this.LoggerService.LogException("PaymentController > DMN(while filling or inserting response): " + ex.StackTrace);
            }

            return this.Ok();
        }

        /// <summary>
        /// Gets the Paypal Url to redirect
        /// </summary>
        /// <param name="wrapper">Wrapper of items to make a transaction</param>
        /// <returns>IHttpActionResult object contains a url to redirect.</returns>
        [Route("paypalurl")]
        [HttpPost]
        public async Task<IHttpActionResult> PaypalUrl(PaypalDetailsWrapper wrapper)
        {
            string companyId = User.Identity.GetUserId();
            List<PayPalDetailsModel> list = wrapper.ItemDetails;
            PaypalDetails details = wrapper.PaypalDetails;
            var guid = Guid.NewGuid();
            this.commonService.SetCache<PaypalDetails>(details, guid.ToString());
            List<Item> itemList = this.GetItemListing(list);
            if (!itemList.Any())
            {
                return this.BadRequest("No items found");
            }

            var price = itemList.Sum(p => Convert.ToDecimal(p.price)).ToString("N2");

            var paymentInit = new Payment
            {
                intent = "sale",
                payer = new Payer
                {
                    payment_method = AppSettings.Get<string>("PaymentMethod")
                },
                transactions = new List<Transaction> 
                {
                new Transaction 
                {
                    item_list = new ItemList 
                    { 
                        items = itemList
                    },
                    amount = new Amount 
                    {
                        currency = AppSettings.Get<string>("interestPriceCurrency"),
                        total = price.ToString(),
                        details = new Details
                        {
                            subtotal = price.ToString(),

                            ////tax = tax.ToString(),
                            ////shipping = shipping.ToString()
                        }
                    }, 

                    ////description = description
                },
            },
                redirect_urls = new RedirectUrls
                {
                    return_url = string.Format(AppSettings.Get<string>("PaypalReturnURL"), guid),
                    cancel_url = string.Format(AppSettings.Get<string>("PaypalCancelURL"), guid),
                },
            };

            try
            {
                var createdPayment = paymentInit.Create(this.Api);
                var approvalUrl = createdPayment.links.ToArray().FirstOrDefault(f => f.rel.Contains("approval_url"));

                if (approvalUrl != null)
                {
                    bool result = this.paymentService.InsertPayPalTransaction(new PaypalPayTransactionModel
                    {
                        CreatedOn = DateTime.UtcNow,
                        PaymentId = createdPayment.id,
                        PayToken = guid,
                        IsPaymentDone = false,
                        ClientId = list[0].Id,
                        CompanyId = companyId,
                        Need = list[0].Need,
                        Amount = Convert.ToDecimal(price),
                        IsPaypalTransaction = true
                    });
                    if (result)
                    {
                        return this.Ok<string>(approvalUrl.href);
                    }
                }

                this.LoggerService.LogException("PaymentController > Paypal error");
                return this.BadRequest("Paypal error");
            }
            catch (Exception ex)
            {
                this.LoggerService.LogException("PaymentController > Paypal runtime error: " + ex.StackTrace);
                return this.BadRequest("Paypal runtime error");
            }
        }

        /// <summary>
        /// Confirms the Paypal transaction
        /// </summary>
        /// <param name="token">Token returned by Paypal on confirmation</param>
        /// <param name="payerId">Payer Id returned by Paypal on confirmation</param>
        /// <param name="id">Id returned by Paypal on confirmation</param>
        /// <returns>IHttpActionResult object</returns>
        [HttpGet]
        [AllowAnonymous]
        public async Task<IHttpActionResult> PaypalConfirmed(string token, string payerId, string id)
        {
            try
            {
                PaypalDetails paypalDetails = this.commonService.GetCache<PaypalDetails>(id);
                PaypalPayTransactionModel paymentTransaction = this.paymentService.GetPaypalPaymentId(Guid.Parse(id));
                if (paymentTransaction == null)
                {
                    return this.Redirect(string.Format(AppSettings.Get<string>("WebReturnURL"), paypalDetails.Domain, "Invalid request"));
                }

                string paymentId = paymentTransaction.PaymentId;
                if (string.IsNullOrWhiteSpace(paymentId))
                {
                    return this.Redirect(string.Format(AppSettings.Get<string>("WebReturnURL"), paypalDetails.Domain, "Can not proceed now, please try again."));
                }

                var paymentExecution = new PaymentExecution
                {
                    payer_id = payerId
                };

                var payment = new Payment { id = paymentId };
                var response = payment.Execute(this.Api, paymentExecution);
                if (response.state.ToLower() == "approved")
                {
                    PaypalPaymentDetailsModel paymentDetail = new PaypalPaymentDetailsModel();
                    paymentDetail.TransactionId = paymentTransaction.Id;
                    paymentDetail.PayerId = payerId;
                    paymentDetail.Email = response.payer.payer_info.email;
                    paymentDetail.FirstName = response.payer.payer_info.first_name;
                    paymentDetail.LastName = response.payer.payer_info.last_name;
                    paymentDetail.Address = response.payer.payer_info.shipping_address.line1 + " " + response.payer.payer_info.shipping_address.line2 + " " + response.payer.payer_info.shipping_address.state;
                    paymentDetail.Status = response.payer.status;
                    paymentDetail.IsComplete = true;
                    paymentDetail.CreatedOn = DateTime.UtcNow;
                    if (this.paymentService.CompletePaypalTransaction(paymentDetail))
                    {
                        try
                        {
                            bool isSuccess = true;
                            string crmContactId = this.youfferContactService.GetMappingEntryByContactId(paymentTransaction.ClientId).ContactCRMId;
                            string crmLeadId = this.youfferContactService.GetMappingEntryByContactId(paymentTransaction.ClientId).LeadId;
                            string crmOrgId = this.youfferContactService.GetOrgCRMId(paymentTransaction.CompanyId).CRMId;
                            LeadModel leadModel = this.crmManagerService.GetLead(crmLeadId);

                            decimal price = AppSettings.Get<decimal>(ConfigConstants.UserPrice);
                            BuyUserModelDto buyUserModelDto = new BuyUserModelDto { CompanyId = paymentTransaction.CompanyId, UserId = paymentTransaction.ClientId, Interest = paymentTransaction.Need, Amount = price, PurchasedFromCash = false, PurchasedFromCredit = false };
                            isSuccess = this.crmManagerService.AddOpportunity(buyUserModelDto);

                            ////return this.Redirect(string.Format(AppSettings.Get<string>("WebReturnURL"), "Payment Done"));
                            return this.Redirect(string.Format(AppSettings.Get<string>("WebPaymentDone").ToString() + "?status=success", paypalDetails.Domain));
                        }
                        catch (Exception ex)
                        {
                            this.LoggerService.LogException("PaymentController > G2SSuccess(while updating system for who purchased what): " + ex.StackTrace);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                this.LoggerService.LogException("PaypalConfirmed :- " + ex.Message);
            }

            ////return this.Redirect(string.Format(AppSettings.Get<string>("WebReturnURL"), "Payment Error found"));
            return this.Redirect(string.Format(AppSettings.Get<string>("WebPaymentDone").ToString() + "?status=error"));
        }

        /// <summary>
        /// Confirms the 2CheckOut transaction
        /// </summary>
        /// <returns>IHttpActionResult object</returns>
        [Route("success2co")]
        [HttpPost]
        [AllowAnonymous]
        public async Task<IHttpActionResult> ReturnFrom2CO()
        {
            ////string companyId = User.Identity.GetUserId();
            string companyId = HttpContext.Current.Request.Form["clientId"].ToString();
            var success = HttpContext.Current.Request.Form["credit_card_processed"].ToString();
            if (success.Equals("Y", StringComparison.InvariantCultureIgnoreCase))
            {
                var guid = Guid.NewGuid();
                bool result = this.paymentService.InsertPayPalTransaction(new PaypalPayTransactionModel
                {
                    CreatedOn = DateTime.UtcNow,
                    PaymentId = HttpContext.Current.Request.Form["order_number"].ToString(),
                    PayToken = guid,
                    IsPaymentDone = true,
                    ClientId = HttpContext.Current.Request.Form["li_0_product_id"].ToString(),
                    CompanyId = companyId,
                    Need = HttpContext.Current.Request.Form["li_0_name"].ToString(),
                    Amount = Convert.ToDecimal(HttpContext.Current.Request.Form["tPrice"]),
                    IsPaypalTransaction = false
                });

                if (result)
                {
                    PaypalPayTransactionModel paymentTransaction = this.paymentService.GetPaypalPaymentId(guid, true);
                    PaypalPaymentDetailsModel paymentDetail = new PaypalPaymentDetailsModel();
                    paymentDetail.TransactionId = paymentTransaction.Id;
                    paymentDetail.PayerId = "2COpayer";
                    paymentDetail.Email = HttpContext.Current.Request.Form["email"].ToString();
                    paymentDetail.FirstName = HttpContext.Current.Request.Form["card_holder_name"].ToString();
                    paymentDetail.Address = HttpContext.Current.Request.Form["ship_street_address"].ToString() + " " + HttpContext.Current.Request.Form["ship_street_address2"].ToString() + " " + HttpContext.Current.Request.Form["ship_state"].ToString() + " " + HttpContext.Current.Request.Form["ship_city"].ToString() + " " + HttpContext.Current.Request.Form["ship_zip"].ToString() + " " + HttpContext.Current.Request.Form["ship_country"].ToString();
                    paymentDetail.Status = "SUCCESS";
                    paymentDetail.IsComplete = true;
                    paymentDetail.CreatedOn = DateTime.UtcNow;
                    if (this.paymentService.CompletePaypalTransaction(paymentDetail))
                    {
                        try
                        {
                            bool isSuccess = true;

                            BuyUserModelDto buyUserModelDto = new BuyUserModelDto { CompanyId = paymentTransaction.CompanyId, UserId = paymentTransaction.ClientId, Interest = paymentTransaction.Need, Amount = paymentTransaction.Amount, PurchasedFromCash = false, PurchasedFromCredit = false };
                            isSuccess = this.crmManagerService.AddOpportunity(buyUserModelDto);

                            ////return this.Redirect(string.Format(AppSettings.Get<string>("WebReturnURL"), "Payment Done"));
                            return this.Redirect(AppSettings.Get<string>("site_payment_url").ToString() + "?status=success");
                        }
                        catch (Exception ex)
                        {
                            this.LoggerService.LogException("PaymentController > ReturnFrom2CO(while updating system after successfull payment): " + ex.StackTrace);
                        }
                    }
                }
            }

            return this.Redirect(AppSettings.Get<string>("site_payment_url").ToString() + "?status=error");
        }

        /// <summary>
        /// Confirms that the user has cancelled the Paypal transaction.
        /// </summary>
        /// <returns>IHttpActionResult object</returns>
        [HttpGet]
        [AllowAnonymous]
        public async Task<IHttpActionResult> PaypalCancelled()
        {
            ////return this.Redirect(string.Format(AppSettings.Get<string>("WebReturnURL"), "Payment Cancelled by you"));
            return this.Redirect(AppSettings.Get<string>("site_payment_url").ToString() + "?status=cancelled");
        }

        /// <summary>
        /// Gets the Item listing
        /// </summary>
        /// <param name="list">List of PaypalDetailsModel</param>
        /// <returns>List object</returns>
        private List<Item> GetItemListing(List<PayPalDetailsModel> list)
        {
            List<Item> itemList = new List<Item>();
            try
            {
                foreach (PayPalDetailsModel item in list)
                {
                    itemList.Add(new Item
                    {
                        name = item.Name,
                        currency = AppSettings.Get<string>("interestPriceCurrency"),
                        price = item.Price.ToString("N2"),
                        quantity = "1"
                    });
                }
            }
            catch (Exception ex)
            {
                this.LoggerService.LogException("GetItemListing :- " + ex.Message);
            }

            return itemList;
        }

        #region Helpers

        /// <summary>
        /// Identifies if the transaction is fake.
        /// </summary>
        /// <returns>Transaction is fake or not.</returns>
        private bool PaymentVerification()
        {
            return false;
        }

        /// <summary>
        /// checkes if checksum is valid
        /// </summary>
        /// <param name="totalAmount">total amount.</param>
        /// <param name="currency">transaction currency.</param>
        /// <param name="responseTimeStamp">response timestamp.</param>
        /// <param name="pppTransactionID">ppptransaction id.</param>
        /// <param name="status">status of transaction.</param>
        /// <param name="productId">product id.</param>
        /// <param name="responseChecksum">response checksum.</param>
        /// <param name="advanceResponseChecksum">advance response checksum.</param>
        /// <returns>true or false</returns>
        private bool IsChecksumValid(string totalAmount, string currency, string responseTimeStamp, string pppTransactionID, string status, string productId, string responseChecksum, string advanceResponseChecksum)
        {
            string hashString = AppSettings.Get<string>("secret_key")
            + totalAmount
            + currency
            + responseTimeStamp
            + pppTransactionID
            + status
            + productId;
            string checksum = this.GetHash(hashString, this.encoding);
            bool result = (checksum == responseChecksum ? true : false) == false ? (checksum == advanceResponseChecksum ? true : false) : true;
            return result;
        }

        /// <summary>
        /// Generates invoice for the transaction.
        /// </summary>
        /// <param name="g2SModel">g2s model.</param>
        /// <returns>Invoice created successfully or not.</returns>
        private long CreateInvoice(G2SModel g2SModel)
        {
            InvoiceModel invoiceModel = new InvoiceModel();
            invoiceModel.ClientId = g2SModel.ClientId;
            invoiceModel.ClientInterest = g2SModel.ClientInterest;
            invoiceModel.CompanyId = g2SModel.CompanyId;
            invoiceModel.EnumStatus = InvoiceStatus.Pending;
            long result = this.paymentService.CreateInvoice(invoiceModel);
            return result;
        }

        /// <summary>
        /// Updates invoice for the transaction.
        /// </summary>
        /// <param name="id">invoice id.</param>
        /// <param name="invoiceStatus">status of payment and invoice.</param>
        /// <returns>Invoice updated successfully or not.</returns>
        private bool UpdateInvoice(long id, InvoiceStatus invoiceStatus)
        {
            InvoiceModel invoiceModel = this.paymentService.GetInvoice(id);
            invoiceModel.EnumStatus = invoiceStatus;
            bool result = this.paymentService.UpdateInvoice(invoiceModel);
            return result;
        }

        /// <summary>
        /// Mails invoice for the transaction.
        /// </summary>
        /// <returns>Mail sent successfully or not.</returns>
        private bool MailInvoice()
        {
            return false;
        }

        /// <summary>
        /// Encodes parameters in stiring
        /// </summary>
        /// <param name="parameters">Dictionary of string, string having params.</param>
        /// <param name="encoding">encoding string.</param>
        /// <returns>string of encoded parameters</returns>
        private string EncodeParameters(Dictionary<string, string> parameters, string encoding)
        {
            StringBuilder postData = new StringBuilder();
            foreach (KeyValuePair<string, string> param in parameters)
            {
                if (postData.Length != 0)
                {
                    postData.Append("&");
                }

                postData.Append(HttpUtility.UrlDecode(param.Key, Encoding.GetEncoding(encoding)));
                postData.Append("=");
                postData.Append(HttpUtility.UrlDecode(param.Value, Encoding.GetEncoding(encoding)));
            }

            return postData.ToString();
        }

        /// <summary>
        /// Creates parameters map
        /// </summary>
        /// <param name="encoding">The encoding.</param>
        /// <param name="g2SModel">The g2 s model.</param>
        /// <returns>Dictionary object</returns>
        private Dictionary<string, string> ConstructParametersMap(string encoding, G2SModel g2SModel)
        {
            Dictionary<string, string> parameters = new Dictionary<string, string>();
            string timestamp = DateTime.UtcNow.ToString(this.dateFormat);
            parameters.Add("merchant_site_id", AppSettings.Get<string>("merchant_site_id"));
            parameters.Add("merchant_id", AppSettings.Get<string>("merchant_id"));
            parameters.Add("currency", AppSettings.Get<string>("interestPriceCurrency"));
            parameters.Add("item_name_1", g2SModel.Product.Title);
            parameters.Add("item_amount_1", (Convert.ToDouble(g2SModel.Product.Price) + Convert.ToDouble(g2SModel.ProcessingFee)).ToString());
            parameters.Add("numberofitems", "1");
            parameters.Add("total_amount", (Convert.ToDouble(g2SModel.Product.Price) + Convert.ToDouble(g2SModel.ProcessingFee)).ToString());
            parameters.Add("item_number_1", "1");
            parameters.Add("item_quantity_1", "1");
            parameters.Add("encoding", encoding);
            parameters.Add("time_stamp", timestamp);
            parameters.Add("version", "4.0.0");
            parameters.Add("invoice_id", g2SModel.InvoiceId);
            parameters.Add("success_url", AppSettings.Get<string>("success_url"));
            parameters.Add("error_url", AppSettings.Get<string>("error_url"));
            parameters.Add("pending_url", AppSettings.Get<string>("pending_url"));
            parameters.Add("notify_url", AppSettings.Get<string>("dmn_url"));
            parameters.Add("customField1", g2SModel.ClientId);
            parameters.Add("customField2", g2SModel.ClientInterest);
            parameters.Add("customField3", g2SModel.CompanyId);
            return parameters;
        }

        /// <summary>
        /// calculates checksum
        /// </summary>
        /// <param name="parameters">The _params.</param>
        /// <param name="merchantKey">The merchant key.</param>
        /// <param name="encoding">The encoding.</param>
        /// <returns>checksum string</returns>
        private string CalculateChecksum(Dictionary<string, string> parameters, string merchantKey, string encoding)
        {
            StringBuilder allVals = new StringBuilder();
            allVals.Append(merchantKey);
            foreach (KeyValuePair<string, string> param in parameters)
            {
                allVals.Append(param.Value);
            }

            string checksum = this.GetHash(allVals.ToString(), encoding);
            return checksum;
        }

        /// <summary>
        /// Fills the g2s response.
        /// </summary>
        /// <param name="parameters">The parameters.</param>
        /// <param name="g2SModel">The G2S Model.</param>
        /// <returns>G2S Request Model.</returns>
        private G2SRequestModel FillG2SRequest(Dictionary<string, string> parameters, G2SModel g2SModel)
        {
            G2SRequestModel request = new G2SRequestModel();
            request.Currency = parameters["currency"];
            request.ItemName = parameters["item_name_1"];
            request.ItemAmount = parameters["item_amount_1"];
            request.NumberOfItems = parameters["numberofitems"];
            request.TotalAmount = parameters["total_amount"];
            request.ItemNumber = parameters["item_number_1"];
            request.ItemQuantity = parameters["item_quantity_1"];
            request.Encoding = parameters["encoding"];
            request.TimeStamp = parameters["time_stamp"];
            request.Version = parameters["version"];
            request.InvoiceId = parameters["invoice_id"];
            request.SuccessUrl = parameters["success_url"];
            request.ErrorUrl = parameters["error_url"];
            request.PendingUrl = parameters["pending_url"];
            request.NotifyUrl = parameters["notify_url"];
            request.ClientId = g2SModel.ClientId;
            request.ClientInterest = g2SModel.ClientInterest;
            request.CompanyId = g2SModel.CompanyId;
            request.Address = string.Empty;
            request.City = string.Empty;
            request.Country = string.Empty;
            request.Email = string.Empty;
            request.Phone = string.Empty;
            request.Zip = string.Empty;
            return request;
        }

        /// <summary>
        /// Fills the g2s response.
        /// </summary>
        /// <param name="query">The query.</param>
        /// <returns>G2S Response Model.</returns>
        private G2SResponseModel FillG2SResponse(string query)
        {
            G2SResponseModel response = new G2SResponseModel();
            response.PPPStatus = HttpUtility.ParseQueryString(query).Get("ppp_status");
            response.CardCompany = HttpUtility.ParseQueryString(query).Get("cardCompany");
            response.NameOnCard = HttpUtility.ParseQueryString(query).Get("nameOnCard");
            response.FirstName = HttpUtility.ParseQueryString(query).Get("first_name");
            response.LastName = HttpUtility.ParseQueryString(query).Get("last_name");
            response.Address = HttpUtility.ParseQueryString(query).Get("address1");
            response.City = HttpUtility.ParseQueryString(query).Get("city");
            response.Country = HttpUtility.ParseQueryString(query).Get("country");
            response.Email = HttpUtility.ParseQueryString(query).Get("email");
            response.State = HttpUtility.ParseQueryString(query).Get("state");
            response.Zip = HttpUtility.ParseQueryString(query).Get("zip");
            response.Phone = HttpUtility.ParseQueryString(query).Get("phone1");
            response.Currency = HttpUtility.ParseQueryString(query).Get("currency");
            response.UserId = HttpUtility.ParseQueryString(query).Get("customField1");
            response.Interest = HttpUtility.ParseQueryString(query).Get("customField2");
            response.CompanyId = HttpUtility.ParseQueryString(query).Get("customField3");
            response.MerchantSiteId = HttpUtility.ParseQueryString(query).Get("merchant_site_id");
            response.MerchantId = HttpUtility.ParseQueryString(query).Get("merchant_id");
            response.MerchantLocale = HttpUtility.ParseQueryString(query).Get("merchantLocale");
            response.RequestVersion = HttpUtility.ParseQueryString(query).Get("requestVersion");
            response.PPPTransactionID = HttpUtility.ParseQueryString(query).Get("PPP_TransactionID");
            response.ProductId = HttpUtility.ParseQueryString(query).Get("productId");
            response.CustomData = HttpUtility.ParseQueryString(query).Get("customData");
            response.PaymentMethod = HttpUtility.ParseQueryString(query).Get("payment_method");
            response.InvoiceId = HttpUtility.ParseQueryString(query).Get("invoice_id");
            response.ResponseTimeStamp = HttpUtility.ParseQueryString(query).Get("responseTimeStamp");
            response.Message = HttpUtility.ParseQueryString(query).Get("message");
            response.Error = HttpUtility.ParseQueryString(query).Get("Error");
            response.Status = HttpUtility.ParseQueryString(query).Get("Status");
            response.ExErrCode = HttpUtility.ParseQueryString(query).Get("ExErrCode");
            response.ErrCode = HttpUtility.ParseQueryString(query).Get("ErrCode");
            response.AuthCode = HttpUtility.ParseQueryString(query).Get("AuthCode");
            response.ReasonCode = HttpUtility.ParseQueryString(query).Get("ReasonCode");
            response.Token = HttpUtility.ParseQueryString(query).Get("Token");
            response.TokenId = HttpUtility.ParseQueryString(query).Get("tokenId");
            response.ResponseChecksum = HttpUtility.ParseQueryString(query).Get("responsechecksum");
            response.AdvanceResponseChecksum = HttpUtility.ParseQueryString(query).Get("advanceResponseChecksum");
            response.TotalAmount = HttpUtility.ParseQueryString(query).Get("totalAmount");
            response.TransactionID = HttpUtility.ParseQueryString(query).Get("TransactionID");
            response.DynamicDescriptor = HttpUtility.ParseQueryString(query).Get("dynamicDescriptor");
            response.UniqueCC = HttpUtility.ParseQueryString(query).Get("uniqueCC");
            response.ItemNumber = HttpUtility.ParseQueryString(query).Get("item_number_1");
            response.ItemAmount = HttpUtility.ParseQueryString(query).Get("item_amount_1");
            response.ItemQuantity = HttpUtility.ParseQueryString(query).Get("item_quantity_1");
            response.IsValid = this.IsChecksumValid(response.TotalAmount, response.Currency, response.ResponseTimeStamp, response.PPPTransactionID, response.Status, response.ProductId, response.ResponseChecksum, response.AdvanceResponseChecksum);
            return response;
        }

        /// <summary>
        /// Fills the g2s DMN.
        /// </summary>
        /// <param name="query">The query.</param>
        /// <returns>G2S DMN Model.</returns>
        private G2SDMNModel FillG2SDMN(string query)
        {
            G2SDMNModel dmn = new G2SDMNModel();
            dmn.PPPStatus = HttpUtility.ParseQueryString(query).Get("ppp_status");
            dmn.CardCompany = HttpUtility.ParseQueryString(query).Get("cardCompany");
            dmn.NameOnCard = HttpUtility.ParseQueryString(query).Get("nameOnCard");
            dmn.FirstName = HttpUtility.ParseQueryString(query).Get("first_name");
            dmn.LastName = HttpUtility.ParseQueryString(query).Get("last_name");
            dmn.Address = HttpUtility.ParseQueryString(query).Get("address1");
            dmn.City = HttpUtility.ParseQueryString(query).Get("city");
            dmn.Country = HttpUtility.ParseQueryString(query).Get("country");
            dmn.Email = HttpUtility.ParseQueryString(query).Get("email");
            dmn.State = HttpUtility.ParseQueryString(query).Get("state");
            dmn.Zip = HttpUtility.ParseQueryString(query).Get("zip");
            dmn.Phone = HttpUtility.ParseQueryString(query).Get("phone1");
            dmn.Currency = HttpUtility.ParseQueryString(query).Get("currency");
            dmn.UserId = HttpUtility.ParseQueryString(query).Get("customField1");
            dmn.Interest = HttpUtility.ParseQueryString(query).Get("customField2");
            dmn.CompanyId = HttpUtility.ParseQueryString(query).Get("customField3");
            dmn.MerchantSiteId = HttpUtility.ParseQueryString(query).Get("merchant_site_id");
            dmn.MerchantId = HttpUtility.ParseQueryString(query).Get("merchant_id");
            dmn.MerchantLocale = HttpUtility.ParseQueryString(query).Get("merchantLocale");
            dmn.RequestVersion = HttpUtility.ParseQueryString(query).Get("requestVersion");
            dmn.PPPTransactionID = HttpUtility.ParseQueryString(query).Get("PPP_TransactionID");
            dmn.ProductId = HttpUtility.ParseQueryString(query).Get("productId");
            dmn.CustomData = HttpUtility.ParseQueryString(query).Get("customData");
            dmn.PaymentMethod = HttpUtility.ParseQueryString(query).Get("payment_method");
            dmn.InvoiceId = HttpUtility.ParseQueryString(query).Get("invoice_id");
            dmn.ResponseTimeStamp = HttpUtility.ParseQueryString(query).Get("responseTimeStamp");
            dmn.Message = HttpUtility.ParseQueryString(query).Get("message");
            dmn.Error = HttpUtility.ParseQueryString(query).Get("Error");
            dmn.Status = HttpUtility.ParseQueryString(query).Get("Status");
            dmn.ExErrCode = HttpUtility.ParseQueryString(query).Get("ExErrCode");
            dmn.ErrCode = HttpUtility.ParseQueryString(query).Get("ErrCode");
            dmn.AuthCode = HttpUtility.ParseQueryString(query).Get("AuthCode");
            dmn.ReasonCode = HttpUtility.ParseQueryString(query).Get("ReasonCode");
            dmn.Token = HttpUtility.ParseQueryString(query).Get("Token");
            dmn.TokenId = HttpUtility.ParseQueryString(query).Get("tokenId");
            dmn.ResponseChecksum = HttpUtility.ParseQueryString(query).Get("responsechecksum");
            dmn.AdvanceResponseChecksum = HttpUtility.ParseQueryString(query).Get("advanceResponseChecksum");
            dmn.TotalAmount = HttpUtility.ParseQueryString(query).Get("totalAmount");
            dmn.TransactionID = HttpUtility.ParseQueryString(query).Get("TransactionID");
            dmn.DynamicDescriptor = HttpUtility.ParseQueryString(query).Get("dynamicDescriptor");
            dmn.UniqueCC = HttpUtility.ParseQueryString(query).Get("uniqueCC");
            dmn.ItemNumber = HttpUtility.ParseQueryString(query).Get("item_number_1");
            dmn.ItemAmount = HttpUtility.ParseQueryString(query).Get("item_amount_1");
            dmn.ItemQuantity = HttpUtility.ParseQueryString(query).Get("item_quantity_1");
            dmn.IsValid = this.IsChecksumValid(dmn.TotalAmount, dmn.Currency, dmn.ResponseTimeStamp, dmn.PPPTransactionID, dmn.Status, dmn.ProductId, dmn.ResponseChecksum, dmn.AdvanceResponseChecksum);
            return dmn;
        }

        /// <summary>
        /// Gets Hash
        /// </summary>
        /// <param name="text">The text.</param>
        /// <param name="charset">The charset.</param>
        /// <returns>hash string</returns>
        private string GetHash(string text, string charset)
        {
            HashAlgorithm md = HashAlgorithm.Create(this.messageDigestMD5);
            if (md == null)
            {
                System.Console.Error.WriteLine("MD5 implementation not found.");
                return null;
            }

            byte[] encoded = null;
            try
            {
                encoded = Encoding.GetEncoding(this.encoding).GetBytes(text);
            }
            catch (EncoderFallbackException ex)
            {
                this.LoggerService.LogException("PaymentController > GetHash(Cannot encode text into bytes): " + ex.Message);
                System.Console.Error.WriteLine("Cannot encode text into bytes: " + ex.Message);
                return null;
            }

            byte[] bytes = md.ComputeHash(encoded);

            // Output the bytes of the hash as a string (text/plain)
            StringBuilder sb = new StringBuilder(2 * bytes.Length);
            for (int i = 0; i < bytes.Length; i++)
            {
                int low = (int)(bytes[i] & 0x0f);
                int high = (int)((bytes[i] & 0xf0) >> 4);
                sb.Append(this.hexadecimal[high]);
                sb.Append(this.hexadecimal[low]);
            }

            return sb.ToString();
        }
        #endregion
    }
}