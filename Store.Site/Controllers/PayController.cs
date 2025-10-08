using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ApexStore.Application.Services.Carts;
using ApexStore.Application.Services.Fainances.Commands.AddRequestPay;
using ApexStore.Application.Services.Fainances.Queries.GetRequestPayService;
using ApexStore.Application.Services.Orders.Commands.AddNewOrder;
using ApexStore.Domain.Entities.Carts;
using Store.Site.Models;
using Store.Site.Utilities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Text;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using Parbad;
using Parbad.Gateway.ZarinPal;
using ApexStore.Domain.Entities.Finances;
using Parbad.AspNetCore;
using ApexStore.Domain.Entities.Orders;
//using ZarinPal.Class;
//using Dto.Payment;
//using Zarinpal.AspNetCore.Interfaces;
//using Zarinpal.AspNetCore.DTOs;


namespace Store.Site.Controllers
{
    [Authorize]
    public class PayController : Controller
    {
        private readonly IAddRequestPayService _addRequestPayService;
        private readonly ICartService _cartService;
        private readonly CookiesManeger _cookiesManeger;
        //private readonly Payment _payment;
        //private readonly Authority _authority;
        //private readonly Transactions _transactions;
        //private readonly IZarinpalService _zarinPalService;
        private readonly IGetRequestPayService _getRequestPayService;
        private readonly IAddNewOrderService _addNewOrderService;

        private readonly IOnlinePayment _onlinePayment;

        private static readonly HttpClient client = new HttpClient();
        //private readonly IZarinpalService _zarinpalService;
        public PayController(IAddRequestPayService addRequestPayService
            , ICartService cartService
            , IGetRequestPayService getRequestPayService
            , IAddNewOrderService addNewOrderService
            , IOnlinePayment onlinePayment)
        {
            _addRequestPayService = addRequestPayService;
            _cartService = cartService;
            _cookiesManeger = new CookiesManeger();
            //_zarinPalService = zarinPalService;
            //var expose = new Expose();
            //_payment = expose.CreatePayment();
            //_authority = expose.CreateAuthority();
            //_transactions = expose.CreateTransactions();
            _getRequestPayService = getRequestPayService;
            _addNewOrderService = addNewOrderService;

            _onlinePayment = onlinePayment;
            //_zarinpalService = zarinpalService;
        }

