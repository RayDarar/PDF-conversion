﻿using PDF_conversion.src.interfaces;
using PDF_conversion.src.logic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PDF_conversion.src.templates
{
    public class Application8Template : DataTemplateBase
    {
        public override string GetTemplateName() => "Приложение 8";

        public override DataFormatBase Parse(DataSource source)
        {
            throw new NotImplementedException();
        }
    }
}