﻿using Microsoft.Owin.StaticFiles.ContentTypes;

namespace BoilerWebApi
{
    public class JsonContentTypeProvider : FileExtensionContentTypeProvider
    {
        public JsonContentTypeProvider()
        {
            Mappings.Add(".json", "application/json");
        }
    }
}