        public async Task<IActionResult> Index()
        {
            long? UserId = ClaimUtility.GetUserId(User);
            var cart = _cartService.GetMyCart(_cookiesManeger.GetBrowserId(HttpContext), UserId);
            var requestPay = _addRequestPayService.Execute(cart.Data.SumAmount, UserId.Value);

            if (cart.Data.SumAmount > 0)
            {

                //var result = await _onlinePayment.RequestAsync(invoice =>
                //{
                //    invoice.SetZarinPalData("پرداخت فاکتور شماره:" + requestPay.Data.RequestPayId)
                //    .SetCallbackUrl($"https://localhost:7043/Pay/Verify?guid={requestPay.Data.guid}")
                //    .SetAmount(25000)
                //    .SetGateway("ZarinPal");
                //});

                var result = await _onlinePayment.RequestAsync("ParbadVirtual", requestPay.Data.RequestPayId, 25000, $"https://localhost:7043/Pay/Verify?guid={requestPay.Data.guid}");

                //var paymentRequest = new ZarinpalRequestDTO(25000, "پرداخت فاکتور شماره:" + requestPay.Data.RequestPayId, $"https://localhost:7043/Pay/Verify?guid={requestPay.Data.guid}");

                //var paymentResponse = await _zarinPalService.RequestAsync(paymentRequest);

                if (result.IsSucceed == true)
                    return result.GatewayTransporter.TransportToGateway();
                else
                    ViewBag.Error = "خطا در اتصال به درگاه پرداخت.";
                return RedirectToAction("Index", "Cart");
            }



            //frist try
            //var result = await _payment.Request(new DtoRequest()
            //{
            //    Mobile = "09121112222",
            //    //CallbackUrl = $"https://localhost:7043/Pay/Verify?guid={requestPay.Data.guid}",
            //    CallbackUrl = $"https://localhost:7043/Pay/Verify",
            //    Description = "پرداخت فاکتور شماره:" + requestPay.Data.RequestPayId,
            //    Email = requestPay.Data.Email,
            //    //Amount = requestPay.Data.Amount,
            //    Amount =25000,
            //    MerchantId = "xxxxxxxxx-xxxx-xxxx-xxxx-xxxxxxxxxxx"
            //}, ZarinPal.Class.Payment.Mode.sandbox);
            //return Redirect($"https://sandbox.zarinpal.com/pg/StartPay/{result.Authority}");

            //twice try
            //int toman = 5000;
            //var request = new ZarinpalRequestDTO(toman, "خرید",
            //    $"https://localhost:7043/Pay/Verify?guid={requestPay.Data.guid}",
            //    email: "test@test.com",
            //    mobile: "09123456789",
            //    orderId: "1111");

            //var result = await _zarinpalService.RequestAsync(request);

            //if (result.Data != null)
            //{
            //    // You can store or log zarinpal data in database
            //    string authority = result.Data.Authority;
            //    int code = result.Data.Code;
            //    int fee = result.Data.Fee;
            //    string feeType = result.Data.FeeType;
            //    string message = result.Data.Message;
            //}

            //if (result.IsSuccessStatusCode)
            //    return Redirect(result.RedirectUrl);
            ////https://sandbox.zarinpal.com/
            //else
            //    return RedirectToAction("Index", "Cart");

            //try
            //string merchant = "xxxxxxxxx-xxxx-xxxx-xxxx-xxxxxxxxxxx";
            //string amount = "1100";
            //string authority;
            //string description = "خرید تستی ";
            //string callbackurl = $"https://localhost:7043/Pay/Verify?guid={requestPay.Data.guid}";
            //Models.RequestParameters Parameters = new Models.RequestParameters(merchant, amount, description, callbackurl, "", "");

            //var client = new RestClient(URLs.requestUrl);

            //Method method = Method.Post;

            //var request = new RestRequest("", method);

            //request.AddHeader("accept", "application/json");

            //request.AddHeader("content-type", "application/json");

            //request.AddJsonBody(Parameters);

            //var requestresponse = client.ExecuteAsync(request);

            //JObject jo = JObject.Parse(requestresponse.Result.Content);

            //string errorscode = jo["errors"].ToString();

            //JObject jodata = JObject.Parse(requestresponse.Result.Content);

            //string dataauth = jodata["data"].ToString();


            //if (dataauth != "[]" && dataauth != "{}")
            //{


            //    authority = jodata["data"]["authority"].ToString();

            //    string gatewayUrl = URLs.gateWayUrl + authority;

            //    return Redirect(gatewayUrl);

            //}
            //else
            //{
            //    return RedirectToAction("Index", "Cart");
            //}

            //try
            //var _url = "https://sandbox.zarinpal.com/pg/rest/WebGate/PaymentRequest.json";

            //var _values = new Dictionary<string, string>
            //{
            //    { "MerchantID", "xxxxxxxx-xxxx-xxxx-xxxx-xxxxxxxxxxxx" }, //Change This To work, some thing like this : xxxxxxxx-xxxx-xxxx-xxxx-xxxxxxxxxxxx
            //    { "Amount", "500" }, //Toman
            //    { "CallbackURL",  $"https://localhost:7043/Pay/Verify?guid={requestPay.Data.guid}" },
            //    { "Mobile", "09101231122" },
            //    { "Description", "This is a test payment" }
            //};
            //var _paymentRequestJsonValue = JsonConvert.SerializeObject(_values);


            //HttpClient client = new HttpClient();
            //client.BaseAddress = new Uri(_url);
            //client.DefaultRequestHeaders
            //      .Accept
            //      .Add(new MediaTypeWithQualityHeaderValue("application/json"));//ACCEPT header

            //HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Post, "relativeAddress");
            //request.Content = new StringContent(_paymentRequestJsonValue,
            //                                    Encoding.UTF8,
            //                                    "application/json");//CONTENT-TYPE header

            //client.SendAsync(request)
            //      .ContinueWith(responseTask =>
            //      {
            //          Console.WriteLine("Response: {0}", responseTask.Result);
            //      });


            //var content = new StringContent(_paymentRequestJsonValue, Encoding.UTF8, "application/json");

            //var _response = await client.PostAsync(_url, content);
            //var _responseString = await _response.Content.ReadAsStringAsync();

            //ViewBag.StatusCode = _response.StatusCode;
            //ViewBag._responseString = _responseString;

            //ZarinPalRequestResponseModel _zarinPalResponseModel =
            // JsonConvert.DeserializeObject<ZarinPalRequestResponseModel>(_responseString);

            //if (_response.StatusCode != System.Net.HttpStatusCode.OK) // Post Error
            //    return View();

            //if (_zarinPalResponseModel.Status != 100) //Zarinpal Did not Accepted the payment
            //    return View();

            ////SandBox Mode
            //return Redirect("https://sandbox.zarinpal.com/pg/StartPay/"+_zarinPalResponseModel.Authority/*+"/Sad"*/); 

            //ZarinPal.ZarinPal zarinpal = ZarinPal.ZarinPal.Get();

            //String MerchantID = "71c705f8-bd37-11e6-aa0c-000c295eb8fc";
            //String CallbackURL = "http://localhost:59701/VerficationPage.aspx";
            //long Amount = 100;
            //String Description = "This is Test Payment";

            //ZarinPal.PaymentRequest pr = new ZarinPal.PaymentRequest(MerchantID, Amount, CallbackURL, Description);

            //zarinpal.EnableSandboxMode();
            //var res = zarinpal.InvokePaymentRequest(pr);
            //if (res.Status == 100)
            //{
            //    Response.Redirect(res.PaymentURL);
            //}
            else
            {
                return RedirectToAction("Index", "Cart");
            }
        }

