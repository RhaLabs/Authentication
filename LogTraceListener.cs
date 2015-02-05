/*
 * Created by SharpDevelop.
 * User: bcrawford
 * Date: 9/17/2014
 * Time: 5:01 PM
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using Microsoft.Exchange.WebServices.Data;
using Logger;

namespace Microsoft.Exchange.WebServices.Authentication
{
  /// <summary>
  /// Description of LogTraceListener.
  /// </summary>
  public class LogTraceListener : ITraceListener
  {
    public LogTraceListener()
    {
    }
    
    public void Trace(string traceType, string traceMessage)
    {
      var logger = LoggerAsync.InstanceOf.ExchangeLogger;
      
      logger.Trace("{1}:  {0}\r\n", traceMessage, traceType);
    }
  }
}
