using System;
using System.Web;
using System.Web.UI;
using System.Text;
using System.Collections;

public class MessageBox 
{
    private static Hashtable m_executingPages = new Hashtable();

    /// <summary>
    /// constructor 
    /// </summary>
    private MessageBox() { }

    /// <summary>
    /// routine used to output a message to the user's screen
    /// </summary>
    /// <param name="sMessage"></param>
    public static void Show(string sMessage)
    {
        // If this is the first time a page has called this method then
        if (!m_executingPages.Contains(HttpContext.Current.Handler))
        {
            // Attempt to cast HttpHandler as a Page.
            Page executingPage = HttpContext.Current.Handler as Page;

            if (executingPage != null)
            {
                // Create a Queue to hold one or more messages.
                Queue messageQueue = new Queue();

                // Add our message to the Queue
                messageQueue.Enqueue(sMessage);

                // Add our message queue to the hash table. Use our page reference
                // (IHttpHandler) as the key.
                m_executingPages.Add(HttpContext.Current.Handler, messageQueue);

                // Wire up Unload event so that we can inject 
                // some JavaScript for the alerts.
                executingPage.Unload += new EventHandler(ExecutingPage_Unload);
            }
        }
        else
        {
            // If were here then the method has allready been 
            // called from the executing Page.
            // We have allready created a message queue and stored a
            // reference to it in our hastable. 
            Queue queue = (Queue)m_executingPages[HttpContext.Current.Handler];

            // Add our message to the Queue
            queue.Enqueue(sMessage);
        }
    }