        public async Task<IActionResult> Verify(Guid guid, string authority, string status)
        {

            var requestPay = _getRequestPayService.Execute(guid);

            var invoice = await _onlinePayment.FetchAsync();

            if(invoice == null)
                return RedirectToAction("Index", "Cart");

            var verifyResult = await _onlinePayment.VerifyAsync(invoice);

            var UserId = ClaimUtility.GetUserId(User);
            var cart = _cartService.GetMyCart(_cookiesManeger.GetBrowserId(HttpContext), UserId);
            if (verifyResult.IsSucceed)
            {
                _addNewOrderService.Execute(new RequestAddNewOrderSericeDto
                {
                    CartId = cart.Data.CartId,
                    UserId = UserId.Value,
                    RequestPayId = requestPay.Data.Id
                });
                return RedirectToAction("Index","orders");
            }
            //if (HttpContext.IsValidZarinpalVerifyQueries())
            //{
            //    var verify = new ZarinpalVerifyDTO(requestPay.Data.Amount,
            //           HttpContext.GetZarinpalAuthorityQuery()!);

            //    var response = await _zarinpalService.VerifyAsync(verify);

            //    if (response.Data != null)
            //    {
            //        // You can store or log zarinpal data in database
            //        ulong refId = response.Data.RefId;
            //        int fee = response.Data.Fee;
            //        string feeType = response.Data.FeeType;
            //        int code = response.Data.Code;
            //        string cardHash = response.Data.CardHash;
            //        string cardPan = response.Data.CardPan;
            //    }

            //    if (response.IsSuccessStatusCode)
            //    {
            //        // Do Somethings...
            //        var refId = response.RefId;
            //        var statusCode = response.StatusCode;
            //    }

            //    return View(response.IsSuccessStatusCode);
            //}
            return View(false);
        }
    }


    public class VerificationPayResultDto
    {
        public int Status { get; set; }
        public long RefID { get; set; }
    }
}