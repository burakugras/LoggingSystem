﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.CrossCutingConcerns.Logging
{
    public class LogDetail
    {
        public string MethodName { get; set; }
        public string Username { get; set; }
        public string FullName { get; set; }
        public List<LogParameter> Parameters { get; set; }

        public LogDetail()
        {
            FullName=string.Empty;
            MethodName=string.Empty;
            Username=string.Empty;
            Parameters = new List<LogParameter>();
        }

        public LogDetail(string methodName, string username, string fullName, List<LogParameter> parameters)
        {
            MethodName = methodName;
            Username = username;
            FullName = fullName;
            Parameters = parameters;
        }
    }
}