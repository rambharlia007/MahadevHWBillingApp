﻿using System;

namespace MahadevHWBillingApp.Models
{
    public class ExcelAttribute : Attribute
    {
        public string ColumnIndex { get; set; }
        public string Format { get; set; }
        public string ColumnName { get; set; }
        public bool IsTotalRequired = false;
    }
}