using System;
using System.Collections.Generic;
using System.Text;

namespace ApplicationCore.Logging
{
    public class LogDetail
    {
        public LogDetail()
        {
            Timestamp = DateTime.Now;
        }

        public DateTime Timestamp { get; set; }
        public string Message { get; set; }

        // WHERE
        public string Product { get; set; }
        // client, server, library, etc...
        public string Layer { get; set; }
        // class name, file name, etc...
        public string Location { get; set; }
        public string Hostname { get; set; }

        // WHO
        public string UserId { get; set; }
        public string UserName { get; set; }

        // EVERYTHING ELSE
        public long? ElapsedMilliseconds { get; set; }  // only for performance entries
        public Exception Exception { get; set; }  // the exception for error logging
        public CustomException CustomException { get; set; }
        public string CorrelationId { get; set; } // exception shielding from server to client
        public Dictionary<string, object> AdditionalInfo { get; set; }  // everything else


        public static LogDetail GetLogDetail(string message, Exception ex)
        {
            return new LogDetail
            {
                Product = "NoiseEvent.API",
                Location = "API",    // this application
                Layer = "Job",                  // unattended executable invoked somehow
                UserName = Environment.UserName,
                Hostname = Environment.MachineName,
                Message = message,
                Timestamp = DateTime.Now,
                Exception = ex
            };
        }
    }
}