    /// <summary>
    /// Our page has finished rendering so lets output the
    /// JavaScript to produce the alert's
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    public static void ExecutingPage_Unload(object sender, EventArgs e)
    {
        // Get our message queue from the hashtable
        Queue queue = (Queue)m_executingPages[HttpContext.Current.Handler];

        if (queue != null)
        {
            StringBuilder sb = new StringBuilder();

            // How many messages have been registered?
            int iMsgCount = queue.Count;

            // Use StringBuilder to build up our client slide JavaScript.
            sb.Append("<script language='javascript'>");

            // Loop round registered messages
            string sMsg;

            while (iMsgCount-- > 0)
            {
                sMsg = (string)queue.Dequeue();

                sMsg = sMsg.Replace("\n", "\\n");

                sMsg = sMsg.Replace("\"", "'");

                sb.Append(@"alert( """ + sMsg + @""" );");
            }

            if (HttpContext.Current.Session["Check_Unlock_Right"] != null &&
                    HttpContext.Current.Session["Check_Unlock_Right"].ToString() == "true")
                sb.Append(@"window.location=""CRLogin.aspx"";");
            else if (HttpContext.Current.Session["CRLogin_Error"] != null &&
                    HttpContext.Current.Session["CRLogin_Error"].ToString() == "true")
            {
                HttpContext.Current.Session["CRLogin_Error"] = "false";
                
                sb.Append(@"window.location=""CRLogin.aspx"";");
            }
            else
            {
                if (HttpContext.Current.Session["App_Mode"] != null)
                {
                    switch (HttpContext.Current.Session["App_Mode"].ToString())
                    {
                        case "RPHVerify":
                            if (HttpContext.Current.Session["App_SubMode"] != null)
                            {
                                switch (HttpContext.Current.Session["App_SubMode"].ToString())
                                {
                                    case "RPHVerify_Initial_Load":
                                        sb.Append(@"window.location=""Restock_Main.aspx"";");
                                        
                                        break;
                                }
                            }
                            break;
                        case "WeighPill":
                            sb.Append(@"window.location=""WeighPill.aspx"";");
                            
                            break;
                        case "WeighCanister":
                            sb.Append(@"window.location=""WeighCanister.aspx"";");
                            
                            break;
                        case "OPRestock":
                        case "RTS":
                            if (HttpContext.Current.Session["App_SubMode"] != null)
                            {
                                switch (HttpContext.Current.Session["App_SubMode"].ToString())
                                {
                                    //OpRestock
                                    case "OPReplenish_Complete":
                                        sb.Append(@"window.location=""Restock_Main.aspx"";");
                                        
                                        break;
                                    case "RTS_Vial_Scanned":
                                        sb.Append(@"window.location=""RTS_Canister.aspx"";");
                                        
                                        break;
                                    case "RTS_Canister_Vial_Scanned":
                                        sb.Append(@"window.location=""RTS_Canister.aspx"";");
                                        
                                        break;
                                    case "RTS_Canister_Initialize":
                                        sb.Append(@"window.location=""RTS_Canister.aspx"";");
                                        
                                        break;
                                    case "Initial_Load":
                                        sb.Append(@"window.location=""Restock_Main.aspx"";");
                                        
                                        break;
                                }
                            }

                            break;
                        case "AutoFill_Restock":

                        case "AutoFill":
                            if (HttpContext.Current.Session["App_SubMode"] != null)
                            {
                                switch (HttpContext.Current.Session["App_SubMode"].ToString())
                                {
                                    case "TechReplenish_Initial_Load":
                                        sb.Append(@"window.location=""AutoFill_Restock_Main.aspx"";");
                                        
                                        break;
                                    case "Replenish_LotInfo":
                                        sb.Append(@"window.location=""AutoFill_LotInformation.aspx"";");
                                        
                                        break;
                                    case "RPHVerify_Initial_Load":
                                        sb.Append(@"window.location=""AutoFill_Restock_Main.aspx"";");
                                        
                                        break;
                                    case "TechCalibrate_Initial_Load":
                                        sb.Append(@"window.location=""AutoFill_Restock_Main.aspx"";");
                                        
                                        break;
                                    case "Nonscannable_CWIP_Scanned":
                                        sb.Append(@"window.location=""Autofill_ReplenishCount.aspx"";");
                                        
                                        break;
                                    case "Replenish_NDC_Scanned":
                                        sb.Append(@"window.location=""Autofill_ReplenishCount.aspx"";");
                                        
                                        break;
                                    case "Replenish_Weigh_Scanned":
                                        sb.Append(@"window.location=""Autofill_ReplenishCount.aspx"";");
                                        
                                        break;
                                    case "Replenish_CWIP_Scanned":
                                        sb.Append(@"window.location=""Autofill_ReplenishCount.aspx"";");
                                        
                                        break;
                                    case "Door_Scan_From_Machine":
                                        sb.Append(@"window.location=""AutoFill_Restock_Main.aspx"";");
                                        
                                        break;
                                    case "Verify_Scanned_Canister":
                                        sb.Append(@"window.location=""Autofill_RPHVerifyCount.aspx"";");
                                        
                                        break;
                                    case "Verify_RPH_Tag":
                                        sb.Append(@"window.location=""Autofill_RPHVerifyCount.aspx"";");
                                        
                                        break;
                                    case "Verify_Scanned_RTS":
                                        sb.Append(@"window.location=""Autofill_RPHVerifyCount.aspx"";");
                                       
                                        break;
                                    case "Verify_Scanned_NDC":
                                        sb.Append(@"window.location=""Autofill_RPHVerifyCount.aspx"";");
                                        
                                        break;
                                    case "Canister_Scan_Initialize_Restock":
                                        sb.Append(@"window.location=""AutoFill_Restock_Main.aspx"";");
                                        
                                        break;
                                    case "Stock_Initialize":
                                        sb.Append(@"window.location=""AutoFill_RestockCanister.aspx"";");
                                        
                                        break;
                                    case "Initial_Load":
                                        sb.Append(@"window.location=""AutoFill_Restock_Main.aspx"";");
                                        
                                        break;
                                    case "AutoFill_Restock":
                                        sb.Append(@"window.location=""AutoFill_Restock_Main.aspx"";");
                                        
                                        break;
                                    case "Stock_RPHTag_Scanned":
                                        sb.Append(@"window.location=""AutoFill_RestockCanister.aspx"";");
                                        
                                        break;
                                }
                            }

                            break;
                    }
                }
            }

            //Close our JS
            sb.Append(@"</script>");

            //Were done, so remove our page reference from the hashtable
            m_executingPages.Remove(HttpContext.Current.Handler);

            //Write the JavaScript to the end of the response stream
            HttpContext.Current.Response.Write(sb.ToString());
        }
    }
}


